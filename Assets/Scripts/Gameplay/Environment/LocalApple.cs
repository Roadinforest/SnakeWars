using Gameplay.Common;
//using Network.Extensions;
//using Network.Schemas;
//using Network.Services.Factory;
//using Network.Services.RoomHandlers;

using LocalMode.Factory;
using Network.Schemas;
using LocalMode.Extensions;

using Reflex.Attributes;
using UnityEngine;

namespace Gameplay.Environment
{
    public class LocalApple : MonoBehaviour
    {
        private LocalVfxFactory _vfxFactory;

        [Inject]
        public void Construct(LocalVfxFactory vfxFactory)
        {
            _vfxFactory = vfxFactory;
        }

        public void Collect()
        {
            _vfxFactory.CreateAppleDeathVfx(transform.position);
            //gameObject.SetActive(false);
            ChangePosition();
            //不是直接销毁，而是回收到对象池中，下次再使用的时候直接从对象池中获取，节省了创建和销毁的开销
        }

        public void ChangePosition()
        {
            float minX = -20;
            float maxX = 20;
            float minZ = -20;
            float maxZ = 20;


            // 生成一个随机的Vector3，其中y=0
            float x = Random.Range(minX, maxX);
            float y = 0f; // 确保y轴始终为0
            float z = Random.Range(minZ, maxZ);

            transform.position = new Vector3(x, y, z);
        }
    }
}
