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

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            SpawnEnemyRandomLocation();
        }
    }
}
