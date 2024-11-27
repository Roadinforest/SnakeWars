using System;
using System.Collections.Generic;
using Colyseus;
using Cysharp.Threading.Tasks;
using Network.Schemas;
using Network.Services.RoomHandlers;
using Services;
using UnityEngine;
using UI;

namespace Network.Services
{
    public class NetworkClient
    {
        private const string GameRoomName = "GameRoom";
        private const string TestGameRoomName = "TestGameRoom";
        private readonly StaticDataService _staticData;
        private readonly IEnumerable<INetworkRoomHandler> _handlers;

        private ColyseusRoom<GameRoomState> _room;
        private ColyseusRoom<GameRoomState> _testRoom;
        private UIRoot _uIroot;
        
        public NetworkClient(StaticDataService staticData, IEnumerable<INetworkRoomHandler> handlers)
        {
            _staticData = staticData;
            _handlers = handlers;
        }

        public async UniTask<ConnectionResult> Connect(string username,int type)
        {
            var result = await TryConnect(username,type);

            if (result.IsFailure)
                Debug.Log("Fail In Connect");

            return result;
        }

        public async UniTask Disconnect()
        {
            if (_room == null)
                return;

            await _room.Leave();
            
            foreach (var handler in _handlers) 
                handler.Dispose();
        }

        private void InitializeHandlers()
        {
            foreach (var handler in _handlers)
                handler.Handle(_room);
        }

        private async UniTask<ConnectionResult> TryConnect(string username, int type)
        {
            var settings = _staticData.ForConnection();
            var client = new ColyseusClient(settings);

            try
            {
                _testRoom = await client.JoinOrCreate<GameRoomState>(TestGameRoomName, new Dictionary<string, object>()
                {
                    ["type"] = type,
                    [nameof(username)] = username
                });

                // 监听 seatReservation 消息
                _testRoom.OnMessage<ColyseusMatchMakeResponse>("seatReservation", async (reservation) =>
                {
                    try
                    {
                        Debug.Log("Get reservation session id :" + reservation.sessionId);
                        // 使用 seatReservation 加入房间
                        _room = await client.ConsumeSeatReservation<GameRoomState>(reservation);
                        InitializeHandlers();

                        _uIroot=GameObject.FindObjectOfType<UIRoot>();
                        _uIroot.GameStart();

                        Debug.Log("Joined room successfully");
                    }
                    catch (Exception e)
                    {
                        Debug.Log($"Error joining room inner: {e.Message}");
                    }
                });

            }
            catch (Exception exception)
            {
                Debug.Log($"Error joining room: {exception}");
                return ConnectionResult.Failure(exception.Message);
            }

            return ConnectionResult.Success();
        }
    }
}
