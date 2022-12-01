using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCombat : MonoBehaviour
{

    public float range = 15f;
    float nextTimeToFire = 0f;
    float fireRate = 1f;

    public Transform target;

    public GameObject bulletPrefab;
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemey = null;
        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemey = enemy;
            }
        }

        if(nearestEnemey != null && shortestDistance <= range)
        {
            target = nearestEnemey.transform;
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTarget();

        if (target == null)
        {
            return;
        }

        Vector2 dir = target.position - transform.position;

        if (nextTimeToFire <= 0)
        {
            shoot();
            nextTimeToFire = 1f / fireRate;
        }

        nextTimeToFire -= Time.deltaTime;
    }

    void shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if(bullet != null)
        {
            bullet.seek(target);
        }
    }
}
