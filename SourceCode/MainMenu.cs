using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

	// Requirement 1.0.1
    public void PlayGame()
    {
        SceneManager.LoadScene("TowerDefenseGame");
        Debug.Log("Switching from main menu to game");
    }

	// Requirement 1.0.2
    public void QuitGame()
    {
        Application.Quit();
        // This will still show in the Unity Engine console
        Debug.Log("Application has quit.");
    }

}
