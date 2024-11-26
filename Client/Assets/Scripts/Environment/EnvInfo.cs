using System.Collections.Generic;
using System.Dynamic;
using JetBrains.Annotations;
using UnityEngine;


namespace Environment
{
    public class EnvInfo: MonoBehaviour
    {
        private int currentScene = -1;
        private Dictionary<int, int> boundry = new Dictionary<int, int>
            {
                {0, 5},
                {1, 10},
                {2, 15}
            };

        public void setIndex(int index)
        {
            currentScene = index;
            if (index < 0 || index >= 3) return;
            Debug.Log(getBoundry());
        }

        public int getIndex()
        {
            return currentScene;
        }

        public int getBoundry()
        {
            return boundry[currentScene];
        }

        public int getBoundry(int index)
        {
            if (!boundry.ContainsKey(index)) return -1;
            return boundry[index];
        }
    }
}
