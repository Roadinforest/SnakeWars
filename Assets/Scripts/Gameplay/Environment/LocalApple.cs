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
            //����ֱ�����٣����ǻ��յ�������У��´���ʹ�õ�ʱ��ֱ�ӴӶ�����л�ȡ����ʡ�˴��������ٵĿ���
        }

        public void ChangePosition()
        {
            float x = Random.Range(-_xRange,_xRange);
            float y = 0f; // ȷ��y��ʼ��Ϊ0
            float z = Random.Range(-_zRnage,_zRnage);

            transform.position = new Vector3(x, y, z);
        }
    }
}
