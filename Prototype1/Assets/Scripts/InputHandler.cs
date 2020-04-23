using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{

    public Text inputBuffer;
    int inputBufferLength;


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

            // Check and update enemies
            bool correctKeystroke = false; // to be used in accuracy calculation
            foreach (Enemy e in Spawner.enemyList)
            {
                if (e.CheckSubstringMatch(inputBuffer.text))
                {
                    correctKeystroke = true;
                    if (e.CheckCompleteMatch(inputBuffer.text))
                    {
                        Destroy(e.gameObject);
                        ResetBuffer();
                    }
                }
            }
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
