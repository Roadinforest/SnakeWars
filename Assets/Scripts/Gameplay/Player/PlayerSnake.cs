using Reflex.Attributes;
using Services;
using UnityEngine;

namespace Gameplay.Player
{
    // 基本上就是掌控着Aim的小环，还有输入
    public class PlayerSnake : MonoBehaviour
    {
        [SerializeField] private PlayerAim _playerAim;
        private InputService _input;
        
        public Vector3 TargetPoint { get; private set; }
        
        [Inject]
        public void Construct(InputService input) => 
            _input = input;

        private void Update()
        {
           // 如果没有按下，就隐藏；否则显示 
            if (!_input.IsMoveButtonPressed())
            {
                _playerAim.gameObject.SetActive(false);
            }
            else
            {
                TargetPoint = _input.WorldMousePosition();
                _playerAim.gameObject.transform.position = TargetPoint;
                _playerAim.gameObject.SetActive(true);
            }
        }
    }
}