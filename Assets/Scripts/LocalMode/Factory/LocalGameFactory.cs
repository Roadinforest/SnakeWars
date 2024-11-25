using Gameplay.SnakeLogic;

//using Network.Schemas;
//using Network.Services.RoomHandlers;
//using Network.Services.Snakes;

//using Network.Schemas;
using LocalMode.Snakes;
using LocalMode.Factory;
using UnityEngine;

namespace LocalMode.Factory
{
    public class LocalGameFactory
    {
        private readonly LocalSnakesRegistry _snakes;
        private readonly LocalSnakesFactory _snakesFactory;
        private readonly LocalSnakesDestruction _snakesDestruction;
        private readonly LocalAppleFactory _appleFactory;


        public LocalGameFactory(LocalSnakesRegistry snakes, 
            LocalSnakesFactory snakesFactory, LocalSnakesDestruction snakesDestruction , LocalAppleFactory appleFactory)
        {
            _snakes = snakes;
            _snakesFactory = snakesFactory;
            _snakesDestruction = snakesDestruction;
            _appleFactory = appleFactory;
            CreateApple(3);
        }

        public void RemoveSnake(string key)
        {
            if (!_snakes.Contains(key))
                return;
            
            _snakesDestruction.Destruct(key);
            _snakesFactory.RemoveSnake(key);
        }

        //public Snake CreateSnake(string key, PlayerSchema player) =>
        //        _snakesFactory.CreatePlayerSnake(key, player);

        public Snake CreateSnake(string key) =>
                _snakesFactory.CreateLocalSnake(key);

        public void CreateApple(int count)
        {
            for(int i=0;i<count;i++)
            {
                _appleFactory.CreateApple();
            }
        }

        //_networkStatus.IsPlayer(key) 
        //    ? _snakesFactory.CreatePlayerSnake(key, player) 
        //    : _snakesFactory.CreateRemoteSnake(key, player);

    }
}