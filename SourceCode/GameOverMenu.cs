using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{

    public void ReplayGame()
    {
        SceneManager.LoadScene("TowerDefenseGame");
        Debug.Log("Switching from Game Over Menu to Game");
        Time.timeScale = 1f;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Switching from Game Over Menu to Main Menu");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
        // This will still show in the Unity Engine console
        Debug.Log("Application has quit.");
    }
}
