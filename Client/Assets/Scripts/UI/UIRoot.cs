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
        [SerializeField] private NoticeScreen _noticeScreen;

        private void Start()
        {
            Debug.Log("In UIROOT");
            _connectionScreen.Hide();
            _leaderboardScreen.Hide();
            _countdownScreen.Hide();
            _noticeScreen.Hide();
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
        private void showLoadingNotice()
        {
            _noticeScreen.Show();
            _noticeScreen.ShowInfo("Waiting for other players...");
        }
        private void showCountdownScreen()
        {
            _countdownScreen.Show();
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

            _connectionScreen.ReturnClicked += ShowEnterScreen;
            _connectionScreen.ConnectedSucceed += OnConnected;
        }

        private void OnDisable()
        {
            _enterScreen.SingleClicked -= showLeaderBoard;
            _enterScreen.DoubleClicked -= showConnectScreen;
            _enterScreen.MultiClicked -= showConnectScreen;
            _connectionScreen.ReturnClicked -= ShowEnterScreen;
            _connectionScreen.ConnectedSucceed -= showLeaderBoard;

            _connectionScreen.ConnectedSucceed -= OnConnected;
        }

        private void OnConnected()
        {
            _connectionScreen.Hide();
            _leaderboardScreen.Hide();
            _noticeScreen.Hide();
            showLoadingNotice();
        }

        public void GameStart()
        {
            _countdownScreen.Hide();
            _noticeScreen.Hide();
            _leaderboardScreen.Show();
        }
    }
}