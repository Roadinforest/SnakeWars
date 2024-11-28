
using System;
using System.Collections.Generic;
using Network.Schemas;
using Network.Services.Factory;
using UnityEngine;
using UI.Screens;
using Debug = UnityEngine.Debug;


namespace Network.Services.RoomHandlers
{
    public class NetworkTimeListener : IDisposable
    {
        private readonly NetworkGameFactory _networkGameFactory;
        private readonly List<Action> _disposes;
        private GameRoomState _state;
        private CountDownScreen _downScreen;

        public NetworkTimeListener(NetworkGameFactory networkGameFactory)
        {
            _networkGameFactory = networkGameFactory;
            _disposes = new List<Action>();
            _downScreen = GameObject.FindObjectOfType<CountDownScreen>();
        }

        public void Initialize(GameRoomState state)
        {
            _state = state;
            state.leftTime.OnChange(OnLeftTimeChanged);
        }
        
        public void Dispose()
        {
            _disposes.ForEach(dispose => dispose?.Invoke());
            _disposes.Clear();
        }
        
        private void OnLeftTimeChanged()
        {
            if(_downScreen == null)
            {
                Debug.Log("DownScreen is null");
            _downScreen = GameObject.FindObjectOfType<CountDownScreen>();
                _downScreen.Show();
                return;
            }
            _downScreen.ShowCountDown((int)_state.leftTime.leftTime/1000);
        }
    }
}
