using UI.Screens;
using UnityEngine;

namespace UI
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private ConnectionScreen _connectionScreen;
        [SerializeField] private LeaderboardScreen _leaderboardScreen;
        [SerializeField] private EnterScreen   _enterScreen;
        [SerializeField] private CountDownScreen _countdownScreen;

        private void Start()
        {
            Debug.Log("In UIROOT");
            _connectionScreen.Hide();
            _leaderboardScreen.Hide();
            _countdownScreen.Hide();
            _enterScreen.Show();

        }

        private void ShowEnterScreen()
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
        private void showCountdownScreen()
        {
            _countdownScreen.Show();
            _countdownScreen.ShowCountDown(99);
            Debug.Log("Show countdown screen");
        }

        private void setTypeLocal()
        {
            _connectionScreen.setType(0);
        }
        private void setTypeDouble()
        {
            _connectionScreen.setType(1);
        }
        private void setTypeMulti()
        {
            _connectionScreen.setType(2);
        }

        private void OnEnable()
        {
            _enterScreen.SingleClicked += showLeaderBoard;
            _enterScreen.DoubleClicked += showConnectScreen;
            _enterScreen.MultiClicked += showConnectScreen;

            _enterScreen.SingleClicked += setTypeLocal;
            _enterScreen.DoubleClicked += setTypeDouble;
            _enterScreen.MultiClicked += setTypeMulti;

            _enterScreen.MultiClicked += showCountdownScreen;

            _connectionScreen.ReturnClicked += ShowEnterScreen;
            _connectionScreen.Connected += showLeaderBoard;

            _connectionScreen.Connected += OnConnected;
        }

        private void OnDisable()
        {
            _enterScreen.SingleClicked -= showLeaderBoard;
            _enterScreen.DoubleClicked -= showConnectScreen;
            _enterScreen.MultiClicked -= showConnectScreen;
            _connectionScreen.ReturnClicked -= ShowEnterScreen;
            _connectionScreen.Connected -= showLeaderBoard;

            _connectionScreen.Connected -= OnConnected;
        }

        private void OnConnected()
        {
            _connectionScreen.Hide();
            _leaderboardScreen.Show();
        }
    }
}