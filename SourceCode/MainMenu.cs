using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene("TowerDefenseGame");
        Debug.Log("Switching from main menu to game");
    }

    public void QuitGame()
    {
        Application.Quit();
        // This will still show in the Unity Engine console
        Debug.Log("Application has quit.");
    }

}
