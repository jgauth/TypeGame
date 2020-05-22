using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

// object passed for KeyPressed Events
public class KeyPressedEventArgs : EventArgs {
    public char key { get; set; }
    public bool isCorrect { get; set; }
}
public class WordCompletedEventArgs : EventArgs {
    public int wordLength { get; set; }
}

public class InputHandler : MonoBehaviour {

    public Text inputBuffer;
    public static event EventHandler<KeyPressedEventArgs> KeyPressed; // event triggered on every individual key press
    public static event EventHandler<WordCompletedEventArgs> WordCompleted; // event triggered when a single word is completed

    public static bool gameStarted = false; // this should probably be moved to its own GameState class 

    // Reset current buffer to empty string
    public void ResetBuffer() {
        inputBuffer.text = "";
    }
    
    private void GetMainInput() {

        // Main input loop
        foreach (char c in Input.inputString) {

            if (c == '\b') {
                if (inputBuffer.text.Length != 0) {
                    inputBuffer.text = inputBuffer.text.Substring(0, inputBuffer.text.Length - 1); // backspace
                }
            }

            else if ((c == '\n') || (c == '\r') || (c == ' ')) {
                // ignore enter/return/space
                continue;
            }

            else {
                // all other characters get added to the input buffer
                inputBuffer.text += c;
                gameStarted = true; // start the game on first keypress
            }

            KeyPressedEventArgs e = new KeyPressedEventArgs();
            e.key = c;
            e.isCorrect = false; 

            // iterate through list backwards so that items can be removed
            for (int i=Targetable.targetableList.Count - 1; i>=0; i--) {
                Targetable t = Targetable.targetableList[i];

                if (t.CheckSubstringMatch(inputBuffer.text)) {
                    e.isCorrect = true;

                    if (t.CheckCompleteMatch(inputBuffer.text)) {
                        
                        WordCompletedEventArgs wc = new WordCompletedEventArgs();
                        wc.wordLength = inputBuffer.text.Length;
                        OnWordCompleted(wc); // trigger global word completed event

                        ResetBuffer();
                    }
                }
            }

            OnKeyPressed(e); // trigger global key pressed event
        }
    }

    private void GetControlInput() {

        // get input that controls game ie F-keys, clear buffer

        // Ctrl + Backspace 
        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.Backspace))
        {
            ResetBuffer();
        }

        // Press F11 to toggle fullscreen
        if (Input.GetKeyDown(KeyCode.F11))
        {
            Screen.fullScreen = !Screen.fullScreen;
        }

        // Press F1 to reload scene
        if (Input.GetKeyDown(KeyCode.F1))
        {
            gameStarted = false;
            SceneManager.LoadScene(0);
        }
    }

    void Awake() {
        ResetBuffer();
    }

    // Update is called once per frame
    void Update() {
        GetMainInput();
        GetControlInput();
    }

    protected virtual void OnKeyPressed(KeyPressedEventArgs e) {
        if (KeyPressed != null) {
            KeyPressed(this, e);
        }
    }

    protected virtual void OnWordCompleted(WordCompletedEventArgs e) {
        if (WordCompleted != null) {
            WordCompleted(this, e);
        }
    }
}
