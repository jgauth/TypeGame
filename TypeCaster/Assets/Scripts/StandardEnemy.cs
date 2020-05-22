using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnemy : MonoBehaviour {

    Targetable targetable;

    void Awake() {
        targetable = gameObject.GetComponent<Targetable>();
        targetable.TargetableWordCompleted += c_TargetableWordCompleted;
    }

    private void c_TargetableWordCompleted(object sender, TargetableWordCompletedEventArgs args) {
        Destroy(gameObject);
    }
    
    private void OnDisable() {
        targetable.TargetableWordCompleted -= c_TargetableWordCompleted;
    }
}
