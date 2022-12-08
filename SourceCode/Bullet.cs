using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform target;

    public float speed = .75f;

    public int attackDamage = 100;


    public void seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;


        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

    }

	// Requirement 6.0.2
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");

        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().enemyTakeDamge(attackDamage);
        }

        Destroy(gameObject);

    }
}
