using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    [SerializeField] 
    private GameObject prefab;
    [SerializeField] 
    private int initialPoolSize = 10;

    private Pool<GameObject> pool;

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
        return obj;
    }

    public void ActivatePooledObject()
    {
        GameObject obj = pool.GetObject();
        obj.SetActive(true);
    }

    public void ReturnPooledObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.ReturnObject(obj);
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
}