using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    private int enemyCounter = 0;
    private int enemyMaxCount = 5;

    public int enemiesKilled = 0;

    public GameObject GameOverMenu;
    public TextMeshProUGUI GameInfoText;

    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine("spawnEnemies");
       GameOverMenu.gameObject.SetActive(false);
    }

	// Requirement 5.0.4
    IEnumerator spawnEnemies()
    {
       // Set a limit to the loop to limit how many enemies spawn
        while(enemyCounter <= enemyMaxCount)
        {
            // wait in intervals before spawning next enemy via Instantiate (object, 2d vector location, quarternion.identity just sets the object to no rotation. A bit confusing, but it is necessary for unity to load the gameobject without error)
            yield return new WaitForSeconds(3.5f);
            Instantiate(enemy, new Vector2(5.5f,2f), Quaternion.identity);
            enemyCounter += 1;
        }
    }

    public void enemyWasKilled()
    {
        enemiesKilled += 1;

        if (enemiesKilled == enemyMaxCount)
        {
            // Show Game Over Pannel
            Debug.Log("All Enemies Destroyed. Switching from Game to Game Over Menu");
			// Requirement 1.2.3
            GameOverMenu.gameObject.SetActive(true);
            GameInfoText.text = "All enemies destroyed, good job defending!";
            Time.timeScale = 0f;
        }
    }

}
