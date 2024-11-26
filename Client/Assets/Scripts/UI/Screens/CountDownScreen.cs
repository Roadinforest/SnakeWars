using System;
using Cysharp.Threading.Tasks;
using Infrastructure;
using Reflex.Attributes;
using UnityEngine;
namespace UI.Screens
{
    public class CountDownScreen : GameScreen
    {
        private Game _game;
        private CountDownPanel _panel;

        [Inject]
        public void Construct(Game game) => 
            _game = game;

        public event Action Connected;
        
        protected override void Awake()
        {
            base.Awake();
            _panel=new CountDownPanel(Screen);
        }
        public void ShowCountDown(int number)
        {
            _panel.ShowCountDown(number);
        }
        
        private void OnEnable()
        {
        }
        
        private void OnDisable()
        {
        }

    }
}
