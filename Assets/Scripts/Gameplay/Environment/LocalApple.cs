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
        [SerializeField] private UniqueId _uniqueId;
        //private NetworkTransmitter _transmitter;
        private LocalVfxFactory _vfxFactory;

        [Inject]
        //public void Construct(NetworkTransmitter transmitter, VfxFactory vfxFactory)
        //{
        //    _transmitter = transmitter;
        //    _vfxFactory = vfxFactory;
        //}

        public void Construct(LocalVfxFactory vfxFactory)
        {
            _vfxFactory = vfxFactory;
        }

        public void Collect()
        {
            //_transmitter.SendAppleCollect(_uniqueId.Value);
            _vfxFactory.CreateAppleDeathVfx(transform.position);
            gameObject.SetActive(false);
            //不是直接销毁，而是回收到对象池中，下次再使用的时候直接从对象池中获取，节省了创建和销毁的开销
        }

        public void ChangePosition(Vector2Schema current, Vector2Schema previous)
        {
            transform.position = current.ToVector3();
            gameObject.SetActive(true);
        }
    }
}
