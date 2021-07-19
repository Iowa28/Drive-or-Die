using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private int maxLifePoint = 3;
    [SerializeField] private GameObject heartImage;
    [SerializeField] private Transform heartsFolder;
    [SerializeField] private float xPos = 80f;
    [SerializeField] private float yPos = -124f;
    [SerializeField] private float xOffset = 60f;
    [SerializeField] private float invincibilityBonusLenght;
    [SerializeField] private List<AudioSource> collisionSounds;

    private List<GameObject> heartImages = new List<GameObject>();
    private int currentLifePoint;
    
    private CarController carController;
    private RespawnManager respawnManager;

    private float invincibilityCounter;
    private float flashCounter;

    void Start()
    {
        currentLifePoint = maxLifePoint;
        carController = GetComponent<CarController>();
        respawnManager = GetComponent<RespawnManager>();

        for (int i = 0; i < maxLifePoint; i++)
        {
            AddHeartToList();
        }
    }

    private void AddHeartToList()
    {
        GameObject image = Instantiate(heartImage, new Vector3(xPos, yPos, 0), Quaternion.identity,
            heartsFolder);

        heartImages.Add(image);

        xPos += xOffset;
    }
    
    public void IncreaseLifePoint()
    {
        currentLifePoint++;

        if (currentLifePoint > maxLifePoint)
        {
            currentLifePoint = maxLifePoint;
        }
        else
        {
            AddHeartToList();
        }
    }

    public void DecreaseLifePoint()
    {
        if (!respawnManager.IsInvincible() && heartImages.Count > 0)
        {
            currentLifePoint--;
            Destroy(heartImages[heartImages.Count - 1]);
            heartImages.RemoveAt(heartImages.Count - 1);
            xPos -= xOffset;
            PlayCollisionSound();

            if (currentLifePoint <= 0)
            {
                FindObjectOfType<GameManager>().EndGame();
            }
            else
            {
                respawnManager.Respawn();
                carController.StartCar();
            }
        }
    }

    private void PlayCollisionSound()
    {
        AudioSource sound = collisionSounds[Random.Range(0, collisionSounds.Count)];
        sound.Play();
    }

    public float GetInvincibilityBonusLenght()
    {
        return invincibilityBonusLenght;
    }
}
