using UI.Screens;
using UnityEngine;

namespace UI
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private ConnectionScreen _connectionScreen;
        [SerializeField] private LeaderboardScreen _leaderboardScreen;
        [SerializeField] private EnterScreen   _enterScreen;

        private void Start()
        {
            Debug.Log("In UIROOT");
            _connectionScreen.Hide();
            _leaderboardScreen.Hide();
            _enterScreen.Show();
            _enterScreen.SingleClicked += showLeaderBoard;
            _enterScreen.DoubleClicked += showConnectScreen;
            _enterScreen.MultiClicked += showConnectScreen;
            _connectionScreen.ReturnClicked += showEnterScreen;
        }

        private void showEnterScreen()
        {
            _enterScreen.Show();
            Debug.Log("Show Enter Screen");
        }
        private void showLeaderBoard()
        {
            _leaderboardScreen.Show();
            Debug.Log("Show leader board");
        }
        private void showConnectScreen()
        {
            _connectionScreen.Show();
            Debug.Log("Show connect screen");
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