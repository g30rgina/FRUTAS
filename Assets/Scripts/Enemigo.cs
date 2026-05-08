using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public Transform[] patrolPoints;

    private Vector3 moveDirection;
    private Rigidbody2D _rigidBody2D;
    private int direction = 1;
    private float movementSpeed = 2;
    public int patrolIndex = 0;
    private Transform playerPosition;

    public float detectionRange = 5;
    public float attackRange = 1;
    public float attackCooldown = 1f;
    private float lastAttackTime;

    public int health = 2;
    public int damage = 1;

    public AudioClip attackSound;
    public AudioClip deathSound;
    private AudioSource audioSource;

    void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        float distanceToPlayer = Vector3.Distance(playerPosition.position, transform.position);

        if (distanceToPlayer > detectionRange)
        {
            Patrol();
        }
        else if (distanceToPlayer < detectionRange && distanceToPlayer > attackRange)
        {
            FollowPlayer();
        }
        else if (distanceToPlayer < attackRange)
        {
            Attack();
        }
    }

    void Patrol()
    {
        float distanceToPoint = Vector3.Distance(transform.position, patrolPoints[patrolIndex].position);

        if (distanceToPoint < 1f)
        {
            patrolIndex = patrolIndex == 0 ? 1 : 0;
        }

        Vector3 moveDirection = patrolPoints[patrolIndex].position - transform.position;

        if (moveDirection.x < 0)
        {
            direction = -1;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (moveDirection.x > 0)
        {
            direction = 1;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        _rigidBody2D.linearVelocity = new Vector2(direction * movementSpeed, _rigidBody2D.linearVelocity.y);
    }

    void FollowPlayer()
    {
        Vector3 moveDirection = playerPosition.position - transform.position;
        Movement(moveDirection);
    }

    void Movement(Vector3 moveDirection)
    {
        if (moveDirection.x < 0)
        {
            direction = -1;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (moveDirection.x > 0)
        {
            direction = 1;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        _rigidBody2D.linearVelocity = new Vector2(direction * movementSpeed, _rigidBody2D.linearVelocity.y);
    }

    void Attack()
    {
        direction = 0;
        _rigidBody2D.linearVelocity = Vector2.zero;

        if (Time.time > lastAttackTime + attackCooldown)
        {
            if (attackSound != null) audioSource.PlayOneShot(attackSound);
            playerPosition.GetComponent<PlayerController>().TakeDamage(damage);
            lastAttackTime = Time.time;
        }
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0) Die();
    }

    void Die()
    {
        if (deathSound != null) AudioSource.PlayClipAtPoint(deathSound, transform.position);
        Destroy(gameObject);
    }
}