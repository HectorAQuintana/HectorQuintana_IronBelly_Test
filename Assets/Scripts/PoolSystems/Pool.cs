using System;
using System.Collections.Generic;

public class Pool<T>
{
    private Queue<T> poolQueue;
    private Func<T> OnCreate;

    public Pool(int initialSize, Func<T> OnCreate)
    {
        poolQueue = new Queue<T>();
        this.OnCreate = OnCreate;
        ExpandPool(initialSize);
    }

    public void ExpandPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            poolQueue.Enqueue(OnCreate());
        }
    }

    public T GetObject()
    {
        if (poolQueue.Count == 0)
        {
            ExpandPool(1);  // Expand pool by one if it's empty
        }

        return poolQueue.Dequeue();
    }

    public void ReturnObject(T obj)
    {
        poolQueue.Enqueue(obj);
    }
}
