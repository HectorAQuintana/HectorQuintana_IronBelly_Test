using System.Collections.Generic;

public class Pool<T> where T : new()
{
    private Queue<T> poolQueue;

    public Pool(int initialSize)
    {
        poolQueue = new Queue<T>();
        ExpandPool(initialSize);
    }

    public void ExpandPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            poolQueue.Enqueue(new T());
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
