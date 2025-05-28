using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class PoolItem
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public static ObjectPool Instance;

    public List<PoolItem> items;
    private Dictionary<string, Queue<GameObject>> poolDict;

    void Awake()
    {
        Instance = this;
        poolDict = new Dictionary<string, Queue<GameObject>>();

        foreach (var item in items)
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            for (int i = 0; i < item.size; i++)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }
            poolDict.Add(item.tag, queue);
        }
    }

    public GameObject Spawn(string tag, Vector3 pos, Quaternion rot)
    {
        if (!poolDict.ContainsKey(tag))
        {
            Debug.LogWarning("No pool for tag: " + tag);
            return null;
        }

        GameObject obj = poolDict[tag].Dequeue();
        obj.SetActive(true);
        obj.transform.SetPositionAndRotation(pos, rot);
        poolDict[tag].Enqueue(obj);
        return obj;
    }
}