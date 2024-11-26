using System;
using Cysharp.Threading.Tasks;
using Infrastructure;
using Reflex.Attributes;
using UnityEngine;

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
        public void Construct(Game game) =>
            _game = game;

        public event Action  SingleClicked;
        public event Action  DoubleClicked;
        public event Action  MultiClicked;
        
        protected override void Awake()
        {
            base.Awake();
            _enterPanel = new EnterPanel(Screen);
            Debug.Log("Enter Screen Construct");
        }

        // ����¼�������
        private void OnSingleButtonClicked()
        {
            Debug.Log("Single Button Clicked!");
            _enterPanel.HideError();
            this.Hide();//���ص�ǰ����
            SingleClicked?.Invoke();

            //��ʼ����ģʽ
            _localGameFactory.CreateSnake("123");

        }

        private void OnDoubleButtonClicked()
        {
            Debug.Log("Double Button Clicked!");
            this.Hide();//���ص�ǰ����

            DoubleClicked?.Invoke();
            //_enterPanel.ShowError("Waiting for development");
        }
        private void OnMultiButtonClicked()
        {
            Debug.Log("Multi Button Clicked!");
            this.Hide();//���ص�ǰ����
            MultiClicked?.Invoke();
            //_enterPanel.ShowError("Waiting for development");
        }

        private void OnEnable()
        {
            Debug.Log("Enter Screen Enable");
            _enterPanel.SingleClicked += OnSingleButtonClicked;
            _enterPanel.DoubleClicked += OnDoubleButtonClicked;
            _enterPanel.MultiClicked += OnMultiButtonClicked;
            _enterPanel.QuitClicked += OnQuitClicked;
        }

        private void OnDisable()
        {
            Debug.Log("Enter Screen Disable");
            _enterPanel.SingleClicked -= OnSingleButtonClicked;
            _enterPanel.DoubleClicked -= OnDoubleButtonClicked;
            _enterPanel.MultiClicked -= OnMultiButtonClicked;
            _enterPanel.QuitClicked -= OnQuitClicked;
        }


        private void OnQuitClicked()
        {
            Debug.Log("Quit Button Click");

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

    }
}
