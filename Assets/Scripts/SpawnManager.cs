using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private RoadSpawner roadSpawner;
    private ObstacleSpawner obstacleSpawner;
    
    [SerializeField] private List<BonusSpawner> bonusSpawners;
    
    void Start()
    {
        roadSpawner = GetComponent<RoadSpawner>();
        obstacleSpawner = GetComponent<ObstacleSpawner>();
    }

    public void SpawnTriggerEntered()
    {
        roadSpawner.MoveRoad();
        obstacleSpawner.SpawnObstacles();
        
        foreach (BonusSpawner bonusSpawner in bonusSpawners)
        {
            bonusSpawner.SpawnBonus();
        }
    }
}
