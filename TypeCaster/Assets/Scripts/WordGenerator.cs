using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WordGenerator : MonoBehaviour
{

    public static string[] easyWords;
    public static string[] mediumWords;
    public static string[] bossWords;

    void Awake()
    {
        easyWords = Resources.Load<TextAsset>("easy").text.Split('\n');
        mediumWords = Resources.Load<TextAsset>("medium").text.Split('\n');
        bossWords = Resources.Load<TextAsset>("boss").text.Split('\n');
    }

    public static string GetRandomWord(string difficulty)
    {
        string word;
        switch (difficulty)
        {
            case "easy":
                word = easyWords[Random.Range(0, easyWords.Length)].Trim();
                break;
            case "medium":
                word = mediumWords[Random.Range(0, mediumWords.Length)].Trim();
                break;
            case "boss":
                word = bossWords[Random.Range(0, bossWords.Length)].Trim();
                break;
            default:
                word = easyWords[Random.Range(0, easyWords.Length)].Trim();
                break;
        }
        return word;
    }
}
