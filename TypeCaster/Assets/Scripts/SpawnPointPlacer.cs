using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[ExecuteInEditMode]
public class SpawnPointPlacer : MonoBehaviour {

    public GameObject spawnPointPrefab;
    public Transform holder;
    public PathCreator pathCreator;
    public float spacing = 3;

    const float minSpacing = .1f;

    private void Start() {
        if (pathCreator && spawnPointPrefab && holder) {
            DestroyObjects();

            VertexPath path = pathCreator.path;

            spacing = Mathf.Max(minSpacing, spacing);
            float distance = spacing * 3; // multiply by 2 so that first three are skipped

            while (distance < path.length) {
                Vector3 point = path.GetPointAtDistance(distance);
                Quaternion rotation = path.GetRotationAtDistance(distance);
                Instantiate(spawnPointPrefab, point, rotation, holder);
                distance += spacing;
            }
        }
    }

    // private void Update() {
    //     if (pathCreator && spawnPointPrefab && holder) {
    //         DestroyObjects();

    //         VertexPath path = pathCreator.path;

    //         spacing = Mathf.Max(minSpacing, spacing);
    //         float distance = spacing;

    //         while (distance < path.length) {
    //             Vector3 point = path.GetPointAtDistance(distance);
    //             Quaternion rotation = path.GetRotationAtDistance(distance);
    //             Instantiate(spawnPointPrefab, point, rotation, holder);
    //             distance += spacing;
    //         }
    //     }
    // }

    void DestroyObjects() {
        int numChildren = holder.childCount;
        for (int i = numChildren - 1; i >= 0; i--) {
            DestroyImmediate (holder.GetChild (i).gameObject, false);
        }
    }
}