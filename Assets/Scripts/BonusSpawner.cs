using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bonusPrefab;
    
    [SerializeField] private int spawnInterval = 250;
    [SerializeField] private int spawnProbability = 1;
    [SerializeField] private float rightEdge = -7.75f;
    [SerializeField] private float leftEdge = 7.75f;
    
    private int lastSpawnX = 0;
    private GameObject bonusesFolder;

    void Start()
    {
        bonusesFolder = GameObject.Find("Bonuses");
    }

    public void SpawnBonus()
    {
        lastSpawnX += spawnInterval;
        
        int probability = Random.Range(0, spawnProbability);
        if (probability == 0)
        {
            Instantiate(bonusPrefab, 
                new Vector3(lastSpawnX, bonusPrefab.transform.position.y, Random.Range(rightEdge, leftEdge)),
                bonusPrefab.transform.rotation, bonusesFolder.transform);
        }
    }
}
