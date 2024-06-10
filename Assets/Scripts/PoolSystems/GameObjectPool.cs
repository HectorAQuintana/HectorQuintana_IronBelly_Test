using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    [SerializeField] 
    private GameObject prefab;
    [SerializeField] 
    private int initialPoolSize = 10;
    [SerializeField]
    private bool randomIniPos = false;
    [SerializeField]
    private Vector3 iniPosMaxRange = Vector3.one;
    [SerializeField]
    private Vector3 iniPosMinRange = Vector3.one;

    private Pool<GameObject> pool;

    void Start()
    {
        pool = new Pool<GameObject>(initialPoolSize, () => CreateObject(prefab));
    }

    private GameObject CreateObject(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab);
        SetRandomPos(obj);
        obj.SetActive(false);
        return obj;
    }

    private void SetRandomPos(GameObject obj)
    {
        if(!randomIniPos)
        {
            return;
        }

        float x = Random.Range(iniPosMinRange.x, iniPosMaxRange.x);
        float y = Random.Range(iniPosMinRange.y, iniPosMaxRange.y);
        float z = Random.Range(iniPosMinRange.z, iniPosMaxRange.z);

        obj.transform.position = new Vector3(x, y, z);
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