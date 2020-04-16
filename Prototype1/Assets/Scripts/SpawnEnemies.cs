using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{

    public GameObject enemy;
    
    void Start()
    {

        for (int i=0; i<10; i++)
        {
            float xPos = Random.Range(-4.0f, 4.0f);
            float zPos = Random.Range(-4.0f, 4.0f);

            Instantiate(enemy, new Vector3(xPos, 1, zPos), Quaternion.identity);
        }
        
    }

}
