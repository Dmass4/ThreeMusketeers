using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public HealthBar healthbar;
    public int maxHealth = 100;
    public int currentHealth;

    // boolean when taking damage to prevent too much knockback/damage at once
    private bool invincibility = false;
    
    public int goldCount;
    public TMP_Text goldCountDisplay;
    public GameObject TowerButtonPannel;

    public float speed;
    private  Waypoints Wpoints;
    private int waypointIndex;

    private Rigidbody2D rb;
    private Vector2 playerDirection;

    // Knockback variables
    public float KBForce = 15;
    public bool knockbackTrigger = false;
    public bool KnockFromRight;

    public bool isFacingLeft;
    private Vector2 facingLeft;

    // Start is called before the first frame update
    void Start()
    {
        TowerButtonPannel.gameObject.SetActive(false);
        rb = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
        healthbar.setMaxHealth(maxHealth);

        facingLeft = new Vector2(-transform.localScale.x, transform.localScale.y);
        isFacingLeft = false;

        Wpoints = GameObject.FindGameObjectWithTag("Waypoints").GetComponent<Waypoints>();
    }

    // Update is called once per frame
    void Update()
    {
        getPlayerDirection();
    }

    // Update is ran every physic based interaction
    private void FixedUpdate()
    {
        playerVelocity();

        if(Input.GetAxisRaw("Horizontal") > 0 && isFacingLeft)
        {
            isFacingLeft = false;
            flipFacing();
        }
        if(Input.GetAxisRaw("Horizontal") < 0 && !isFacingLeft)
        {
            isFacingLeft = true;
            flipFacing();
        }

        // Invincibity to prevent too much damage/knockbacks at once
        if (invincibility == false)
        {
            // Determine knockback trigger and direction to apply knockback
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

    void flipFacing()
    {
        if (isFacingLeft)
        {
            transform.localScale = facingLeft;
        }

        if (!isFacingLeft)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    // Method for the player to take damage from the enemy
    // Update Healthbar
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

    // Gold Collions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gold")
        {
            addGold(10);
        }
    }

    void addGold(int amount)
    {
        Debug.Log("Adding Gold");
        goldCount += amount;
        updateGoldCount();
    }

    public void subtractGold(int amount)
    {
        goldCount -= amount;
        updateGoldCount();
    }

    void updateGoldCount()
    {
        // Connects back to GoldCountPannel that displays a basic UI at the bottom of the game's camera
        // Displays goldCount 
        // Enables/disables visibility of TowerButtonPannel
        goldCountDisplay.text = goldCount.ToString();
        if (goldCount >= 20)
        {
            TowerButtonPannel.gameObject.SetActive(true);
        }
        
        else
        {
            TowerButtonPannel.gameObject.SetActive(false);
        }
    }

}
