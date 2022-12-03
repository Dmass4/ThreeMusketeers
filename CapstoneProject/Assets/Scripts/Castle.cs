using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Castle : MonoBehaviour
{
    private int maxHealth = 30;
    public int currentHealth;

    public HealthBar healthbar;

    public GameObject GameOverMenu;
    public TextMeshProUGUI GameInfoText;

    // Start is called before the first frame update
    void Start()
    {
        GameOverMenu.gameObject.SetActive(false);
        currentHealth = maxHealth;
        healthbar.setMaxHealth(maxHealth);
    }

    //Method for the tower to take damage from the enemy
    public void castleTakeDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.setHealth(currentHealth);

        if (currentHealth <= 0)
        {
            endGame();
            Destroy(this.gameObject);
            
        }
    }

    // For Trigger (passthrough?) type collisions with Enemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            int collisionDamage = collision.GetComponent<Enemy>().damage;;
            castleTakeDamage(collisionDamage);
            Debug.Log("Castle Hit by Enemy!");
        }
    }

    private void endGame()
    {
        Debug.Log("Castle Destroyed. Switching from Game to Game Over Menu");
        GameOverMenu.gameObject.SetActive(true);
        GameInfoText.text = "Castle was destroyed, try focusing on protecting it more!";
        Time.timeScale = 0f;
    }
}