﻿using Gameplay.Animations;
using Gameplay.Environment;
using Gameplay.SnakeLogic;
using LocalMode.Factory;
using Reflex.Attributes;
using UnityEngine;

namespace Gameplay.Player
{
    public class SnakeHeadTrigger : MonoBehaviour
    {
        [SerializeField] private SnakeHead _head;
        [SerializeField] private SnakeDeath _snakeDeath;
        [SerializeField] private SnakeHeadAnimator _animator;
        [SerializeField] private SphereCollider _mouthCollider;
        [SerializeField, Range(0, 180)] private float _deathAngle = 100f;
        [SerializeField] private LayerMask _targetMask;

        private readonly Collider[] _colliders = new Collider[3];

        [Inject] LocalGameFactory _localGameFactory;

        private void FixedUpdate()
        {
            var hits = OverlapHits();
            for (var i = 0; i < hits; i++)
                ProcessCollision(_colliders[i],i);
        }

        private int OverlapHits() =>
            Physics.OverlapSphereNonAlloc(_mouthCollider.transform.position, _mouthCollider.radius, _colliders, _targetMask);

        private void ProcessCollision(Component target,int i)
        {
            Debug.Log("In collision");
            if (target.TryGetComponent(out Apple apple))
            {
                _animator.PlayEat();
                apple.Collect();
            }
            else if (target.TryGetComponent(out LocalApple localApple))
            {
                _animator.PlayEat();
                localApple.Collect();
                Debug.Log("Eat local apple");
            }
            else if (target.TryGetComponent(out SnakeHead head))
            {
                var angle = Vector3.Angle(_head.transform.forward, head.transform.forward);
                if (angle > _deathAngle)
                    _snakeDeath.Die();
                _localGameFactory.RemoveSnake(transform.position);
            }
            else
            {
                _snakeDeath.Die();
                _localGameFactory.RemoveSnake(transform.position);
                
                Debug.Log("I die");
            }
            _colliders[i] = null;
        }
    }
}