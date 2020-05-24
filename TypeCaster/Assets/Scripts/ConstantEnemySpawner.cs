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
        Debug.Log("STARTIN COROUTINE");
        StartCoroutine(EnemySpawner());
    }
    //Vector3 newPos= myPos + Quaternion.AngleAxis(RandomAngle, playerUpVector) * playerLocalDirection * DistanceFromPlayer;
    void SpawnInFrontOfPlayer() {
        float distance = Random.Range(minSpawnDistance, maxSpawnDistance); // get random distance 
        GameObject enemyPrefab = standardEnemyPrefabs[Random.Range(0, standardEnemyPrefabs.Count)];
        Vector3 enemyPos = player.position + Quaternion.AngleAxis(Random.Range(-20, 20), player.up) * player.forward * distance;
        enemyPos.y = 0;

        Instantiate(enemyPrefab, enemyPos, Quaternion.identity, enemyHolder);

        // Quaternion randomAngle = Quaternion.Euler(0, Random.Range(-20, 20), 0) * player.rotation;

        // Vector3 enemyPos = player.position + (randomAngle * player.forward);
        // enemyPos.y = 0f; // place enemy on ground - TODO probably a better way to do this. Raycast to ground?

        // GameObject enemyPrefab = standardEnemyPrefabs[Random.Range(0, standardEnemyPrefabs.Count)];
        // Instantiate(enemyPrefab, enemyPos, Quaternion.identity, enemyHolder);

    }

    IEnumerator EnemySpawner() {
        while (true) {
            if (InputHandler.gameStarted) {
                Debug.Log("SPAWNING");
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
