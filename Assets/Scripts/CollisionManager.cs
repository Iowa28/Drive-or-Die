using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private CarController carController;
    private HealthController healthController;
    private GameManager gameManager;
    private RespawnManager respawnManager;
    private SpawnManager spawnManager;

    [SerializeField] private AudioSource canisterSound;
    [SerializeField] private AudioSource heartSound;
    [SerializeField] private AudioSource bonusSound;

    void Start()
    {
        carController = GetComponent<CarController>();
        healthController = GetComponent<HealthController>();
        gameManager = FindObjectOfType<GameManager>();
        respawnManager = FindObjectOfType<RespawnManager>();
        spawnManager = FindObjectOfType<SpawnManager>();
    }
    
    private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("InvincibilityCollider") && respawnManager.IsInvincible())
                other.transform.parent.GetComponent<BoxCollider>().enabled = false;

            if (other.CompareTag("Obstacle") && !respawnManager.IsInvincible())
            {
                carController.StopCar();
                healthController.DecreaseLifePoint();
            }
            if (other.CompareTag("Heart"))
            {
                heartSound.Play();
                Destroy(other.gameObject);
                healthController.IncreaseLifePoint();
            }
    
            if (other.CompareTag("Bonus"))
            {
                bonusSound.Play();
                Destroy(other.gameObject);
                respawnManager.MakeInvincibility(healthController.GetInvincibilityBonusLenght());
            }
            
            if (other.CompareTag("SpawnTrigger"))
                spawnManager.SpawnTriggerEntered();

            if (other.CompareTag("Canister"))
            {
                canisterSound.Play();
                Destroy(other.gameObject);
                gameManager.AddPetrol();
            }
        }
}
