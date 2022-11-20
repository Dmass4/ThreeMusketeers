using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject tower;

    public int maxHealth = 100;
    public int currentHealth;
    public int goldCount;
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

    public bool isFacingLeft;
    private Vector2 facingLeft;

    // Start is called before the first frame update
    void Start()
    {
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

        /*
        if (Input.GetKeyDown(KeyCode.Space)) //Test to see how damage works to player
        {
            takeDamage(20);
        }
        */

        //Test to place towers
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Vector2 mousePosition = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(tower, mousePosition, Quaternion.identity);
            Debug.Log("Tower Placed!");
        }
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

        // invincibity to prevent too many knockbacks at once
        if (invincibility == false)
        {
            // Determine knockback trigger and direction
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

    // Coin Collions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            // 1 gold coin = 10 goldCount
            Debug.Log("Gold coin collected! Adding 10");
            goldCount += 10;
        }
    }

    public void addCoin(int amount)
    {
        goldCount += amount;
    }

}
