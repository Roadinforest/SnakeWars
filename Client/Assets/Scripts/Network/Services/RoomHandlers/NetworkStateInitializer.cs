﻿using Colyseus;
using Network.Schemas;

namespace Network.Services.RoomHandlers
{
    public class NetworkStateInitializer : INetworkRoomHandler
    {
        private readonly NetworkPlayersListener _playersListener;
        private readonly NetworkApplesListener _applesListener;
        private readonly NetworkTimeListener _timeListener;
        private ColyseusRoom<GameRoomState> _room;

        public NetworkStateInitializer(NetworkPlayersListener playersListener, NetworkApplesListener applesListener,
            NetworkTimeListener timeListener)
        {
            _playersListener = playersListener;
            _applesListener = applesListener;
            _timeListener = timeListener;
        }

        public void Handle(ColyseusRoom<GameRoomState> room)
        {
            _room = room;
            _room.OnStateChange += OnStateChanged;
        }
        
        public void Dispose()
        {
            _room.OnStateChange -= OnStateChanged;
            _playersListener.Dispose();
        }

        private void OnStateChanged(GameRoomState state, bool isFirstState)
        {
            if (isFirstState == false)
                return;
            
            _room.OnStateChange -= OnStateChanged;
            _playersListener.Initialize(state);
            _applesListener.Initialize(state);
            _timeListener.Initialize(state);
        }
    }
}