using UnityEngine;
using UnityEngine.SceneManagement;

public class BossEnemy : MonoBehaviour {

    public ParticleSystem deathParticleEffect;
    public AudioClip deathSound;
    public float attackCooldown;
    public float attackDamage;
    public string PlayerGameObjectName;
    public bool finalEnemyInLevel;
    public VictoryMenu victoryMenu;


    Animator animator;
    Targetable targetable;
    GameObject player;

    float attackCooldownTimeLeft;


    void Awake() {
        animator = gameObject.GetComponent<Animator>();
        targetable = gameObject.GetComponent<Targetable>();
        targetable.TargetableWordCompleted += c_TargetableWordCompleted;

        attackCooldownTimeLeft = attackCooldown;
    }

    private void Start() {
        player = GameObject.Find(PlayerGameObjectName);
    }

    private void Update() {
        
        if (InputHandler.gameStarted) {
            attackCooldownTimeLeft -= Time.deltaTime;

            if (attackCooldownTimeLeft <= 0.0f) {
                Attack();
                attackCooldownTimeLeft = attackCooldown;
            }
        }
    }

    void Attack() {
        // animator.Play("Attack01");
    }

    // animation event
    public void AttackApex() {
        PlayerLife p = player.GetComponent<PlayerLife>();
        p.receiveDamage(attackDamage);
    }


    private void c_TargetableWordCompleted(object sender, TargetableWordCompletedEventArgs args) {
        // Need to set particle "stop action" to "destroy" in the editor so that it will be automatically destroyed when finished playing
        ParticleSystem ps = Instantiate(deathParticleEffect, transform.position, Quaternion.identity);

        // this function spawns a gameobject at transform.position to play the sound, then destroys the gameObject
        AudioSource.PlayClipAtPoint(deathSound, transform.position, 0.3f);
        Destroy(gameObject);

        if (finalEnemyInLevel) {
            Victory();
        }
    }

    void Victory() {
        // should show victory screen
        InputHandler.gameStarted = false;
        victoryMenu.PlayerWins();
    }

    private void OnDisable() {
        targetable.TargetableWordCompleted -= c_TargetableWordCompleted;
    }
}
