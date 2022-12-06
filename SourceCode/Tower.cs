using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        damage = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
