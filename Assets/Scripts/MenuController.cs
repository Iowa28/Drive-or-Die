using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private int levelSceneIndex = 1;
    
    public void StartGame()
    {
        SceneManager.LoadScene(levelSceneIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
