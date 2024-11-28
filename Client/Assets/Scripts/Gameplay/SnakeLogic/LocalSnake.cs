using System;
using System.Collections.Generic;
using Extensions;
using Gameplay.Common;

using LocalMode.Factory;
using Network.Schemas;

using Reflex.Attributes;
using Services.Leaders;
using UnityEngine;
using Services;
using Random = UnityEngine.Random;


namespace Gameplay.SnakeLogic
{
    public class LocalSnake: MonoBehaviour
    {
        [SerializeField] private Snake _snake;
        [SerializeField] private ushort _score=0;
        [SerializeField] private byte _size=3;
        private bool _isIntialized=false;
        public PlayerSchema _localSnakeData = new PlayerSchema();
        private readonly List<Action> _disposes = new List<Action>();
        
        private LocalSnakesFactory _snakesFactory;
        [Inject] private LeaderboardService _leaderboard;

        private InputService _input;
        public Vector3 TargetPoint { get; private set; }
        
        [Inject]
        public void Construct(LocalSnakesFactory snakesFactory,LeaderboardService leaderboard,InputService input)
        {
            _snakesFactory = snakesFactory;
            _leaderboard = leaderboard;
            _input = input;
        }

        public void Initialize(PlayerSchema _playerSchema)
        {
            _isIntialized = true;
            _localSnakeData=_playerSchema;
            _leaderboard.CreateLeader(_localSnakeData.username, _localSnakeData);
            ChangeSize(_localSnakeData.size);
        }

        public bool isInitialized()
        {
            return _isIntialized;
        }

        public void Initialize()
        {
            _localSnakeData = new PlayerSchema();
            _localSnakeData.username = "Player";

            _localSnakeData.position = new Vector2Schema();
            _localSnakeData.position.x=0;
            _localSnakeData.position.y=0;

            _localSnakeData.skinId = (byte)Random.Range(0, 7);
            _localSnakeData.size= (byte)3;
            _localSnakeData.score= (byte)0;

            _leaderboard.CreateLeader(_localSnakeData.username, _localSnakeData);
        }

        private void Update()
        {
            if (_input.IsMoveButtonPressed())
            {
                TargetPoint = _input.WorldMousePosition();
                _snake.LookAt(TargetPoint);
            }
            else
            {
                _snake.ResetRotation();
            }
        }

        private void OnDestroy()
        {
            if(_localSnakeData != null)
            _leaderboard.RemoveLeader(_localSnakeData.username);
            _disposes.ForEach(dispose => dispose?.Invoke());
            _disposes.Clear();
        }


        private void ChangePosition(Vector3 pos)
        {
            _snake.LookAt(pos);
        }

        public void EatApple()
        {
            _size++;
            _score++;
            ChangeScore(_score);
            ChangeSize(_size);
        }

        public void ChangeScore(ushort current) => 
            _leaderboard.UpdateLeader(_localSnakeData.username, current);

        public void ChangeSize(byte current)
        {
            if (_snake.Body.Size == current)
                return;

            ProcessChangeSizeTo(current);
        }

        private void ProcessChangeSizeTo(int target)
        {
            var difference = _snake.Body.Size - target;

            if (difference < 0)
                _snakesFactory.AddSnakeDetail(_localSnakeData.username, -difference);
            else
                _snakesFactory.RemoveSnakeDetails(_localSnakeData.username, difference);
        }
    }
}
