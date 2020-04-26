using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class KeyPressedEventArgs : EventArgs
{
    public char key { get; set; }
    public bool isCorrect { get; set; }
}

public class InputHandler : MonoBehaviour
{

    public Text inputBuffer;
    int inputBufferLength;
    public static event EventHandler<KeyPressedEventArgs> KeyPressed;

    protected virtual void OnKeyPressed(KeyPressedEventArgs e)
    {
        if (KeyPressed != null)
        {
            KeyPressed(this, e);
        }
    }


    public void ResetBuffer()
    {
        inputBuffer.text = "";
    }

    void GetInput()
    {
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
                        // Spawner.enemyList.RemoveAt(i);
                        Destroy(e.gameObject);
                        ResetBuffer();
                    }
                }
            }

            OnKeyPressed(ev); // send event
        }

        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.Backspace))
        {
            ResetBuffer();
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
    }
}
