using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour {
    public GameObject victoryScreen;

    void Update () {
        if (Input.GetKeyDown(KeyCode.S)) {
            // Temporary
            SceneManager.LoadScene("Menu");
        }
        if (Input.GetKeyDown(KeyCode.M)) {
            SceneManager.LoadScene("Menu");
        }
    }
}
