using UnityEngine;

namespace Gameplay.Common
{
    public class UniqueId : MonoBehaviour
    {
        [field: SerializeField] public string Value { get; private set; }//一旦设置，不准更改

        public void Construct(string id) => 
            Value = id;
    }
}