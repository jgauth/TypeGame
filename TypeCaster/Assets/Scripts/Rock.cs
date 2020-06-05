using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {

    public float speed;
    public string PlayerGameObjectName;
    public float damage;
    public AudioClip hitSound;

    GameObject player;

    void Start() {
        player = GameObject.Find(PlayerGameObjectName);
    }

    // Update is called once per frame
    void Update() {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);

        if (Vector3.Distance(transform.position, player.transform.position) < 0.001f) {
            PlayerLife p = player.GetComponent<PlayerLife>();
            p.receiveDamage(damage);
            AudioSource.PlayClipAtPoint(hitSound, transform.position, 0.3f);
            Destroy(this);
        }
    }
}