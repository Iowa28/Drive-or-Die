using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{ 
    [SerializeField] private Text distanceText;
    [SerializeField] private GameObject gameOverMenu;

    private GameObject player;
    private PetrolController petrolController;
    
    private static int score;
    private static bool gameIsOver;
    
    void Start()
    {
        gameIsOver = false;
        player = GameObject.FindWithTag("Player");
        petrolController = GetComponent<PetrolController>();
    }

    void Update()
    {
        score = Mathf.RoundToInt(player.transform.position.x);
        distanceText.text = score + " m";
    }
    

    public void EndGame()
    {
        if (!gameIsOver)
        {
            gameIsOver = true;

            gameOverMenu.SetActive(true);
        }
    }

    public void AddPetrol()
    {
        petrolController.IncreasePetrolCount();
    }

    public void SetPetrolStopValue(bool value)
    {
        petrolController.SetStop(value);
    }

    public static bool IsGameOver()
    {
        return gameIsOver;
    }

    public static int GetScore()
    {
        return score;
    }
}
