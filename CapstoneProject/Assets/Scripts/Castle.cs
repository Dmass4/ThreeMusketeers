using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthbar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.setMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Method for the tower to take damage from the player
    public void castleTakeDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.setHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Castle Destroyed!");
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

}