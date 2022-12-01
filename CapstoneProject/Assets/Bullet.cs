using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform target;

    public float speed = 2f;

    public int attackDamage = 5;

    public LayerMask enemyLayers;

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

        if(dir.magnitude <= distanceThisFrame)
        {
            hitTarget();
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

    }

    void hitTarget()
    {
        Destroy(gameObject);

        //TODO: Add Damage to Enemy Hit

    }
}