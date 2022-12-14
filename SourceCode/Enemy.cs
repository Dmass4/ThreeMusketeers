using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public Player Player;
    public Gold Gold;

    public EnemySpawner EnemySpawner;

    private int maxHealth = 100;
    private int currentHealth;
    public int damage = 7;
    private bool isAttacking = false;

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
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        EnemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();        
    }
	
	// Requirement 5.0.1
    // Update is called once per frame
    void Update()
    {
        // Only move if not attacking in order to stop and conduct attack animation
        if (isAttacking == false)
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
    }

    // Method for the enemy to take damage from the player
    // Update Healthbar
    public void enemyTakeDamge(int damage)
    {
        currentHealth -= damage;
        healthbar.setHealth(currentHealth);

        if(currentHealth <= 0)
        {
            enemyDie();
        }
    }

	// Requirement 5.0.2
    // For Trigger (passthrough?) type collisions with Player
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.tag == "Player" && isAttacking == false)
        {
            // Set boolean back to true to disbale movement along waypoints
            isAttacking = true;
            // Determine left or right knockback
            if (collision.transform.position.x <= transform.position.x)
            {
                collision.GetComponent<Player>().KnockFromRight = true;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                collision.GetComponent<Player>().KnockFromRight = false;
            }
            collision.GetComponent<Player>().knockbackTrigger = true;
            // Calls the IEnumerator attackAnimation() below to call a wait command while performing animation
            StartCoroutine("attackAnimation");
            //player.takeDamage(damage);
            collision.GetComponent<Player>().takeDamage(damage);
            Debug.Log("Player Hit by Enemy!");
        }
    }

    // onTriggerStay accounts for when player stays within the collision circle, hence to triggering another attack
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isAttacking == false)
        {
            // Set boolean back to true to disbale movement along waypoints
            isAttacking = true;
            // Determine left or right knockback
            if (collision.transform.position.x <= transform.position.x)
            {
                collision.GetComponent<Player>().KnockFromRight = true;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                collision.GetComponent<Player>().KnockFromRight = false;
            }
            collision.GetComponent<Player>().knockbackTrigger = true;
            // Calls the IEnumerator attackAnimation() below to call a wait command while performing animation
            StartCoroutine("attackAnimation");
            //player.takeDamage(damage);
            collision.GetComponent<Player>().takeDamage(damage);
            Debug.Log("Player Hit by Enemy!");
        }
    }

	// Requirement 5.0.3
    IEnumerator attackAnimation()
    {
        // Trigger spin animation
        animator.SetTrigger("SpinAttack");
        yield return new WaitForSeconds(0.75f);
        // Set boolean back to false to continue movement along waypoints
        isAttacking = false;
    }

    // Method triggers enemy death conditions such as generating gold
    void enemyDie()
    {
        Debug.Log("Enemy is dead! Dropping Gold");
        Instantiate(Gold, transform.position, Quaternion.identity);
        enemyDestroy();
    }

    // Method to actually destroy the enemy gameObject, kept separate for useful troublshooting if issues arise from removing the gameObject
    void enemyDestroy()
    {
        EnemySpawner.enemyWasKilled();
        Debug.Log("Destroying Enemy GameObject!");
        Destroy(this.gameObject);
    }

}
