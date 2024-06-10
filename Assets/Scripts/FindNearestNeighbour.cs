using System.Collections.Generic;
using UnityEngine;

public class FindNearestNeighbour : MonoBehaviour
{
    // Static list to contain all active neighbours
    private static List<FindNearestNeighbour> allNeighbours = new List<FindNearestNeighbour>();
    private FindNearestNeighbour nearestNeighbour = null;
    private LineRenderer lineRenderer;

    private void Awake()
    {     
        // Add a LineRenderer component if it doesn't exist
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }
    }

    private void OnEnable()
    {
        allNeighbours.Add(this);
    }

    private void OnDisable()
    {
        allNeighbours.Remove(this);
    }

    private void OnDestroy()
    {
        // In case the object is destroyed for some unintended reason
        allNeighbours.Remove(this);
    }

    private void LateUpdate()
    {
        FindNearest();

        if (nearestNeighbour != null)
        {
            DrawLineToNearestNeighbour();
        }
        else
        {
            // Disable the line if there is no nearest neighbour
            lineRenderer.enabled = false;
        }
    }

    private void FindNearest()
    {
        float minDistance = float.MaxValue;
        nearestNeighbour = null;

        // Loop through all neighbours to find the nearest one
        foreach (var neighbour in allNeighbours)
        {
            if (neighbour == this)
            {
                continue;
            }

            float distance = Vector3.Distance(transform.position, neighbour.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestNeighbour = neighbour;
            }
        }
    }

    private void DrawLineToNearestNeighbour()
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, nearestNeighbour.transform.position);
        }
    }
}
