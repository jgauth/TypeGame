﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class StatsCalculation : MonoBehaviour
{

    public TextMeshProUGUI accuracyText;
    public TextMeshProUGUI WPMText;
    public TextMeshProUGUI timerText;

    // accuracy variables
    private int totalKeyPresses = 0;
    private int totalCorrectKeyPresses = 0;
    private float accuracy;

    // wpm variables
    private int lettersCompleted = 0;
    private float timer = 0.0f;
    private float wpm = 0;

    void c_KeyPressed(object sender, KeyPressedEventArgs e)
    {
        if (e.key == '\b')
        {
            Debug.Log("received backspace: " + (int)e.key);
        }
        else if ((int)e.key == 127)
        {
            Debug.Log("Received ASCII DEL: " + (int)e.key);
        }
        else
        {
            Debug.Log("Received key press: " + (int)e.key + " -- Correct: " + e.isCorrect);
            totalKeyPresses += 1;
            if (e.isCorrect) totalCorrectKeyPresses += 1;
            accuracy = ((float)totalCorrectKeyPresses / (float)totalKeyPresses) * 100f;
        }
    }

    void c_WordCompleted(object sender, WordCompletedEventArgs e)
    {
        lettersCompleted += e.wordLength;
        wpm = (lettersCompleted / 5.0f) / (timer / 60.0f);
        Debug.Log("Word Completed length: " + e.wordLength);
    }

    private void Update()
    {
        if (InputHandler.gameStarted)
            timer += Time.deltaTime;
    }

    void Start()
    {
        InputHandler.KeyPressed += c_KeyPressed;
        InputHandler.WordCompleted += c_WordCompleted;
    }

    private void OnGUI()
    {
        timerText.text = string.Format("{0:N2}", timer);
        accuracyText.text = "Accuracy: " + Math.Round(accuracy, 2) + "%";
        WPMText.text = "WPM: " + Math.Round(wpm, 1);
    }
}
