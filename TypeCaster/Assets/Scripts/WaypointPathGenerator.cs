using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class WaypointPathGenerator : MonoBehaviour {

    // public Transform[] waypoints;
    public Transform waypointHolder;

    // Transform[] waypoints;
    List<Transform> waypoints = new List<Transform>();

    void Start() {
        foreach (Transform t in waypointHolder) {
            waypoints.Add(t);
        }

        if (waypoints.Count > 0) {
            // Create a new bezier path from the waypoints.
            BezierPath bezierPath = new BezierPath(waypoints, false, PathSpace.xyz);
            bezierPath.GlobalNormalsAngle = 90f;
            GetComponent<PathCreator>().bezierPath = bezierPath;
        }
    }
}
