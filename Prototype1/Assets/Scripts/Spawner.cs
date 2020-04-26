using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public static List<Enemy> enemyList = new List<Enemy>();

    public GameObject enemyPrefab;


    void SpawnEnemyRandomLocation()
    {
        float xPos = Random.Range(-4.0f, 4.0f);
        float zPos = Random.Range(-4.0f, 4.0f);

        GameObject g = (GameObject)Instantiate(enemyPrefab, new Vector3(xPos, 1, zPos), Quaternion.identity);
        Enemy e = g.GetComponent<Enemy>();
        enemyList.Add(e);
    }

    void SpawnWordHighlightingTestEnemies()
    {
        float xPos = Random.Range(-4.0f, 4.0f);
        float zPos = Random.Range(-4.0f, 4.0f);

        GameObject g1 = (GameObject)Instantiate(enemyPrefab, new Vector3(xPos, 1, zPos), Quaternion.identity);
        Enemy e1 = g1.GetComponent<Enemy>();
        e1.SetWord("aaaa");
        enemyList.Add(e1);

        xPos = Random.Range(-4.0f, 4.0f);
        zPos = Random.Range(-4.0f, 4.0f);

        GameObject g2 = (GameObject)Instantiate(enemyPrefab, new Vector3(xPos, 1, zPos), Quaternion.identity);
        Enemy e2 = g2.GetComponent<Enemy>();
        e2.SetWord("aab");
        enemyList.Add(e2);

        xPos = Random.Range(-4.0f, 4.0f);
        zPos = Random.Range(-4.0f, 4.0f);

        GameObject g3 = (GameObject)Instantiate(enemyPrefab, new Vector3(xPos, 1, zPos), Quaternion.identity);
        Enemy e3 = g3.GetComponent<Enemy>();
        e3.SetWord("ac");
        enemyList.Add(e3);

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
