using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WordGenerator : MonoBehaviour {

    public static string[] words;

    void Awake() {
        words = Resources.Load<TextAsset>("sample_dict").text.Split('\n');
    }

    public static string GetRandomWord() {
        return words[Random.Range(0, words.Length)].Trim();
    }
}
