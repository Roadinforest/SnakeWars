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

        public event Action Connected;
        
        protected override void Awake()
        {
            base.Awake();
            _panel=new NoticePanel(Screen);
        }
        public void ShowInfo(string info)
        {
            this._panel.ShowInfo(info);
        }
        
        private void OnEnable()
        {
        }
        
        private void OnDisable()
        {
        }

    }
}
