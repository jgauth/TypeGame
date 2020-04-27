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
    public string hexTextHighlightColor;

    string word;

    public string GetWord()
    {
        return word;
    }

    // set the enemies kill word
    public void SetWord(string newWord)
    {
        word = newWord;
        SetText(word);
    }

    // set the text the player actually sees
    private void SetText(string newText)
    {
        text.text = newText;
    }

    public bool CheckSubstringMatch(string input)
    {
        // Return true if input is substring of word starting at index 0
        // Return false otherwise
        
        // also updates in word highlighting

        if (input.Length <= word.Length && input.Equals(word.Substring(0, input.Length))) // input cannot be longer than word && substring matches
        {
            // update display text color
            SetText($"<color={hexTextHighlightColor}>{word.Substring(0, input.Length)}</color>{word.Substring(input.Length, word.Length-input.Length)}");

            return true;
        }
        else
        {
            SetText(word);
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

        word = words[Random.Range(0, words.Length)].Trim();

        SetText(word);
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
