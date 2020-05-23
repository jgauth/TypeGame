using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnemy : MonoBehaviour {

    public ParticleSystem deathParticleEffect;
    public AudioClip deathSound;
    
    Targetable targetable;

    void Awake() {
        targetable = gameObject.GetComponent<Targetable>();
        targetable.TargetableWordCompleted += c_TargetableWordCompleted;
    }

    private void c_TargetableWordCompleted(object sender, TargetableWordCompletedEventArgs args) {
        // Need to set particle "stop action" to "destroy" in the editor so that it will be automatically destroyed when finished playing
        ParticleSystem ps = Instantiate(deathParticleEffect, transform.position, Quaternion.identity);

        // this function spawns a gameobject at transform.position to play the sound, then destroys the gameObject
        AudioSource.PlayClipAtPoint(deathSound, transform.position, 0.3f);
        Destroy(gameObject);
    }
    
    private void OnDisable() {
        targetable.TargetableWordCompleted -= c_TargetableWordCompleted;
    }
}
