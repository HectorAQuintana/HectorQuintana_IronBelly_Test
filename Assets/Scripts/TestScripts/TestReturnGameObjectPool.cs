using UnityEngine;

public class TestReturnGameObjectPool : MonoBehaviour
{
    private GameObjectPool pool;

    void Start()
    {
        pool = FindObjectOfType<GameObjectPool>();
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            pool.ReturnPooledObject(gameObject);
        }
    }
}
