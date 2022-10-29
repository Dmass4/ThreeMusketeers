using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Player player;
    public Animator animator;

    private int maxHealth = 100;
    private int currentHealth;
    private int damage = 7;
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
    }

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

    //Method for the enemy to take damage from the player
    public void enemyTakeDamge(int damage)
    {
        currentHealth -= damage;
        healthbar.setHealth(currentHealth);

        if(currentHealth <= 0)
        {
            enemyDie();
        }
    }

    // For Trigger (passthrough?) type collisions with Player
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.tag == "Player")
        {
            // Set boolean back to true to disbale movement along waypoints
            isAttacking = true;
            // Determine left or right knockback
            if (collision.transform.position.x <= transform.position.x)
            {
                player.KnockFromRight = true;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                player.KnockFromRight = false;
            }
            player.knockbackTrigger = true;
            // Calls the IEnumerator attackAnimation() below to call a wait command while performing animation
            StartCoroutine("attackAnimation");
            player.takeDamage(damage);
            Debug.Log("Player Hit by Enemy!");
        }
    }

    IEnumerator attackAnimation()
    {
        // Trigger spin animation
        animator.SetTrigger("SpinAttack");
        yield return new WaitForSeconds(0.75f);
        // Set boolean back to false to continue movement along waypoints
        isAttacking = false;
    }

    void enemyDie()
    {
        Debug.Log("Enemy is dead!");
        Destroy(this.gameObject);
    }
}
