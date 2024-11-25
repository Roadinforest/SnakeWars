using System;
using Cysharp.Threading.Tasks;
using Infrastructure;
using Reflex.Attributes;
using UnityEngine;

namespace UI.Screens
{
    public class EnterScreen: GameScreen
    {
        [SerializeField] private string _emptyUsernameMessage = "EMPTY USERNAME!";

        private Game _game;
        //private EnterPanel _enterPanel;

        [Inject]
        public void Construct(Game game) => 
            _game = game;

        public event Action Connected;
        
        protected override void Awake()
        {
            base.Awake();
            //_enterPanel = new EnterPanel(Screen);
            Debug.Log("Enter Screen Construct");
        }
        
        private void OnEnable()
        {
            Debug.Log("Enter Screen Enable");
            //_enterPanel.ConnectClicked += OnConnectClicked;
            //_enterPanel.QuitClicked += OnQuitClicked;
        }
        
        private void OnDisable()
        {
            Debug.Log("Enter Screen Disable");
            //_enterPanel.ConnectClicked -= OnConnectClicked;
            //_enterPanel.QuitClicked -= OnQuitClicked;
        }

        //private void OnConnectClicked() => 
        //    ConnectServer().Forget();
                
        //private void OnQuitClicked() => 
        //    Application.Quit();

        //private async UniTask ConnectServer()
        //{
        //    _enterPanel.HideError();
            
        //    if (string.IsNullOrWhiteSpace(_enterPanel.Username))
        //    {
        //        _enterPanel.ShowError(_emptyUsernameMessage);
        //        return;
        //    }

        //    _enterPanel.BlockButtons();

        //    //var result = await _game.Connect(_connectionPanel.Username);

        //    //if (result.IsSuccess)
        //    //    Connected?.Invoke();
        //    //else
        //    //    _connectionPanel.ShowError(result.Message);

        //    Connected.Invoke();//后来加上的

        //    _enterPanel.UnblockButtons();
        //}
    }
}
