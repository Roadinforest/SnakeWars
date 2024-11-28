using System;
using Cysharp.Threading.Tasks;
using Infrastructure;
using Reflex.Attributes;
using UnityEngine;

using Environment;
using LocalMode.Factory;

namespace UI.Screens
{
    public class EnterScreen : GameScreen
    {
        [SerializeField] private string _emptyUsernameMessage = "EMPTY USERNAME!";

        private Game _game;
        private EnterPanel _enterPanel;
        private EnvManager _envManager;

        [Inject] private LocalGameFactory _localGameFactory;
        [Inject] private EnvInfo _envInfo;

        [Inject]
        public void Construct(Game game) =>
            _game = game;

        public event Action SingleClicked;
        public event Action DoubleClicked;
        public event Action MultiClicked;

        protected override void Awake()
        {
            base.Awake();
            _enterPanel = new EnterPanel(Screen);
            _envManager = GameObject.Find("EnvManager").GetComponent<EnvManager>();
            Debug.Log("Enter Screen Construct");
        }

        private void OnSingleButtonClicked()
        {
            _enterPanel.HideError();
            this.Hide();
            SingleClicked?.Invoke();
            _envManager.changeEnv(0);
            _envInfo.setIndex(0);

            //开始本地模式
            _localGameFactory.CreateApple(10);
            _localGameFactory.CreateSnake();
        }

        private void OnDoubleButtonClicked()
        {
            this.Hide();
            _envManager.changeEnv(1);
            _envInfo.setIndex(1);

            DoubleClicked?.Invoke();
        }
        private void OnMultiButtonClicked()
        {
            this.Hide();//隐藏当前界面
            MultiClicked?.Invoke();
            _envManager.changeEnv(2);
            _envInfo.setIndex(2);
        }

        private void OnEnable()
        {
            _enterPanel.SingleClicked += OnSingleButtonClicked;
            _enterPanel.DoubleClicked += OnDoubleButtonClicked;
            _enterPanel.MultiClicked += OnMultiButtonClicked;
            _enterPanel.QuitClicked += OnQuitClicked;
        }

        private void OnDisable()
        {
            _enterPanel.SingleClicked -= OnSingleButtonClicked;
            _enterPanel.DoubleClicked -= OnDoubleButtonClicked;
            _enterPanel.MultiClicked -= OnMultiButtonClicked;
            _enterPanel.QuitClicked -= OnQuitClicked;
        }


        private void OnQuitClicked()
        {

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();//Unuse in WebGL
#endif
        }

    }
}
