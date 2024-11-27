using System;
using UI.Extensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Screens
{
    public class NoticePanel
    {
        private readonly Label _label;

        public NoticePanel(VisualElement parent)
        {
            _label = parent.Q<Label>("notice-label");
            Debug.Log("Enter CountDownScreen Construct");
        }

        public void ShowInfo(string info)
        {
            _label.text = info;
        }
    }
}
