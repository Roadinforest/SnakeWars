using System;
using Cysharp.Threading.Tasks;
using Infrastructure;
using Reflex.Attributes;
using UnityEngine;

//using LocalMode;
using LocalMode.Factory;

namespace UI.Screens
{
    public class EnterScreen: GameScreen
    {
        [SerializeField] private string _emptyUsernameMessage = "EMPTY USERNAME!";

        private Game _game;
        private EnterPanel _enterPanel;

        [Inject]
        private LocalGameFactory _localGameFactory;


        [Inject]
        //public void Construct(Game game, LocalGameFactory localGameFactory)
        //{
        //    _localGameFactory = localGameFactory;
        //    _game = game;
        //}
        public void Construct(Game game) =>
            _game = game;

        public event Action Connected;
        
        protected override void Awake()
        {
            base.Awake();
            _enterPanel = new EnterPanel(Screen);
            Debug.Log("Enter Screen Construct");
        }

        // 点击事件处理方法
        private void OnSingleButtonClicked()
        {
            Debug.Log("Single Button Clicked!");

            //LocalGameFactory l=new LocalGameFactory();
            _localGameFactory.CreateSnake("123");

            
            _enterPanel.HideError();
            this.Hide();//隐藏当前界面
            
        }

        private void OnDoubleButtonClicked()
        {
            Debug.Log("Double Button Clicked!");
            _enterPanel.ShowError("Waiting for development");
        }
        private void OnMultiButtonClicked()
        {
            Debug.Log("Multi Button Clicked!");
            _enterPanel.ShowError("Waiting for development");
            // 可以在这里添加更多的逻辑
        }

        private void OnEnable()
        {
            Debug.Log("Enter Screen Enable");
            _enterPanel.SingleClicked += OnSingleButtonClicked;
            _enterPanel.DoubleClicked += OnDoubleButtonClicked;
            _enterPanel.MultiClicked += OnMultiButtonClicked;

            //_enterPanel.ConnectClicked += OnConnectClicked;
            //_enterPanel.QuitClicked += OnQuitClicked;
        }

        private void OnDisable()
        {
            Debug.Log("Enter Screen Disable");
            _enterPanel.SingleClicked -= OnSingleButtonClicked;
            _enterPanel.DoubleClicked -= OnDoubleButtonClicked;
            _enterPanel.MultiClicked -= OnMultiButtonClicked;
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
