using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public int enemyCount = 0;

    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine("spawnEnemies");
    }

    IEnumerator spawnEnemies()
    {
       // Set a limit to the loop to limit how many enemies spawn
        while(enemyCount < 5)
        {
            // wait in intervals before spawning next enemy via Instantiate (object, 2d vector location, quarternion.identity just sets the object to no rotation. A bit confusing, but it is necessary for unity to load the gameobject without error)
            yield return new WaitForSeconds(3);
            Instantiate(enemy, new Vector2(5.5f,2f), Quaternion.identity);
            enemyCount += 1;
        }
    }

}
