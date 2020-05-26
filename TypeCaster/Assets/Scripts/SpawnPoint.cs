using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public List<GameObject> enemyPrefabs;

    public float spawnRadius;
    public float spawnNum;
    public string EnemyHolderName;
    public string PlayerGameObjectName;

    GameObject enemyHolder;
    GameObject player;

    void Start() {
        enemyHolder = GameObject.Find(EnemyHolderName);
        player = GameObject.Find(PlayerGameObjectName);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == player) {
            for (int i = 0; i < spawnNum; i++) {
                Vector3 spawnPos = Random.insideUnitSphere * spawnRadius + transform.position;
                spawnPos.y = 0;
                GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
                Instantiate(enemyPrefab, spawnPos, Quaternion.identity, enemyHolder.transform);
            }
        }
    }
}