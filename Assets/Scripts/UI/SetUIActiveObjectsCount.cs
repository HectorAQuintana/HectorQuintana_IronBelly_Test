using UnityEngine;
using TMPro;

public class SetUIActiveObjectsCount : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMeshUI;

    private GameObjectPool objectPool;
    static private string LABELNAME = "Active Objects: ";

    private void Start()
    {
        objectPool = FindObjectOfType<GameObjectPool>();

        if(objectPool != null)
        {
            UpdateCount();
            objectPool.OnObjectPooled += UpdateCount;
            objectPool.OnObjectReturned += UpdateCount;
        }
    }

    private void OnDestroy()
    {
        if (objectPool != null)
        {
            objectPool.OnObjectPooled -= UpdateCount;
            objectPool.OnObjectReturned -= UpdateCount;
        }
    }

    public void UpdateCount()
    {
        if(objectPool != null)
        {
            textMeshUI.text = LABELNAME + objectPool.GetObjectsCount;
        }
    }
}
