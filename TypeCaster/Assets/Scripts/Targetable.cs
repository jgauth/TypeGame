using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TargetableAddedEventArgs : EventArgs {
    public Targetable targetableAdded;
}
public class TargetableRemovedEventArgs : EventArgs {
    public Targetable targetableRemoved;
}
public class TargetableTextChangedEventArgs : EventArgs {
    public string newText;
}
public class TargetableWordCompletedEventArgs : EventArgs {

}


public class Targetable : MonoBehaviour {

    // ///////////public float verticalOffset;
    public string hexTextHighlightColor;

    // static list of all targetables
    public static List<Targetable> targetableList = new List<Targetable>();

    // static events
    public static event EventHandler<TargetableAddedEventArgs> TargetableAdded;
    public static event EventHandler<TargetableRemovedEventArgs> TargetableRemoved;

    // Event specific to each instance of Targetable
    public event EventHandler<TargetableTextChangedEventArgs> TargetableTextChanged;
    public event EventHandler<TargetableWordCompletedEventArgs> TargetableWordCompleted;

    // word that if typed, destroys the attached gameObject
    private string killWord;
    
    // word displayed in canvas above targetable, with highlighting
    private string displayWord;

    public string GetKillWord() {
        return killWord;
    }

    public void SetKillWord(string newWord) {
        killWord = newWord;
        UpdateDisplayWord(newWord);
    }

    private void UpdateDisplayWord(string newDisplayWord) {
        displayWord = newDisplayWord;

        TargetableTextChangedEventArgs args = new TargetableTextChangedEventArgs();
        args.newText = newDisplayWord;
        OnTargetableTextChanged(args);
    }

    public bool CheckSubstringMatch(string input) {
        // Return true if input is substring of word starting at index 0
        // Return false otherwise
        
        // also update display word highlighting

        // input cannot be longer than killWord && substring matches
        if (input.Length <= killWord.Length && input.Equals(killWord.Substring(0, input.Length))) {

            string highlightedText = $"<color={hexTextHighlightColor}>{killWord.Substring(0, input.Length)}</color>{killWord.Substring(input.Length, killWord.Length - input.Length)}";
            UpdateDisplayWord(highlightedText); // substring matches to update highlighting
            return true;
        }
        else {
            UpdateDisplayWord(killWord); // substring doesn't match so reset highlighting
            return false;
        }
    }

    public bool CheckCompleteMatch(string input) {
        // Return true if input exactly matches killWord
        if (input.Equals(killWord)) {
            OnTargetableWordCompleted(new TargetableWordCompletedEventArgs()); // trigger word completed event
            return true;
        }

        return false;
    }

    private void Start() {
        SetKillWord(WordGenerator.GetRandomWord());
    }

    private void OnEnable() {
        targetableList.Add(this);

        TargetableAddedEventArgs args = new TargetableAddedEventArgs();
        args.targetableAdded = this;

        OnTargetableAdded(args);
    }

    private void OnDisable() {
        targetableList.Remove(this);

        TargetableRemovedEventArgs args = new TargetableRemovedEventArgs();
        args.targetableRemoved = this;
        
        OnTargetableRemoved(args);
    }


    // OnEvents
    protected virtual void OnTargetableRemoved(TargetableRemovedEventArgs e) {
        if (TargetableRemoved != null) {
            TargetableRemoved(this, e);
        }
    }
    protected virtual void OnTargetableAdded(TargetableAddedEventArgs e) {
        if (TargetableAdded != null) {
            TargetableAdded(this, e);
        }
    }
    protected virtual void OnTargetableTextChanged(TargetableTextChangedEventArgs e) {
        if (TargetableTextChanged != null) {
            TargetableTextChanged(this, e);
        }
    }
    protected virtual void OnTargetableWordCompleted(TargetableWordCompletedEventArgs e) {
        if (TargetableWordCompleted != null) {
            TargetableWordCompleted(this, e);
        }
    }
    
}
