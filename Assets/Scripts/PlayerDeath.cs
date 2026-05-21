using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public float deathY = -10f;
    public AudioClip deathSound;
    public float respawnDelay = 2f;

    private bool isDead = false;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (!isDead && transform.position.y < deathY)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        if (deathSound != null)
            audioSource.PlayOneShot(deathSound);
        Invoke(nameof(Respawn), respawnDelay);
    }

    void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}