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
        // Escape button will pause game, freezing time/movement
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Escape pressed, pausing game");
            PauseMenuUI.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ResumeGame()
    {
        PauseMenuUI.gameObject.SetActive(false);
        Debug.Log("Resuming Game");
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
