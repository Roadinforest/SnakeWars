using Gameplay.Common;
using Network.Services.Factory;
using Reflex.Attributes;
using UnityEngine;
using UI;

namespace Gameplay.SnakeLogic
{
    public class SnakeDeath : MonoBehaviour
    {
        [SerializeField] private UniqueId _uniqueId;

        private NetworkGameFactory _gameFactory;
        private UIRoot _uIroot;

        [Inject]
        public void Construct(NetworkGameFactory gameFactory) =>
            _gameFactory = gameFactory;

        public void Die()
        {
            _uIroot = GameObject.FindObjectOfType<UIRoot>();
            _uIroot.GameOver();
            _gameFactory.RemoveSnake(_uniqueId.Value);
        }
    }
}