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
            //����ֱ�����٣����ǻ��յ�������У��´���ʹ�õ�ʱ��ֱ�ӴӶ�����л�ȡ����ʡ�˴��������ٵĿ���
        }

        public void ChangePosition()
        {
            float minX = -20;
            float maxX = 20;
            float minZ = -20;
            float maxZ = 20;


            // ����һ�������Vector3������y=0
            float x = Random.Range(minX, maxX);
            float y = 0f; // ȷ��y��ʼ��Ϊ0
            float z = Random.Range(minZ, maxZ);

            transform.position = new Vector3(x, y, z);
        }
    }
}
