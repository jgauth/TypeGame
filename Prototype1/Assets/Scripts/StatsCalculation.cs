using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsCalculation : MonoBehaviour
{

    void c_KeyPressed(object sender, KeyPressedEventArgs e)
    {
        if (e.key == '\b')
        {
            Debug.Log("received backspace");
        }
        else
        {
            Debug.Log("Received key press: " + e.key + " -- Correct: " + e.isCorrect);
        }
    }

    void Start()
    {
        InputHandler.KeyPressed += c_KeyPressed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
