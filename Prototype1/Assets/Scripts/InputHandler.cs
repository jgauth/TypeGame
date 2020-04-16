using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{

    public Text inputBuffer;

    void Start()
    {
        inputBuffer.text = "";
    }

    // Update is called once per frame
    void Update()
    {

        // https://docs.unity3d.com/ScriptReference/Input-inputString.html

 
        foreach (char c in Input.inputString)
        {
            if (c == '\b') // has backspace/delete been pressed?
            {
                if (inputBuffer.text.Length != 0)
                {
                    inputBuffer.text = inputBuffer.text.Substring(0, inputBuffer.text.Length - 1);
                }
            }
            else if ((c == '\n') || (c == '\r')) // enter/return
            {

            }
            else
            {
                inputBuffer.text += c;
            }
        }

        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.Backspace))
        {
            inputBuffer.text = "";
        }
    }
}
