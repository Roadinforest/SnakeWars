using Network.Services.RoomHandlers;
using Reflex.Attributes;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerNetworkSync : MonoBehaviour
    {
        [SerializeField] private PlayerAim _playerAim;
        private NetworkTransmitter _transmitter;
        
        [Inject]
        public void Construct(NetworkTransmitter transmitter) => 
            _transmitter = transmitter;

        private void Update() =>
            _transmitter.SendPosition(_playerAim.transform.position);
        //更新位置，这样其他客户端也能够更新
    }
}