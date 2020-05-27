using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSpawner : MonoBehaviour {

    public GameObject spawneePrefab;
    public string PlayerGameObjectName;

    GameObject player;
    bool spawnComplete = false;

    void Start() {
        player = GameObject.Find(PlayerGameObjectName);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == player && !spawnComplete) {

            Vector3 spawnPos = transform.position;
            spawnPos.y = 0;
            Instantiate(spawneePrefab, spawnPos, Quaternion.identity);

            spawnComplete = true;
        }
    }
}
