using System.Collections.Generic;
using Gameplay;
using Gameplay.Common;
using Gameplay.Environment;
using Infrastructure;

using LocalMode.Extensions;
using UnityEngine;

namespace LocalMode.Factory
{
    public class LocalAppleFactory
    {
        private const string ApplePath = "Apple/Local Apple";

        private readonly Assets _assets;
        private readonly Dictionary<string, Apple> _apples;
        private int _xRange = 20;
        private int _zRnage = 20;

        public LocalAppleFactory(Assets assets)
        {
            _assets = assets;
            _apples = new Dictionary<string, Apple>();
            Debug.Log("Instantiate LocalAppleFactory");
        }

        public LocalApple CreateApple()
        {
            float x = Random.Range(-_xRange,_xRange);
            float y = 0f; // 确保y轴始终为0
            float z = Random.Range(-_zRnage,_zRnage);


            var localApple = _assets.Instantiate<LocalApple>(ApplePath, new Vector3(x, y, z), Quaternion.identity, null);
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