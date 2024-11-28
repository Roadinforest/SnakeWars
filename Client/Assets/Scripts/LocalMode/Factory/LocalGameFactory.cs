using Gameplay.SnakeLogic;

using LocalMode.Factory;
using UnityEngine;
using Network.Services.Snakes;

namespace LocalMode.Factory
{
    public class LocalGameFactory
    {
        private readonly SnakesRegistry _snakes;
        private readonly LocalSnakesFactory _snakesFactory;
        private readonly LocalSnakesDestruction _snakesDestruction;
        private readonly LocalAppleFactory _appleFactory;


        public LocalGameFactory(SnakesRegistry snakes, 
            LocalSnakesFactory snakesFactory, LocalSnakesDestruction snakesDestruction , LocalAppleFactory appleFactory)
        {
            _snakes = snakes;
            _snakesFactory = snakesFactory;
            _snakesDestruction = snakesDestruction;
            _appleFactory = appleFactory;
        }

        public void RemoveSnake(Vector3 pos)
        {
            _snakesDestruction.Destruct(pos,"Player");
            _snakesFactory.RemoveSnake();
        }

        public Snake CreateSnake() =>
                _snakesFactory.CreateLocalSnake();

        public void CreateApple(int count)
        {
            for(int i=0;i<count;i++)
            {
                _appleFactory.CreateApple();
            }
        }
    }
}