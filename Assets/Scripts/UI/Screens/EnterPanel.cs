using System;
using UI.Extensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Screens
{
    public class EnterPanel 
    {
        //private readonly TextField _usernameField;
        private readonly Button _singleButton;
        //private readonly Button _doubleButton;
        //private readonly Button _multiButton;
        //private readonly Label _errorLabel;

        //private Action _onUsernameSubmit;
        
        public EnterPanel(VisualElement parent)
        {
            //_usernameField = parent.Q<TextField>("username-field-local");
            _singleButton = parent.Q<Button>("single-button");
            //_doubleButton = parent.Q<Button>("double-button");
            //_multiButton = parent.Q<Button>("multileaderboardScreen-button");
            //_errorLabel = parent.Q<Label>("error-label");
            _singleButton.clicked += OnSingleButtonClicked;
            Debug.Log("Enter Panel Construct");
            
            //_usernameField.RegisterCallback(OnUsernameSubmitted());
        }


        // 点击事件处理方法
        private void OnSingleButtonClicked()
        {
            Debug.Log("Single Button Clicked!");
            // 可以在这里添加更多的逻辑
        }

        //public string Username => _usernameField.value;

        //public event Action QuitClicked
        //{
        //    //Debug.Log("quit clicked");
        //    //add => _quitButton.clicked += value;
        //    //remove => _quitButton.clicked -= value;
        //}

        //public event Action ConnectClicked
        //{
        //    add
        //    {
        //        _connectButton.clicked += value;
        //        _onUsernameSubmit += value;
        //    }
        //    remove
        //    {
        //        _connectButton.clicked -= value;
        //        _onUsernameSubmit -= value;
        //    }
        //}

        //public void HideError() => 
        //    _errorLabel.Hide();

        //public void ShowError(string error)
        //{
        //    _errorLabel.text = error;
        //    _errorLabel.Show();
        //}

        //public void BlockButtons() => 
        //    SetEnabledButtons(false);

        //public void UnblockButtons() => 
        //    SetEnabledButtons(true);

        //private EventCallback<KeyDownEvent> OnUsernameSubmitted() =>
        //    evt =>
        //    {
        //        if (evt.character != '\n') 
        //            return;

        //        _onUsernameSubmit?.Invoke();
        //    };

        //private void SetEnabledButtons(bool isEnable)
        //{
        //    Debug.Log($"set enable {isEnable}");
        //    _usernameField.SetEnabled(isEnable);
        //    _singleButton.SetEnabled(isEnable);
        //    _doubleButton.SetEnabled(isEnable);
        //    _multiButton.SetEnabled(isEnable);
        //}
    }
}
