using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool: MonoBehaviour
{

    [System.Serializable]
    public class Pool
    {
        public GroundType tag;
        public GameObject preFab;
        public int size = 10;
    }

    #region singleton
    public static EffectPool Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Pool> Pools;
    public Dictionary<GroundType, Queue<GameObject>> poolDictionary;
    void Start()
    {
        poolDictionary = new Dictionary<GroundType, Queue<GameObject>>();
        foreach (Pool pool in Pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.preFab);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }


    public GameObject SpawnFromPool(GroundType Tag, Vector3 position)
    {
        if (!poolDictionary.ContainsKey(Tag))
        {
            return null;
        }

        GameObject objectToSpawn = poolDictionary[Tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;

        poolDictionary[Tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }

}
