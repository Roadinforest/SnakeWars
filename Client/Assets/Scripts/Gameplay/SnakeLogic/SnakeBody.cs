using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.SnakeLogic
{
    public class SnakeBody : MonoBehaviour
    {
        [SerializeField] private Transform _head;
        [SerializeField] private float _detailDistance = 1f;

        private readonly List<Transform> _details = new List<Transform>();
        private readonly SnakeHistory _history = new SnakeHistory();
        
        public int Size => _details.Count;

        private void Awake() => 
            _history.Add(_head);

        public void AddDetail(GameObject detail)
        {
            var detailTransform = detail.transform;
            _details.Add(detailTransform);
            _history.Add(detailTransform);
        }

        public GameObject RemoveLastDetail()
        {
            if (_details.Count <= 1)
                throw new Exception("Snake has not details!");

            var detail = _details[0];
            _details.Remove(detail);
            _history.RemoveLast();
            return detail.gameObject;
        }

        public IEnumerable<Vector3> GetBodyDetailPositions() => 
            _details.Select(detail => detail.position);

        private void LateUpdate()
        {
            var distance = (_head.position - _history.FirstPosition).magnitude;
            distance = UpdateHistories(distance);
            MoveDetails(distance);
        }

        private float UpdateHistories(float distance)
        {
            //大于身体节数时，进行跟随移动
            while (distance > _detailDistance)
            {
                //确定方向
                var direction = (_head.position - _history.FirstPosition).normalized;
                //添加历史记录
                _history.AddToFront(_history.FirstPosition + direction * _detailDistance, _head.rotation);
                //减少距离
                distance -= _detailDistance;
            }

            return distance;
        }
        
        private void MoveDetails(float distance)
        {
            var percent = distance / _detailDistance;
            
            for (var i = 0; i < _details.Count; i++)
                (_details[i].position, _details[i].rotation) = _history.Lerp(i, percent);
        }
    }
}