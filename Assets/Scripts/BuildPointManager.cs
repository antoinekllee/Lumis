using UnityEngine;

public class BuildPointManager : MonoBehaviour
{
    [Header("Defenders")]
    [SerializeField] private GameObject laserDefenderPrefab = null;

    [Header("Building")]
    [SerializeField] private GameObject buildMenuObj = null; 
    [SerializeField] private float buildMenuYOffset = 2f; 
    private bool isBuildMenuOpen = false;

    [Header("Settings")]
    [SerializeField] private float buildPointRange = 1.5f; // Max distance between player and build point to build here

    private Transform nearestBuildPoint = null; 

    private Transform[] buildPointTransforms = new Transform[0];
    private Transform playerTransform = null;

    private void Start()
    {
        buildMenuObj.SetActive(false); 

        buildPointTransforms = GetComponentsInChildren<Transform>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private Transform GetNearestBuildPoint(Vector3 playerPos)
    {
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

    private void EnableMenuAtNearestBuildPoint()
    {
        buildMenuObj.transform.position = nearestBuildPoint.position + Vector3.up * buildMenuYOffset; 
        buildMenuObj.SetActive(true); 
        isBuildMenuOpen = true; 

        // TODO: enable menu animation, particle effect
    }

    private void CloseMenu()
    {
        buildMenuObj.SetActive(false); 
        isBuildMenuOpen = false; 
    }

    public void AttemptBuildAtNearestBuildPoint()
    {
        if (!IsNearestBuildPointOpen())
        {
            Debug.LogError("Cannot build here, build point is occupied."); 
            return; 
        }

        EnableMenuAtNearestBuildPoint(); 
    }

    private void SpawnDefenderAtNearestBuildPoint(GameObject defenderPrefab)
    {
        Instantiate(defenderPrefab, nearestBuildPoint.position, Quaternion.identity, nearestBuildPoint);
        CloseMenu();
    }

    private void Update()
    {
        GetNearestBuildPoint(playerTransform.position); 

        if (isBuildMenuOpen)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                // Spawn laser defender
                SpawnDefenderAtNearestBuildPoint(laserDefenderPrefab);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                // Spawn lightning defender
                // SpawnDefenderAtNearestBuildPoint(lightningDefenderPrefab);
                Debug.LogWarning("Lightning defender not implemented yet.");
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                // Spawn ball defender
                // SpawnDefenderAtNearestBuildPoint(ballDefenderPrefab);
                Debug.LogWarning("Ball defender not implemented yet.");
            }
            else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.E))
            {
                CloseMenu(); 
            }
        }
    }
}
