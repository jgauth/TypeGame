using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatMenu : MonoBehaviour {

    public GameObject defeatScreen;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            // Temporary
            SceneManager.LoadScene("Menu");
        }
        if (Input.GetKeyDown(KeyCode.M)) {
            SceneManager.LoadScene("Menu");
        }
    }
}
