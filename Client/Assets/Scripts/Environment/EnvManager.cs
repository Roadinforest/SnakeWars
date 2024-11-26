using UnityEngine;


namespace Environment
{
    public class EnvManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] Manager;

        public void clear()
        {
            for(int i=0;i<Manager.Length;i++)
            {
                Manager[i].SetActive(false);
            }
        }

        public void changeEnv(int index)
        {
            if(index < 0 || index >= Manager.Length)
            {
                Debug.Log("Error loading Env at index "+index);
                return;
            }
            clear();
            Manager[index].SetActive(true);
            Manager[index].transform.position = Vector3.zero;
        }
    }
}