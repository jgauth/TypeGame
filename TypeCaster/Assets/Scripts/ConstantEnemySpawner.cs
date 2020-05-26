using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ConstantEnemySpawner : MonoBehaviour {

    public List<GameObject> standardEnemyPrefabs;
    public Transform enemyHolder;

    public Transform player;
    public float maxSpawnDistance;
    public float minSpawnDistance;
    public float spawnRate; // time inbetween spawns
    public float spawnNum; // number spawned

    void Start() {
        StartCoroutine(EnemySpawner());
    }

    void SpawnInFrontOfPlayer() {
        float distance = Random.Range(minSpawnDistance, maxSpawnDistance); // get random distance 
        GameObject enemyPrefab = standardEnemyPrefabs[Random.Range(0, standardEnemyPrefabs.Count)];
        Vector3 enemyPos = player.position + Quaternion.AngleAxis(Random.Range(-10, 10), player.up) * player.forward * distance;
        enemyPos.y = 0;

        Instantiate(enemyPrefab, enemyPos, Quaternion.identity, enemyHolder);
    }

    IEnumerator EnemySpawner() {
        while (true) {
            if (InputHandler.gameStarted) {
                for (int i = 0; i < spawnNum; i++) {
                    SpawnInFrontOfPlayer();
                }

                yield return new WaitForSeconds(spawnRate);
            } else {
                yield return new WaitUntil(() => InputHandler.gameStarted == true);
            }
        }
    }
}
