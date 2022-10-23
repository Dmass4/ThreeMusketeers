using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public Player player;
    public int damage = 10;

    private void OnCollisionEnter2D(Collision2D collision) // For Trigger (passthrough?) type collisions
    {
        if (collision.gameObject.tag == "Player")
        {
            player.KBCounter += 10;
            if (collision.transform.position.x <= transform.position.x)
            {
                player.KnockFromRight = true;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                player.KnockFromRight = false;
            }
            player.takeDamage(damage);
        }
    }

    /*
    private void onCollisionEnter2D(Collider2D collision) // For collision (hardbody?) type collisions
    {
        processCollision(collision);
    }

    void processCollision(GameObject collider) // To save reunancy when testing whether we want collisions or triggers call this method
    {
        if (collider.CompareTag("Player"))
        {
            damagePlayer();
        }
    }


    void dmamagePlayer()
    {
        debug.log("Hit");
    }
    */

}