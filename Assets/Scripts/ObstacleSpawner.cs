using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> obstacles;
    [SerializeField] private int poolMaxSize;

    [SerializeField] private int firstIteration;
    
    [SerializeField] private int spawnInterval = 150;

    [SerializeField] private float rightEdge = -7.75f;
    [SerializeField] private float leftEdge = 7.75f;
    
    private int lastSpawnX = 0;
    private GameObject obstaclesFolder;
    private Queue<GameObject> pool;
    
    void Start()
    {
        obstaclesFolder = GameObject.Find("Obstacles");

        pool = new Queue<GameObject>();
        
        while (firstIteration > 0)
        {
            firstIteration--;
            SpawnObstacles();
        }
    }

    public void SpawnObstacles()
    {
        if (pool.Count >= poolMaxSize)
        {
            GameObject oldObstacle = pool.Dequeue();
            Destroy(oldObstacle);
        }
        
        lastSpawnX += spawnInterval;

        GameObject obstacle = obstacles[Random.Range(0, obstacles.Count)];

        GameObject poolObstacle =  Instantiate(obstacle, 
            new Vector3(lastSpawnX, obstacle.transform.position.y, Random.Range(rightEdge, leftEdge)), 
            Quaternion.identity, obstaclesFolder.transform);
            
        pool.Enqueue(poolObstacle);
    }
}
