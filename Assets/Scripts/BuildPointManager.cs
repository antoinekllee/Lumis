using UnityEngine;

public class BuildPointManager : MonoBehaviour
{
    [Header("Defenders")]
    [SerializeField] private GameObject laserDefenderPrefab = null;

    [Header("Settings")]
    [SerializeField] private float buildPointRange = 1.5f; // Max distance between player and build point to build here

    private Transform nearestBuildPoint = null; 

    private Transform[] buildPointTransforms = new Transform[0];
    private Transform playerTransform = null;

    private void Start()
    {
        buildPointTransforms = GetComponentsInChildren<Transform>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private Transform GetNearestBuildPoint(Vector3 playerPos)
    {
        // Transform nearestBuildPoint = null;
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

    private bool IsNearestBuildPointOpen()
    {
        return nearestBuildPoint.childCount == 0; 
    }

    public void AttemptBuildAtNearestBuildPoint()
    {
        if (!IsNearestBuildPointOpen())
        {
            Debug.LogError("Cannot build here, build point is occupied.");
            return;
        }

        Instantiate(laserDefenderPrefab, nearestBuildPoint.position, Quaternion.identity, nearestBuildPoint);
    }

    private void Update()
    {
        GetNearestBuildPoint(playerTransform.position); 
    }
}
