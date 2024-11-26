using System;
using UI.Extensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Screens
{
    public class CountDownPanel
    {
        private readonly Label _label;

        public CountDownPanel(VisualElement parent)
        {
            _label = parent.Q<Label>("countdown-label");
            //_label = parent.Q<Label>("error-label");
            Debug.Log("Enter CountDownScreen Construct");
        }

        public void ShowCountDown(int number)
        {
            int minutes = number / 60;
            int seconds = number % 60;
            _label.text = $"Left {minutes} Min {seconds} Sec";
        }
    }
}
