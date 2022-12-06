using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    //Need to add public animator class when adding animation

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public int attackDamage = 25;
    public float attackRange = .5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack(); //Attack method is called when left click is hit 
        }
    }

    void Attack()
    {
        //Need to add attack animation to this method

        //Detect an array of enemies hit by the player attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage them by iterating over a for loop
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("Enemy Hit!");
            enemy.GetComponent<Enemy>().enemyTakeDamge(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
