using System;
using UI.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Screens
{
    public class NoticePanel
    {
        private readonly Label _noticeLabel;
        private readonly Label _gameOverLabel;
        private readonly Button _homeButton;
        private readonly Button _quitButton;
        private readonly VisualElement _gameOverPanel;

        public NoticePanel(VisualElement parent)
        {
            _noticeLabel = parent.Q<Label>("notice-label");
            _gameOverLabel = parent.Q<Label>("gameover-label");
            _homeButton = parent.Q<Button>("home-button");
            _quitButton = parent.Q<Button>("quit-button");
            _gameOverPanel = parent.Q<VisualElement>("gameover-panel");
            Debug.Log("Enter CountDownScreen Construct");
        }

        public void ShowInfo(string info)
        {
            _gameOverPanel.Hide();
            _noticeLabel.Show();
            _noticeLabel.text = info;
        }

        public void ShowGameEnd()
        {
            _gameOverPanel.Show();
            _noticeLabel.Hide();
        }

        public event Action HomeClicked
        {
            add => _homeButton.clicked += value;
            remove => _homeButton.clicked -= value;
        }

        public event Action QuitClicked
        {
            add => _quitButton.clicked += value;
            remove => _quitButton.clicked -= value;
        }

        
    }
}
