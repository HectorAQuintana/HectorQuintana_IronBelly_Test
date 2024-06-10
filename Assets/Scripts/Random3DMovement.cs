using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random3DMovement : MonoBehaviour
{
    [SerializeField]
    private float zoneX = 10f;
    [SerializeField]
    private float zoneY = 10f;
    [SerializeField]
    private float zoneZ = 10f;
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float changeDirectionInterval = 2f;

    private Vector3 targetPosition;

    void Start()
    {
        SetRandomTarget();
        StartCoroutine(UpdateTargetPosition());
    }

    void Update()
    {
        MoveTowardsTarget();
    }

    private void SetRandomTarget()
    {
        float xPos = Random.Range(-zoneX, zoneX);
        float yPos = Random.Range(-zoneY, zoneY);
        float zPos = Random.Range(-zoneZ, zoneZ);
        targetPosition = new Vector3(xPos, yPos, zPos);
    }

    private void MoveTowardsTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private IEnumerator UpdateTargetPosition()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeDirectionInterval);
            SetRandomTarget();
        }
    }
}
