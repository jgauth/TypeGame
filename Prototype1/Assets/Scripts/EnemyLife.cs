using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EnemyLife : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string[] words = File.ReadAllLines("Assets/sample_dict.txt");

        string word = words[Random.Range(0, words.Length)];

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
