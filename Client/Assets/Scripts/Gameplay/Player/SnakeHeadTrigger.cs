using Gameplay.Animations;
using Gameplay.Environment;
using Gameplay.SnakeLogic;
using LocalMode.Factory;
using Reflex.Attributes;
using UnityEngine;
using Services.Leaders;
using System;

namespace Gameplay.Player
{
    public class SnakeHeadTrigger : MonoBehaviour
    {
        [SerializeField] private SnakeHead _head;
        [SerializeField] private SnakeDeath _snakeDeath;
        [SerializeField] private SnakeHeadAnimator _animator;
        [SerializeField] private SphereCollider _mouthCollider;
        [SerializeField] private LayerMask _targetMask;
        [SerializeField] private LocalSnake _localSnake;
        [SerializeField] private AudioSource _audioSource;

        private readonly Collider[] _colliders = new Collider[3];

        [Inject] LocalGameFactory _localGameFactory;

        // 检测头部是否有碰撞，如果有，则处理碰撞
        private void FixedUpdate()
        {
            var hits = OverlapHits();
            for (var i = 0; i < hits; i++)
                ProcessCollision(_colliders[i],i);
        }

        // 检测碰撞
        private int OverlapHits() =>
            Physics.OverlapSphereNonAlloc(_mouthCollider.transform.position, _mouthCollider.radius, _colliders, _targetMask);

        private void ProcessCollision(Component target,int index)
        {
            if (target.TryGetComponent(out Apple apple))
            {
                _animator.PlayEat();
                apple.Collect();
                _audioSource.Play();
            }

            else if (target.TryGetComponent(out LocalApple localApple))
            {
                _animator.PlayEat();
                localApple.Collect();
                _localSnake.EatApple();
                _audioSource.Play();
            }

            else
            {
                _snakeDeath.Die();
                if(_localSnake != null && _localSnake.isInitialized()==true)
                _localGameFactory.RemoveSnake(transform.position);
            }
            _colliders[index] = null;
        }
    }
}