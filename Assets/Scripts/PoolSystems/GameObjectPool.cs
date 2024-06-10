using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameObjectPool : MonoBehaviour
{
    [SerializeField] 
    private GameObject prefab;
    [SerializeField] 
    private int initialPoolSize = 10;

    private Pool<GameObject> pool;
    private List<GameObject> activeObjects = new List<GameObject>();
    private int objectsSpawned = 0;

    public UnityAction OnObjectPooled;
    public UnityAction OnObjectReturned;

    void Start()
    {
        pool = new Pool<GameObject>(initialPoolSize, () => CreateObject(prefab));
    }

    private GameObject CreateObject(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab);
        obj.SetActive(false);
        return obj;
    }

    public GameObject GetPooledObject()
    {
        GameObject obj = pool.GetObject();
        obj.SetActive(true);
        objectsSpawned++;
        activeObjects.Add(obj);
        OnObjectPooled.Invoke();
        return obj;
    }

    public void ActivatePooledObjects(int qty)
    {
        for (int i = 0; i < qty; i++)
        {
            GetPooledObject();
        }
    }

    public void ReturnPooledObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.ReturnObject(obj);
        objectsSpawned--;
        OnObjectReturned.Invoke();
        activeObjects.Remove(obj);
    }

    public void ReturnPooledObjects(int qty)
    {
        if (qty > activeObjects.Count)
        {
            qty = activeObjects.Count;
        }

        for(int i = 0; i < qty; i++)  
        {
            ReturnPooledObject(activeObjects[0]);
        }
    }

    public void ExpandPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.ReturnObject(obj);
        }
    }

    public int GetObjectsCount => objectsSpawned;
}