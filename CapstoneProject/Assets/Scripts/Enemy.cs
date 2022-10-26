using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    private int maxHealth = 100;
    private int currentHealth;

    public HealthBar healthbar;

    public float speed = 3;
    private Waypoints Wpoints;
    private int waypointIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.setMaxHealth(maxHealth);
        Wpoints = GameObject.FindGameObjectWithTag("Waypoints").GetComponent<Waypoints>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Wpoints.waypoints[waypointIndex].position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, Wpoints.waypoints[waypointIndex].position) < 0.1f)
        {
            if (waypointIndex < Wpoints.waypoints.Length - 1)
            {
                waypointIndex++;
            }
            else
            {
                Destroy(gameObject);
            }
        }

    }

    public void enemyTakeDamge(int damage) //Method for the enemy to take damage from the player
    {
        currentHealth -= damage;
        healthbar.setHealth(currentHealth);

        if(currentHealth <= 0)
        {
            enemyDie();
        }
    }

    void enemyDie()
    {
        Debug.Log("Enemy is dead!");
        Destroy(this.gameObject);
    }
}
