// By: Vindeko...Pretty much a copy of Nate's EnemyHealth Script, to keep everything simple and intact.
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BossHealth : MonoBehaviour
{
    public int bossHealth;
    public int bossMaxHealth;
    public bool isInvulnerable = false;
    public bool bossDead = false;
    public int scoreValue = 100;

    public GameObject bloodEffect;
    public GameObject goldEffect;
    public AudioClip bloodSound;
    public AudioClip goldSound;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) // Null check for AudioSource
        {
            Debug.LogError("AudioSource is missing on " + gameObject.name);
        }
    }

    void Start()
    {
        bossHealth = bossMaxHealth;
        if (bossHealth <= 0) // Check initial health
        {
            Debug.LogError("BossMaxHealth should be greater than 0!");
            bossHealth = 1; // Assign default value if uninitialized
        }
    }

    public void TakeDamage(int amount)
    {
        if (!isInvulnerable)
        {
            bossHealth -= amount;
            Debug.Log(gameObject.name + " health is currently " + bossHealth);

            if (bossHealth <= 0 && !bossDead)
            {
                bossDead = true;
                Die();
            }
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has been defeated!");

        if (BountyManager.Instance != null)
        {
            BountyManager.Instance.UnlockLevel3AfterSandbeast();
        }
        else
        {
            Debug.LogError("BountyManager instance not found!");
        }

        ScoreUI scoreUI = FindObjectOfType<ScoreUI>();
        if (scoreUI != null)
        {
            scoreUI.AddScore(scoreValue);
            Debug.Log("Player awarded " + scoreValue + " points!");
        }
        else
        {
            Debug.LogError("ScoreUI component not found in the scene!");
        }

        // Handle gore effects
        if (GoreToggle.goreEnabled)
        {
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
            if (goldEffect != null)
            {
                Instantiate(goldEffect, transform.position, Quaternion.identity);
            }
            if (goldSound != null)
            {
                audioSource.PlayOneShot(goldSound);
            }
        }

        // Teleport player to another scene
        TeleportToScene("TargetSceneName"); // Replace "TargetSceneName" with your desired scene's name

        // Destroy the boss GameObject
        Destroy(gameObject);
    }

    private void TeleportToScene(string sceneName)
    {
        Debug.Log("Teleporting to scene: " + sceneName);
        SceneManager.LoadScene(5); // Load the specified scene
    }

    public void ActivateInvulnerability(float duration)
    {
        StartCoroutine(InvulnerabilityTimer(duration));
    }

    private IEnumerator InvulnerabilityTimer(float duration)
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(duration);
        isInvulnerable = false;
    }
}