using UnityEngine;

public class BuildPointManager : MonoBehaviour
{
    [SerializeField] private float buildPointRange = 5.0f; // Max distance between player and build point to build here

    private Transform[] buildPointTransforms = new Transform[0];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        buildPointTransforms = GetComponentsInChildren<Transform>();
    }

    public Transform GetNearestBuildPoint(Vector3 playerPos)
    {
        Transform nearestBuildPoint = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Transform buildPoint in buildPointTransforms)
        {
            float distance = Vector3.Distance(playerPos, buildPoint.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestBuildPoint = buildPoint;
            }
        }

        if (nearestDistance <= buildPointRange)
        {
            return nearestBuildPoint;
        }
        else
        {
            return null; 
        }
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
