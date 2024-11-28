using Gameplay.Animations;
using Gameplay.Environment;
using Gameplay.SnakeLogic;
using LocalMode.Factory;
using Reflex.Attributes;
using UnityEngine;
using Services.Leaders;

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
        [SerializeField] private LocalSnake _snake;

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
                //apple会发送信号，这里就不会手动增加
            }

            else if (target.TryGetComponent(out LocalApple localApple))
            {
                _animator.PlayEat();
                localApple.Collect();
                _snake.EatApple();//本地蛇触发加分
            }

            //else if (target.TryGetComponent(out SnakeHead head))
            //{
            //    var angle = Vector3.Angle(_head.transform.forward, head.transform.forward);
            //    if (angle > _deathAngle)
            //        _snakeDeath.Die();

            //    _localGameFactory.RemoveSnake(transform.position);
               

            //    Debug.Log("I die");
            //}
            else
            {
                _snakeDeath.Die();
                _localGameFactory.RemoveSnake(transform.position);
                
                Debug.Log("I die");
            }
            _colliders[index] = null;
        }
    }
}