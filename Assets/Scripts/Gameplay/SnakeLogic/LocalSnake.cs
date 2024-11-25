using System;
using System.Collections.Generic;
using Extensions;
using Gameplay.Common;

//using Network.Extensions;
//using Network.Schemas;
//using Network.Services.Factory;

using LocalMode.Extensions;
//using Network.Schemas;
using LocalMode.Factory;

using Reflex.Attributes;
using Services.Leaders;
using UnityEngine;
using Services;

namespace Gameplay.SnakeLogic
{
    public class LocalSnake: MonoBehaviour
    {
        [SerializeField] private Snake _snake;
        [SerializeField] private UniqueId _uniqueId;
        

        private readonly List<Action> _disposes = new List<Action>();
        
        private LocalSnakesFactory _snakesFactory;
        //private LeaderboardService _leaderboard;

        private InputService _input;
        
        public Vector3 TargetPoint { get; private set; }
        
        [Inject]
        public void Construct(LocalSnakesFactory snakesFactory,InputService input)
        {
            _snakesFactory = snakesFactory;
            _input = input;
        }
        //public void Construct(LocalSnakesFactory snakesFactory, LeaderboardService leaderboard)
        //{
        //    _snakesFactory = snakesFactory;
        //    _leaderboard = leaderboard;
        //}

        public void Initialize()
        {
        }

        private void Update()
        {
            Debug.Log("Update LocalSnake");
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
            _disposes.ForEach(dispose => dispose?.Invoke());
            _disposes.Clear();
        }


        private void ChangePosition(Vector3 pos)
        {
            _snake.LookAt(pos);
        }

        private void ChangeSize(byte current, byte previous)
        {
            if (_snake.Body.Size == current)
                return;

            ProcessChangeSizeTo(current);
        }

        private void ProcessChangeSizeTo(int target)
        {
            var difference = _snake.Body.Size - target;

            if (difference < 0)
                _snakesFactory.AddSnakeDetail(_uniqueId.Value, -difference);
            else
                _snakesFactory.RemoveSnakeDetails(_uniqueId.Value, difference);
        }
    }
}
