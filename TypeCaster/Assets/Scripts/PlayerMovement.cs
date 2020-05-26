using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PlayerMovement : MonoBehaviour {

    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public float speed = 5;
    public float stopPlayerMovementDistance;

    // bool isPlayerMoving;
    float distanceTravelled;

    void Update() {
        if (InputHandler.gameStarted) {
            
            foreach (Targetable t in Targetable.targetableList) {
                if (t.gameObject.tag == "Enemy" && Vector3.Distance(transform.position, t.transform.position) <= stopPlayerMovementDistance) {
                    // isPlayerMoving = false;
                    return;
                }
            }
            // isPlayerMoving = true;
            MoveAlongPath();
        }        
    }

    void MoveAlongPath() {
        if (pathCreator) {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        }
    }
}
