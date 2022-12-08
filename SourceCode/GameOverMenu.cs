using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{

	// Requirement 1.2.4
    public void ReplayGame()
    {
        SceneManager.LoadScene("TowerDefenseGame");
        Debug.Log("Switching from Game Over Menu to Game");
        Time.timeScale = 1f;
    }

	// Requirement 1.2.5
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Switching from Game Over Menu to Main Menu");
        Time.timeScale = 1f;
    }

	// Requirement 1.2.6
    public void QuitGame()
    {
        Application.Quit();
        // This will still show in the Unity Engine console
        Debug.Log("Application has quit.");
    }
}
