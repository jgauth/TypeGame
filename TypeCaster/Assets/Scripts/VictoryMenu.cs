using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour {
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public AudioSource levelMusic;
    public AudioSource victorySound;

    float m_Timer;
    bool m_IsPlayerAtExit = false;

    public void PlayerWins() {
        m_IsPlayerAtExit = true;
        levelMusic.Stop();
        victorySound.Play();
    }

    void Update() {
        if (m_IsPlayerAtExit)
        {
            EndLevel();
        }

    }

    void EndLevel() {
        if (Input.GetKeyDown(KeyCode.S)) {
            // Get the next level
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (Input.GetKeyDown(KeyCode.M)) {
            SceneManager.LoadScene("Menu");
        }
        m_Timer += Time.deltaTime;
        exitBackgroundImageCanvasGroup.alpha = m_Timer / fadeDuration;
    }
}
