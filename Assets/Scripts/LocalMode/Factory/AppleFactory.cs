using System.Collections.Generic;
using Gameplay;
using Gameplay.Common;
using Gameplay.Environment;
using Infrastructure;

using LocalMode.Extensions;
//using Network.Schemas;
using UnityEngine;
//using LocalMode.Schemas;

namespace LocalMode.Factory
{
    public class AppleFactory
    {
        private const string ApplePath = "Apple/Apple";

        private readonly Assets _assets;
        //private readonly Dictionary<string, Apple> _apples;

        public AppleFactory(Assets assets)
        {
            _assets = assets;
            //_apples = new Dictionary<string, Apple>();
        }

        public LocalApple CreateApple()
        {
            float minX = -10;
            float maxX = 10;
            float minZ = -10;
            float maxZ = 10;


            // 生成一个随机的Vector3，其中y=0
            float x = Random.Range(minX, maxX);
            float y = 0f; // 确保y轴始终为0
            float z = Random.Range(minZ, maxZ);

            var localApple = _assets.Instantiate<LocalApple>(ApplePath, new Vector3(x, y, z), Quaternion.identity, null);
            return localApple;
        }

        //public Apple CreateApple(string key, AppleSchema schema)
        //{
        //    var apple = _assets.Instantiate<Apple>(ApplePath, schema.position.ToVector3(), Quaternion.identity, null);
        //    apple.GetComponent<UniqueId>().Construct(key);
        //    //schema.OnPositionChange(apple.ChangePosition);
        //    _apples[key] = apple;
        //    return apple;
        //}

        //public void RemoveApple(string key)
        //{
        //if (!_apples.TryGetValue(key, out var apple)) 
        //    return;

        //_apples.Remove(key);
        //Object.Destroy(apple);
        //}
    }
}