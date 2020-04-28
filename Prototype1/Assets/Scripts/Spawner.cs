using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    void SpawnEnemyRandomLocation()
    {
        float xPos = Random.Range(-4.0f, 4.0f);
        float zPos = Random.Range(-4.0f, 4.0f);

        GameObject g = (GameObject)Instantiate(enemyPrefab, new Vector3(xPos, 1, zPos), Quaternion.identity);
    }

    void SpawnWordHighlightingTestEnemies()
    {
        float xPos = Random.Range(-4.0f, 4.0f);
        float zPos = Random.Range(-4.0f, 4.0f);

        GameObject g1 = (GameObject)Instantiate(enemyPrefab, new Vector3(xPos, 1, zPos), Quaternion.identity);
        Enemy e1 = g1.GetComponent<Enemy>();
        e1.SetWord("aaaa");

        xPos = Random.Range(-4.0f, 4.0f);
        zPos = Random.Range(-4.0f, 4.0f);

        GameObject g2 = (GameObject)Instantiate(enemyPrefab, new Vector3(xPos, 1, zPos), Quaternion.identity);
        Enemy e2 = g2.GetComponent<Enemy>();
        e2.SetWord("aab");

        xPos = Random.Range(-4.0f, 4.0f);
        zPos = Random.Range(-4.0f, 4.0f);

        GameObject g3 = (GameObject)Instantiate(enemyPrefab, new Vector3(xPos, 1, zPos), Quaternion.identity);
        Enemy e3 = g3.GetComponent<Enemy>();
        e3.SetWord("ac");

    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            SpawnEnemyRandomLocation();
        }

        //SpawnWordHighlightingTestEnemies();
    }
}
