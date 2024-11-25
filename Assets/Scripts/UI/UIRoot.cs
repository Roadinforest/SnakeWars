using UI.Screens;
using UnityEngine;

namespace UI
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private ConnectionScreen _connectionScreen;
        [SerializeField] private LeaderboardScreen _leaderboardScreen;
        //[SerializeField] private EnterScreen   _enterScreen;

        private void Start()
        {
            Debug.Log("In UIROOT");
            _connectionScreen.Show();
            //_leaderboardScreen.Show();
            //_enterScreen.Hide();
        }

        //private void OnEnable() => 
        //    _connectionScreen.Connected += OnConnected;

        //private void OnDisable() => 
        //    _connectionScreen.Connected -= OnConnected;

        private void OnConnected()
        {
            //_connectionScreen.Hide();
            //_leaderboardScreen.Show();
        }
    }
}