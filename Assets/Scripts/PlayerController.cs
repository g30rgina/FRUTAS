using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float jumpForce = 10;
    public float bounceForce = 10;

    private InputAction moveAction;
    public Vector2 moveDirection;
    private InputAction jumpAction;
    private InputAction _attackAction;

    public Rigidbody2D rBody2D;
    private SpriteRenderer render;
    private GroundSensor sensor;
    public Animator animator;

    public AudioClip deathSound;
    public AudioClip attackSound;
    private AudioSource audioSource;

    public int health = 3;
    private bool isDead = false;

    void Awake()
    {
        rBody2D = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        sensor = GetComponentInChildren<GroundSensor>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        moveAction = InputSystem.actions["Move"];
        jumpAction = InputSystem.actions["Jump"];
        _attackAction = InputSystem.actions["Attack"];
    }

    void Update()
    {
        if (isDead) return;

        moveDirection = moveAction.ReadValue<Vector2>();

        if (moveDirection.x > 0)
        {
            render.flipX = false;
            animator.SetBool("isRunning", true);
        }
        else if (moveDirection.x < 0)
        {
            render.flipX = true;
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if (jumpAction.WasPressedThisFrame() && sensor.isGrounded)
        {
            rBody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (_attackAction.WasPressedThisFrame())
        {
            Attack();
        }

        animator.SetBool("isJumping", !sensor.isGrounded);
    }

    void FixedUpdate()
    {
        if (isDead) return;
        rBody2D.linearVelocity = new Vector2(moveDirection.x * movementSpeed, rBody2D.linearVelocity.y);
    }

    public void Bounce()
    {
        rBody2D.linearVelocity = new Vector2(rBody2D.linearVelocity.x, 0);
        rBody2D.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
    }

    void Attack()
    {
        if (attackSound != null) audioSource.PlayOneShot(attackSound);
        animator.SetTrigger("isAttacking");

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1.5f);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                hit.GetComponent<Enemigo>().TakeDamage(1);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        health -= damage;
        if (health <= 0) Die();
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;
        if (deathSound != null) audioSource.PlayOneShot(deathSound);
        animator.SetTrigger("isDead");
        rBody2D.linearVelocity = Vector2.zero;
        Invoke("Respawn", 1.5f);
    }

    void Respawn()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}