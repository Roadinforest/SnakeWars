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
        }

        public void ShowCountDown(int seconds)
        {
            int _minutes = seconds / 60;
            int _seconds = seconds % 60;
            _label.text = $"Left {_minutes} Min {_seconds} Sec";
        }
    }
}
