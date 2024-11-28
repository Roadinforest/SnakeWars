
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Colyseus.Schema;
using Extensions;
using Network.Schemas;
using Network.Services.Factory;
using Services.Leaders;
//using UnityEditorInternal;
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

            //state.players.OnAdd(OnPlayerAdded).AddTo(_disposes);
            //state.players.OnRemove(OnPlayerRemoved).AddTo(_disposes);
            //state.;
        }
        
        public void Dispose()
        {
            _disposes.ForEach(dispose => dispose?.Invoke());
            _disposes.Clear();
        }
        
        //private void OnLeftTimeChanged(DataChange[] dataChanges)
        private void OnLeftTimeChanged()
        {
            //Debug.Log("LeftTime Change");
            //Debug.Log(_state.leftTime.leftTime);
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
