using System.Collections.Generic;
using Gameplay.Environment;
using Infrastructure;
using Gameplay.Common;

using UnityEngine;
using System;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;

namespace LocalMode.Factory
{
    public class LocalAppleFactory
    {
        private const string ApplePath = "Apple/Local Apple";

        private readonly Assets _assets;
        private readonly Dictionary<string, LocalApple> _apples;
        private int _mapSize = 20;

        public LocalAppleFactory(Assets assets)
        {
            _assets = assets;
            _apples = new Dictionary<string, LocalApple>();
        }

        public LocalApple CreateApple()
        {
            float x = Random.Range(-_mapSize,_mapSize);
            float y = 0f; // 确保y轴始终为0
            float z = Random.Range(-_mapSize,_mapSize);


            var localApple = _assets.Instantiate<LocalApple>(ApplePath, new Vector3(x, y, z), Quaternion.identity, null);
            string key = Guid.NewGuid().ToString();
            
            localApple.GetComponent<UniqueId>().Construct(key);
            _apples[key] = localApple;
            return localApple;
        }
        // 本地的苹果没有必要添加单独标识


        //基本不会用到，因为本地的苹果没有单独的标识
        public void RemoveApple(string key)
        {
            if (!_apples.TryGetValue(key, out var apple))
                return;

            _apples.Remove(key);
            Object.Destroy(apple);
        }
    }
}