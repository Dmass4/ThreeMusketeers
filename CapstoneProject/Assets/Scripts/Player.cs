using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;
    // boolean when taking damage to prevent too much knockback/damage at once
    private bool invincibility = false;

    public HealthBar healthbar;

    public float speed;
    private  Waypoints Wpoints;
    private int waypointIndex;

    private Rigidbody2D rb;
    private Vector2 playerDirection;

    // Knockback variables
    public float KBForce = 15;
    public bool knockbackTrigger = false;
    public bool KnockFromRight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
        healthbar.setMaxHealth(maxHealth);
        Wpoints = GameObject.FindGameObjectWithTag("Waypoints").GetComponent<Waypoints>();
    }

    // Update is called once per frame
    void Update()
    {
            getPlayerDirection();

        /*
        if (Input.GetKeyDown(KeyCode.Space)) //Test to see how damage works to player
        {
            takeDamage(20);
        }
        */
    }

    // Update is ran every physic based interaction
    private void FixedUpdate()
    {
        playerVelocity();

        // invincibity to prevent too many knockbacks at once
        if (invincibility == false)
        {
            if (knockbackTrigger == true)
            {
                if (KnockFromRight == true)
                {
                    rb.velocity = new Vector2(-KBForce, 0);
                }
                if (KnockFromRight == false)
                {
                    rb.velocity = new Vector2(KBForce, 0);
                }
                knockbackTrigger = false;
            }
        }
    }

    //In progress method for damaging the player
    public void takeDamage(int damage)
    {
        if (invincibility == false)
        {
            currentHealth -= damage;
            healthbar.setHealth(currentHealth);
            if (currentHealth <= 0)
            {
                Destroy(this.gameObject);
            }
            //Calls IEnumerator invincibilityFrames() below to call a wait command
            StartCoroutine("invincibilityFrames");
        }
    }

    IEnumerator invincibilityFrames()
    {
        invincibility = true;
        Debug.Log("Invincibility activated for 1.2 second(s)");
        yield return new WaitForSeconds(1.2f);
        invincibility = false;        
    }

    void getPlayerDirection()
    {
        float directionX = Input.GetAxisRaw("Horizontal");
        float directionY = Input.GetAxisRaw("Vertical");

        playerDirection = new Vector2(directionX, directionY).normalized;
    }

    void playerVelocity()
    {
        rb.velocity = new Vector2(playerDirection.x * speed, playerDirection.y * speed);
    }
}
