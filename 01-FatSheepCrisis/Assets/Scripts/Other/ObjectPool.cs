using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PrefabType
{
    DestoryFX,
    NumberText,
    BlueDiamond,
    OrangeDiamond,
    Gold,
}

public class ObjectPool : Singleton<ObjectPool>
{
    private Canvas canvas;

    public Dictionary<PrefabType, GameObject> prefabs = new Dictionary<PrefabType, GameObject>();

    [System.Serializable]
    public struct Prefabs
    {
        public PrefabType prefabType;
        public GameObject prefab;
    }

    public Prefabs[] prefab;

    public Dictionary<PrefabType, Queue<GameObject>> objectPool;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        canvas = GetComponentInChildren<Canvas>();
        canvas.worldCamera = Camera.main;
        objectPool = new Dictionary<PrefabType, Queue<GameObject>>();

        for (int i = 0; i < prefab.Length; i++)
        {
            prefabs[prefab[i].prefabType] = prefab[i].prefab;
        }
    }

    public void ClearCachePool()
    {
        objectPool.Clear();
    }

    ///<summary>
    ///ªÿ ’GameObject
    ///</summary>
    public void ReturnCacheGameObject(PrefabType type, GameObject obj)
    {
        if (obj == null) return;

        if (type != PrefabType.NumberText)
        {
            obj.transform.SetParent(this.transform);
        }

        obj.SetActive(false);

        if (!objectPool.ContainsKey(type))
        {
            objectPool[type] = new Queue<GameObject>();
        }

        objectPool[type].Enqueue(obj);
    }


    ///<summary>
    ///«Î«ÛGameObject
    ///</summary>
    public GameObject RequestCacheGameObject(PrefabType type, Vector3 position,float num = 0f,int numType = 0)
    {
        GameObject obj = GetFromPool(type);
        if (obj == null)
        {
            obj = Instantiate(prefabs[type]);
            obj.name = type.ToString() + "_" + Random.Range(1, 1000).ToString();
        }

        obj.SetActive(true);
        obj.transform.position = position;

        if (type == PrefabType.NumberText)
        {
            obj.transform.SetParent(canvas.transform);
            obj.GetComponent<DamageNum>().SetTextAndAnimator(num, numType);
        }
        return obj;
    }

    private GameObject GetFromPool(PrefabType type)
    {
        if (objectPool.ContainsKey(type) && objectPool[type].Count > 0)
        {
            GameObject obj = objectPool[type].Dequeue();
            return obj;
        }
        else
        {
            return null;
        }
    }
}
