using System;
using UI.Extensions;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

namespace UI.Screens
{
    public class EnterPanel 
    {
        private readonly Button _singleButton;
        private readonly Button _doubleButton;
        private readonly Button _multiButton;
        private readonly Button _quitButton;
        private readonly Label _errorLabel;

        
        public EnterPanel(VisualElement parent)
        {
            _singleButton = parent.Q<Button>("single-button");
            _doubleButton = parent.Q<Button>("double-button");
            _multiButton = parent.Q<Button>("multi-button");
            _quitButton= parent.Q<Button>("quit-button");
            _errorLabel = parent.Q<Label>("error-label");
        }


        public event Action SingleClicked
        {
            add => _singleButton.clicked += value;
            remove => _singleButton.clicked -= value;
        }

        public event Action DoubleClicked
        {
            add => _doubleButton.clicked += value;
            remove => _doubleButton.clicked -= value;
        }

        public event Action MultiClicked
        {
            add => _multiButton.clicked += value;
            remove => _multiButton.clicked -= value;
        }

        public event Action QuitClicked
        {
            add => _quitButton.clicked += value;
            remove => _quitButton.clicked -= value;
        }

        public void HideError() =>
            _errorLabel.Hide();

        public void ShowError(string error)
        {
            _errorLabel.text = error;
            _errorLabel.Show();
        }

    }
}
