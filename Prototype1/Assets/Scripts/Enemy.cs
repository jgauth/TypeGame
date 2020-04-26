using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{

    // public Text text;
    public TextMeshProUGUI text;
    public Transform label;
    public float verticalOffset;

    string word;

    public string GetWord()
    {
        return word;
    }

    public bool CheckSubstringMatch(string input)
    {
        // Return true if input is substring of word starting at index 0
        // Return false otherwise

        if (input.Length <= word.Length) // input cannot be longer than word
        {
            return input.Equals(word.Substring(0, input.Length));
        }
        else
        {
            return false;
        }
        
    }

    public bool CheckCompleteMatch(string input)
    {
        return input.Equals(word);
    }    

    void Awake()
    {
        string[] words = Resources.Load<TextAsset>("sample_dict").text.Split('\n');

        word = words[Random.Range(0, words.Length)];

        text.text = word;
    }

    void Update()
    {
        label.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * verticalOffset);
    }

    void OnDisable()
    {
        Spawner.enemyList.Remove(this);
    }
}
