using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatMenu : MonoBehaviour {
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public AudioSource levelMusic;
    public AudioSource defeatSound;

    float m_Timer;
    bool m_IsPlayerDead = false;

    public void PlayerDies() {
        m_IsPlayerDead = true;
        levelMusic.Stop();
        defeatSound.Play();
    }

    void Update() {
        if (m_IsPlayerDead)
        {
            EndLevel();
        }
    }

    void EndLevel() {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Restart the level
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("Menu");
        }
        m_Timer += Time.deltaTime;
        exitBackgroundImageCanvasGroup.alpha = m_Timer / fadeDuration;
    }
}
