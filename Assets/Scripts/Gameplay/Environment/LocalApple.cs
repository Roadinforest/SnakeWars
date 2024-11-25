using Gameplay.Common;

using LocalMode.Factory;

using Reflex.Attributes;
using UnityEngine;

namespace Gameplay.Environment
{
    public class LocalApple : MonoBehaviour
    {
        private int _xRange = 20;
        private int _zRnage = 20;
        private LocalVfxFactory _vfxFactory;

        [Inject]
        public void Construct(LocalVfxFactory vfxFactory)
        {
            _vfxFactory = vfxFactory;
        }

        public void SetRange(int xRange,int zRange)
        {
            _xRange = xRange;
            _zRnage = zRange;   
        }

        public void Collect()
        {
            _vfxFactory.CreateAppleDeathVfx(transform.position);
            ChangePosition();
            //不是直接销毁，而是回收到对象池中，下次再使用的时候直接从对象池中获取，节省了创建和销毁的开销
        }

        public void ChangePosition()
        {
            float x = Random.Range(-_xRange,_xRange);
            float y = 0f; // 确保y轴始终为0
            float z = Random.Range(-_zRnage,_zRnage);

            transform.position = new Vector3(x, y, z);
        }
    }
}
