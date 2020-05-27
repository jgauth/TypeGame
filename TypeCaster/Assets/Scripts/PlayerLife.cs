using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour {

    public float health = 100f;
    public TextMeshProUGUI healthText;

    void Start() {
        UpdateHealthText();
    }

    void Update() {
        
    }

    void UpdateHealthText() {
        healthText.text = $"HP: {health}";
    }

    public void receiveDamage(float damage) {
        health -= damage;
        UpdateHealthText();
        // could play a damage sound here
        if (health <= 0) {
            Die();
        }
    }

    void Die() {
        // end game
        // play sound
        // show level failed screen
        InputHandler.gameStarted = false;
        SceneManager.LoadScene("Defeat");
    }
}
