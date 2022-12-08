using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        //PauseMenuUI.gameObject.SetActive(false);
    }

    void Update()
    {
		// Requirement 1.1.0
        // Escape button will pause game, freezing time/movement
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Escape pressed, pausing game");
            PauseMenuUI.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }

	// Requirement 1.1.1
    public void ResumeGame()
    {
        PauseMenuUI.gameObject.SetActive(false);
        Debug.Log("Resuming Game");
        Time.timeScale = 1f;
    }

	// Requirement 1.1.2
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Switching from Game Over Menu to Main Menu");
        Time.timeScale = 1f;
    }

	// Requirement 1.1.3
    public void QuitGame()
    {
        Application.Quit();
        // This will still show in the Unity Engine console
        Debug.Log("Application has quit.");
    }
}
