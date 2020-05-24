using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PlayerMovement : MonoBehaviour {

    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public float speed = 5;
    float distanceTravelled;

    void Update() {
        if (pathCreator && InputHandler.gameStarted) {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        }
    }
}
