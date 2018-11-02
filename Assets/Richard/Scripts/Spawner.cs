using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public int maxEnemies;
    int enemyCount = 0;
    int spawnCounter = 0;
    public GameObject fox, wolf;
	
	// Update is called once per frame
	void Update () {
        if (enemyCount >= maxEnemies) return;
        if (spawnCounter % 100 == 0)
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                Instantiate(fox, transform);
            }
            else if (rand == 1)
            {
                Instantiate(wolf, transform);
            }
            enemyCount++;
        }
        spawnCounter++;
	}
}
