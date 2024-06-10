using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDespawnUISystem : MonoBehaviour
{
    private GameObjectPool objectPool;
    private int quantityToSpawn = 0;

    // Start is called before the first frame update
    void Start()
    {
        objectPool = FindObjectOfType<GameObjectPool>();

    }

    public void UpdateQuantity(string qty)
    {
        quantityToSpawn = int.Parse(qty);
    }

    public void SpawnObjects()
    {
        if(quantityToSpawn <= 0)
        {
            return;
        }

        objectPool.ActivatePooledObjects(quantityToSpawn);
    }

    public void DespawnObjects()
    {
        if (quantityToSpawn <= 0)
        {
            return;
        }

        objectPool.ReturnPooledObjects(quantityToSpawn);
    }
}
