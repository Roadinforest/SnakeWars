using System.Diagnostics;
using System.Linq;
using LocalMode.Snakes;
using Services;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace LocalMode.Factory
{
    public class LocalSnakesDestruction
    {
        private readonly LocalSnakesRegistry _snakes;
        private readonly StaticDataService _staticData;
        private readonly LocalVfxFactory _vfxFactory;


        public LocalSnakesDestruction(StaticDataService staticData, LocalVfxFactory vfxFactory)
        {
            _staticData = staticData;
            _vfxFactory = vfxFactory;
        }

        public void Destruct(Vector3 pos)
        {
            Debug.Log("Call SnakesDestruction");
            //foreach (var position in positions) 
                _vfxFactory.CreateSnakeDeathVfx(pos, _staticData.ForSnakeSkin(1));
        }
    }
}