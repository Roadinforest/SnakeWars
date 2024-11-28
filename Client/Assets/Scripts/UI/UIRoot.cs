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
            _connectionScreen.Hide();
            _leaderboardScreen.Hide();
            _countdownScreen.Hide();
            _noticeScreen.Hide();
            _enterScreen.Show();

        }

        private void ShowGameEnd()
        {
            _countdownScreen.Hide();
            _noticeScreen.Show();
            _leaderboardScreen.Hide();
            _enterScreen.Hide();
            _noticeScreen.ShowGameEnd();
        }

        private void ShowEnterScreen()
        {
            _enterScreen.Show();
        }
        private void showLeaderBoard()
        {
            _leaderboardScreen.Show();
        }
        private void showConnectScreen()
        {
            _connectionScreen.Show();
        }
        private void showLoadingNotice()
        {
            _noticeScreen.Show();
        }
        private void showCountdownScreen()
        {
            _countdownScreen.Show();
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
            
            _noticeScreen.HomeClicked += ShowEnterScreen;
        }

        private void OnDisable()
        {
            _enterScreen.SingleClicked -= showLeaderBoard;
            _enterScreen.DoubleClicked -= showConnectScreen;
            _enterScreen.MultiClicked -= showConnectScreen;
            _connectionScreen.ReturnClicked -= ShowEnterScreen;
            _connectionScreen.ConnectedSucceed -= showLeaderBoard;

            _connectionScreen.ConnectedSucceed -= OnConnected;
            _noticeScreen.HomeClicked -= ShowEnterScreen;
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

        public void GameOver()
        {
            _countdownScreen.Hide();
            _noticeScreen.Show();
            _noticeScreen.ShowGameEnd();
            _leaderboardScreen.Hide();
        }
    }
}