using UnityEngine;
using Services;
using Reflex.Attributes;

namespace Gameplay.SnakeLogic
{
    public class SnakeHead : MonoBehaviour
    {
        private float _speed;
        private Quaternion _targetRotation;

        [Inject]private StaticDataService staticDataService;

        public void Construct(float speed) => 
            _speed = speed;

        // 更加流畅丝滑
        public void LookAt(Vector3 target)
        {
            var direction = target - transform.position;
            _targetRotation = Quaternion.LookRotation(direction, Vector3.up);

            var timeStep = Time.deltaTime * staticDataService.ForSnake().RotationSpeed;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, timeStep);

            //var direction = target - transform.position;
            //_targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            //transform.rotation = _targetRotation;
        }

        //public void LookAt(Vector3 target)
        //{
        //    var direction = target - transform.position;
        //    _targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        //    transform.rotation = _targetRotation;
        //}

        public void ResetRotation() => 
            _targetRotation = transform.rotation;

        private void Update()
        {
            MoveHead();
        }

        private void MoveHead()
        {
            var timeStep = Time.deltaTime * _speed;
            transform.Translate(transform.forward * timeStep, Space.World);
        }
    }
}