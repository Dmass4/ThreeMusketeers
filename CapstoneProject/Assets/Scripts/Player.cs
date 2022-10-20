using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthbar;

    public float speed;
    private  Waypoints Wpoints;
    private int waypointIndex;

    private Rigidbody2D rb;
    private Vector2 playerDirection;

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

        if (Input.GetKeyDown(KeyCode.Space)) //Test to see how damage works to player
        {
            takeDamage(20);
        }
    }

    private void FixedUpdate()
    {
        playerVelocity();
    }

    void takeDamage(int damage) //In progress method for damaging the player
    {
        currentHealth -= damage;
        healthbar.setHealth(currentHealth);
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
