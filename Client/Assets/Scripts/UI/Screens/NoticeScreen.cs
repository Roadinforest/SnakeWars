using System;
using Cysharp.Threading.Tasks;
using Infrastructure;
using Reflex.Attributes;
using UnityEngine;
namespace UI.Screens
{
    public class NoticeScreen : GameScreen
    {
        private Game _game;
        private NoticePanel _panel;

        [Inject]
        public void Construct(Game game) => 
            _game = game;

        public event Action  QuitClicked;
        public event Action  HomeClicked;
        
        protected override void Awake()
        {
            base.Awake();
            _panel=new NoticePanel(Screen);
        }
        public void ShowInfo(string info)
        {
            this._panel.ShowInfo(info);
        }

        public void ShowGameEnd()
        {
            _panel.ShowGameEnd();
        }
        
        private void OnEnable()
        {
            _panel.QuitClicked += OnQuitClicked; 
            _panel.HomeClicked += OnHomeClicked;
        }
        
        private void OnDisable()
        {
            _panel.QuitClicked -= OnQuitClicked; 
            _panel.HomeClicked -= OnHomeClicked;
        }

        private void OnQuitClicked()
        {
            Debug.Log("Quit Button Click");
            QuitClicked?.Invoke();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void OnHomeClicked()
        {
            HomeClicked?.Invoke();
            this.Hide();
        }

    }
}
