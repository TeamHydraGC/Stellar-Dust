// By: Vindeko...Pretty much a copy of Nate's EnemyHealth Script, to keep everything simple and intact.
using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour
{
    // Health variables
    public int bossHealth; // Current health
    public int bossMaxHealth; // Maximum health, set in the Inspector
    public bool isInvulnerable = false; // For invulnerability mechanics
    public bool bossDead = false; // Tracks if the boss is dead

    // Scoring
    public int scoreValue = 100; // Points awarded for defeating the boss

    // Gore effects
    public GameObject bloodEffect; // Blood Particle Prefab
    public GameObject goldEffect; // Gold Particle Prefab
    public AudioClip bloodSound; // Sound for gore-on death
    public AudioClip goldSound; // Sound for gore-off death

    private AudioSource audioSource;

    public static BossHealth Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Initialize health
        bossHealth = bossMaxHealth;

        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int amount)
    {
        if (!isInvulnerable) // Only take damage if not invulnerable
        {
            bossHealth -= amount; // Subtract damage
            Debug.Log(gameObject.name + " health is currently " + bossHealth);

            if (bossHealth <= 0 && !bossDead) // Trigger death if health reaches zero
            {
                bossDead = true;
                Die();
            }
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has been defeated!");

        // Update the player's score
        FindFirstObjectByType<ScoreUI>().AddScore(scoreValue);
        Debug.Log("Player awarded " + scoreValue + " points!");

        // Handle gore effects based on GoreToggle
        if (GoreToggle.goreEnabled)
        {
            // Gore is enabled: spawn blood effect and play blood sound
            if (bloodEffect != null)
            {
                Instantiate(bloodEffect, transform.position, Quaternion.identity);
            }
            if (bloodSound != null)
            {
                audioSource.PlayOneShot(bloodSound);
            }
        }
        else
        {
            // Gore is disabled: spawn gold effect and play gold sound
            if (goldEffect != null)
            {
                Instantiate(goldEffect, transform.position, Quaternion.identity);
            }
            if (goldSound != null)
            {
                audioSource.PlayOneShot(goldSound);
            }
        }

        // Delay destruction to allow sound to play
        StartCoroutine(DestroyAfterSound());
    }

    private IEnumerator DestroyAfterSound()
    {
        yield return new WaitForSeconds(0.5f); // Adjust delay as needed
        Destroy(gameObject);
    }

    public void ActivateInvulnerability(float duration)
    {
        StartCoroutine(InvulnerabilityTimer(duration));
    }

    private IEnumerator InvulnerabilityTimer(float duration)
    {
        isInvulnerable = true;
        Debug.Log(gameObject.name + " is now invulnerable for " + duration + " seconds.");
        yield return new WaitForSeconds(duration);
        isInvulnerable = false;
        Debug.Log(gameObject.name + " is no longer invulnerable.");
    }
}