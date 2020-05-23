using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {

    // This script just makes it so that the background music object is
    // not destroyed when the scene is reloaded or changes

    private static BackgroundMusic instance;

    void Awake() {
        if (instance) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
