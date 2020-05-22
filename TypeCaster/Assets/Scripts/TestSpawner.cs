﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestSpawner : MonoBehaviour {

    public List<GameObject> standardEnemyPrefabs;

    void Start() {
        SpawnSixEnemies();
        // SpawnOverlappers();
    }

    void SpawnTwoEnemies() {
        GameObject toSpawn = standardEnemyPrefabs[Random.Range(0, standardEnemyPrefabs.Count)];
        GameObject g = (GameObject)Instantiate(toSpawn, new Vector3(-3, -1, -6), Quaternion.identity);
        Targetable t = g.GetComponent<Targetable>();

        toSpawn = standardEnemyPrefabs[Random.Range(0, standardEnemyPrefabs.Count)];
        g = (GameObject)Instantiate(toSpawn, new Vector3(3, -1, -6), Quaternion.identity);
        t = g.GetComponent<Targetable>();
    }

    void SpawnSixEnemies() {
        GameObject toSpawn;
        GameObject g;
        Targetable t;

        toSpawn = standardEnemyPrefabs[Random.Range(0, standardEnemyPrefabs.Count)];
        g = (GameObject)Instantiate(toSpawn, new Vector3(-3, -1, -6), Quaternion.identity);
        t = g.GetComponent<Targetable>();

        toSpawn = standardEnemyPrefabs[Random.Range(0, standardEnemyPrefabs.Count)];
        g = (GameObject)Instantiate(toSpawn, new Vector3(3, -1, -6), Quaternion.identity);
        t = g.GetComponent<Targetable>();

        toSpawn = standardEnemyPrefabs[Random.Range(0, standardEnemyPrefabs.Count)];
        g = (GameObject)Instantiate(toSpawn, new Vector3(-3, -1, -3), Quaternion.identity);
        t = g.GetComponent<Targetable>();

        toSpawn = standardEnemyPrefabs[Random.Range(0, standardEnemyPrefabs.Count)];
        g = (GameObject)Instantiate(toSpawn, new Vector3(3, -1, -3), Quaternion.identity);
        t = g.GetComponent<Targetable>();

        toSpawn = standardEnemyPrefabs[Random.Range(0, standardEnemyPrefabs.Count)];
        g = (GameObject)Instantiate(toSpawn, new Vector3(-3, -1, 0), Quaternion.identity);
        t = g.GetComponent<Targetable>();

        toSpawn = standardEnemyPrefabs[Random.Range(0, standardEnemyPrefabs.Count)];
        g = (GameObject)Instantiate(toSpawn, new Vector3(3, -1, 0), Quaternion.identity);
        t = g.GetComponent<Targetable>();
    }

    void SpawnOverlappers() {

        GameObject toSpawn;
        GameObject g;
        Targetable t;

        toSpawn = standardEnemyPrefabs[Random.Range(0, standardEnemyPrefabs.Count)];
        g = (GameObject)Instantiate(toSpawn, new Vector3(-3, -1, 0), Quaternion.identity);
        t = g.GetComponent<Targetable>();

        toSpawn = standardEnemyPrefabs[Random.Range(0, standardEnemyPrefabs.Count)];
        g = (GameObject)Instantiate(toSpawn, new Vector3(-3, -1, -3), Quaternion.identity);
        t = g.GetComponent<Targetable>();

        toSpawn = standardEnemyPrefabs[Random.Range(0, standardEnemyPrefabs.Count)];
        g = (GameObject)Instantiate(toSpawn, new Vector3(-3, -1, -6), Quaternion.identity);
        t = g.GetComponent<Targetable>();
    }
}