using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class KeyPressedEventArgs : EventArgs
{
    public char key { get; set; }
    public bool isCorrect { get; set; }
}
public class WordCompletedEventArgs : EventArgs
{
    public int wordLength { get; set; }
}

public class InputHandler : MonoBehaviour
{

    public Text inputBuffer;
    int inputBufferLength;
    public static event EventHandler<KeyPressedEventArgs> KeyPressed;
    public static event EventHandler<WordCompletedEventArgs> WordCompleted;

    protected virtual void OnKeyPressed(KeyPressedEventArgs e)
    {
        if (KeyPressed != null)
        {
            KeyPressed(this, e);
        }
    }
    protected virtual void OnWordCompleted(WordCompletedEventArgs e)
    {
        if (WordCompleted != null)
        {
            WordCompleted(this, e);
        }
    }


    public void ResetBuffer()
    {
        inputBuffer.text = "";
    }

    void GetInput()
    {
        // Debug.Log("Current buffer: [" + inputBuffer.text + "]");

        // https://docs.unity3d.com/ScriptReference/Input-inputString.html

        foreach (char c in Input.inputString)
        {
            if (c == '\b') // if backspace is pressed
            {
                if (inputBuffer.text.Length != 0)
                {
                    inputBuffer.text = inputBuffer.text.Substring(0, inputBuffer.text.Length - 1);
                }
            }
            else if ((c == '\n') || (c == '\r')) // if enter/return
            {
                continue; // ignore and read next char
            }
            else
            {
                inputBuffer.text += c;
            }

            
            KeyPressedEventArgs ev = new KeyPressedEventArgs();
            ev.key = c;
            ev.isCorrect = false;

            for (int i=Spawner.enemyList.Count - 1; i>=0; i--)
            {
                Enemy e = Spawner.enemyList[i];
                if (e.CheckSubstringMatch(inputBuffer.text))
                {
                    ev.isCorrect = true;
                    if (e.CheckCompleteMatch(inputBuffer.text))
                    {
                        // trigger WordCompleted event
                        WordCompletedEventArgs wc = new WordCompletedEventArgs();
                        wc.wordLength = inputBuffer.text.Length;
                        OnWordCompleted(wc);

                        Destroy(e.gameObject);
                        ResetBuffer();
                    }
                }
            }

            OnKeyPressed(ev); // send event
        }
    }

    void Awake()
    {
        ResetBuffer();
    }

    void Update()
    {
        // Debug.Log(Spawner.enemyList.Count);
        // Debug.Log(input)
        GetInput();

        // Press F11 to toggle fullscreen
        if (Input.GetKeyDown(KeyCode.F11))
        {
            Debug.Log("F11 Pressed");
            Screen.fullScreen = !Screen.fullScreen;
        }

        // Press F1 to reload scene
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log("F1 Pressed");
            SceneManager.LoadSceneAsync(0);
        }

        // this must go after Input.inputString loop
        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.Backspace))
        {
            ResetBuffer();
        }
    }
}
