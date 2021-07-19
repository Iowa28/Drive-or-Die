using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private int menuSceneIndex = 0;
    [SerializeField] private Text infoText;
    [SerializeField] private Text scoreText;

    private void OnEnable()
    {
        SetText(GameManager.GetScore());
    }

    private void SetText(int score)
    {
        int bestScore = PlayerPrefs.GetInt("score");
        if (score > bestScore || bestScore == 0)
        {
            bestScore = score;
            PlayerPrefs.SetInt("score", bestScore);
            infoText.text = "You have set a new record:";
        }

        scoreText.text = bestScore.ToString();
    }
    
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(menuSceneIndex);
    }
}
