using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject startScreen;
    public GameObject levelScreen;

    void PlayGame (string level) {
        switch (level) {
            case "1":
				SceneManager.LoadScene("Level1");
                break;
            case "2":
				SceneManager.LoadScene("Level2");
                break;
            case "3":
				SceneManager.LoadScene("Level3");
                break;
            default:
                SceneManager.LoadScene("Menu");
                break;
		}
    }

    void Start () {
        startScreen.SetActive(true);
        levelScreen.SetActive(false);
    }

    void Update () {
        if (startScreen.activeSelf) {
			if (Input.GetKeyDown(KeyCode.S)) {
				startScreen.SetActive(false);
				levelScreen.SetActive(true);
			}
		} else if (levelScreen.activeSelf) {
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                PlayGame("1");
            } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
                PlayGame("2");
            } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
                PlayGame("3");
            } else if (Input.GetKeyDown(KeyCode.F2)) { 
				levelScreen.SetActive(false);
				startScreen.SetActive(true);
            }
        }
    }
}