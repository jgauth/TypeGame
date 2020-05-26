using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnemy : MonoBehaviour
{

    public ParticleSystem deathParticleEffect;
    public AudioClip deathSound;
    public float speed;
    public string PlayerGameObjectName;


    Targetable targetable;
    GameObject player;
    bool inAttackRange;

    void Awake()
    {
        targetable = gameObject.GetComponent<Targetable>();
        targetable.TargetableWordCompleted += c_TargetableWordCompleted;
    }

    private void Start()
    {
        player = GameObject.Find(PlayerGameObjectName);
    }

    private void Update()
    {
        if (InputHandler.gameStarted && !inAttackRange)
        {
            // move towards player
            float step = speed * Time.deltaTime;
            Vector3 newPosition = player.transform.position;
            newPosition.y = transform.position.y;

            transform.position = Vector3.MoveTowards(transform.position, newPosition, step);

            // rotate towards player
            Vector3 relativePos = player.transform.position - transform.position;
            relativePos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            inAttackRange = true;
        }
    }

    private void c_TargetableWordCompleted(object sender, TargetableWordCompletedEventArgs args)
    {
        // Need to set particle "stop action" to "destroy" in the editor so that it will be automatically destroyed when finished playing
        ParticleSystem ps = Instantiate(deathParticleEffect, transform.position, Quaternion.identity);

        // this function spawns a gameobject at transform.position to play the sound, then destroys the gameObject
        AudioSource.PlayClipAtPoint(deathSound, transform.position, 0.3f);
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        targetable.TargetableWordCompleted -= c_TargetableWordCompleted;
    }
}
