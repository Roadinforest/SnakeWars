using System.Diagnostics;
using System.Linq;
using Network.Services.Snakes;
using Services;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace LocalMode.Factory
{
    public class LocalSnakesDestruction
    {
        private readonly SnakesRegistry _snakes;
        private readonly StaticDataService _staticData;
        private readonly LocalVfxFactory _vfxFactory;


        public LocalSnakesDestruction(SnakesRegistry snake,StaticDataService staticData, LocalVfxFactory vfxFactory)
        {
            _staticData = staticData;
            _vfxFactory = vfxFactory;
            _snakes = snake;
        }

        public void Destruct(Vector3 pos,string snakeId)
        {
            var info = _snakes[snakeId];
            var positions = info.Snake.GetBodyDetailPositions().ToArray();
            var skin = _staticData.ForSnakeSkin(info.Player.skinId);
            
            foreach (var position in positions) 
                _vfxFactory.CreateSnakeDeathVfx(position, skin);

        }
    }
}