using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    [SerializeField] private GameObject obstacle;
    [SerializeField] private float obstacleSpeed = 3f;
    [SerializeField] private List<GameObject> obstaclePrefabs = new List<GameObject>();

    
    [SerializeField] private float spawnTimeMin = 2f;
    [SerializeField] private float spawnTimeMax = 5f;

    private float timeUntilObstacleSpawn; //number counts up until next spawn
    public float obstacleSpawnTime = 2f; // number dictates length of time before spawn

    



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            Spawn();
        }

        if (GameManager.Instance.canSpawn == true)
        {
            SpawnLoop();
        }


    }

    private void SpawnLoop()
    {
        timeUntilObstacleSpawn += Time.deltaTime;

        if (timeUntilObstacleSpawn >= obstacleSpawnTime)
        {
            Spawn();
            obstacleSpawnTime = Random.Range(spawnTimeMin, spawnTimeMax);
            timeUntilObstacleSpawn = 0f;
        }
    }

    private void Spawn()
    {
        GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];
        GameObject spawnedObstacle = Instantiate(obstacleToSpawn, transform.position, Quaternion.identity);
        Rigidbody2D obstacleRB = spawnedObstacle.GetComponent<Rigidbody2D>();
       
        obstacleRB.velocity = Vector2.left * obstacleSpeed;
        GameManager.Instance.currentObstacleSpeed = obstacleSpeed;
       
        GameManager.Instance.activeObstacles.Add(spawnedObstacle);
    }
}
