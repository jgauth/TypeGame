using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using Random = UnityEngine.Random;

public class EnemyAddedEventArgs : EventArgs
{
    public Enemy enemyAdded;
}

public class EnemyRemovedEventArgs : EventArgs
{
    public Enemy enemyRemoved;
}

public class EnemyTextChangedEventArgs : EventArgs
{
    public string newText;
}

public class Enemy : MonoBehaviour
{

    // set in editor
    public float verticalOffset;
    public string hexTextHighlightColor;

    // list of enemies
    public static List<Enemy> enemyList = new List<Enemy>();

    // static events
    public static event EventHandler<EnemyAddedEventArgs> EnemyAdded;
    public static event EventHandler<EnemyRemovedEventArgs> EnemyRemoved;

    // non-static events
    public event EventHandler<EnemyTextChangedEventArgs> EnemyTextChanged;

    // enemy kill word
    private string word;

    public string GetWord()
    {
        return word;
    }

    public void SetWord(string newWord)
    {
        word = newWord;
        ChangeText(word);
    }

    // send text changed event
    private void ChangeText(string newText)
    {
        EnemyTextChangedEventArgs args = new EnemyTextChangedEventArgs();
        args.newText = newText;
        OnEnemyTextChanged(args);
    }

    public bool CheckSubstringMatch(string input)
    {
        // Return true if input is substring of word starting at index 0
        // Return false otherwise
        
        // also updates in word highlighting

        if (input.Length <= word.Length && input.Equals(word.Substring(0, input.Length))) // input cannot be longer than word && substring matches
        {
            // update display text color
            string highlightedText = $"<color={hexTextHighlightColor}>{word.Substring(0, input.Length)}</color>{word.Substring(input.Length, word.Length - input.Length)}";
            ChangeText(highlightedText);

            return true;
        }
        else
        {
            ChangeText(word);
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

        SetWord(words[Random.Range(0, words.Length)].Trim());
    }

    private void OnEnable()
    {
        enemyList.Add(this);

        EnemyAddedEventArgs args = new EnemyAddedEventArgs();
        args.enemyAdded = this;

        OnEnemyAdded(args);

        Debug.Log("Inside enemy OnEnable() - called OnEnemyAdded()");
    }

    void OnDisable()
    {
        enemyList.Remove(this);

        EnemyRemovedEventArgs args = new EnemyRemovedEventArgs();
        args.enemyRemoved = this;
        OnEnemyRemoved(args);
    }


    // static events
    protected virtual void OnEnemyAdded(EnemyAddedEventArgs e)
    {
        if (EnemyAdded != null)
        {
            Debug.Log("Enemy triggering EnemyAdded event from OnEnemyAdded()");
            EnemyAdded(this, e);
        }
    }

    protected virtual void OnEnemyRemoved(EnemyRemovedEventArgs e)
    {
        if (EnemyRemoved != null)
        {
            EnemyRemoved(this, e);
        }
    }

    // non-static event
    protected virtual void OnEnemyTextChanged(EnemyTextChangedEventArgs e)
    {
        if (EnemyTextChanged != null)
        {
            EnemyTextChanged(this, e);
        }
    }
}
