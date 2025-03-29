// By: Vindeko, first attack is same code as NPCShootScript - Normal Shoot Attack for first half of boss health
using UnityEngine;
using System.Collections;

public class SamuraiAttack : MonoBehaviour
{
    public GameObject projectile; // Projectile prefab for shooting
    public Transform shootPoint; // Point where the projectiles spawn
    public float shootCooldown = 2f; // Time between shots

    public float dashSpeed = 10f; // Speed of the dash
    public float dashDuration = 0.5f; // Time the dash lasts
    public float dashCooldown = 3f; // Time between dashes

    private BossHealth bossHealth;
    private Transform player; // Reference to the player's position
    private bool isDashing = false; // To prevent overlapping dashes
    private bool isShooting = true; // Start with shooting as the main attack
    private float attackCooldownTimer = 0f; // Timer to handle cooldowns

    void Start()
    {
        // Get references
        bossHealth = GetComponent<BossHealth>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        attackCooldownTimer = shootCooldown; // Start shooting immediately
    }

    void Update()
    {
        // Check if boss health is below 50% to switch attacks
        if (bossHealth.bossHealth <= bossHealth.bossMaxHealth / 2 && isShooting)
        {
            isShooting = false; // Stop shooting
        }

        if (isShooting)
        {
            // Handle shooting
            attackCooldownTimer += Time.deltaTime;
            if (attackCooldownTimer >= shootCooldown)
            {
                ShootProjectile();
                attackCooldownTimer = 0f; // Reset cooldown timer
            }
        }
        else if (!isDashing)
        {
            // Dash at the player
            StartCoroutine(DashTowardsPlayer());
        }
    }

    void ShootProjectile()
    {
        if (projectile != null && shootPoint != null)
        {
            Instantiate(projectile, shootPoint.position, shootPoint.rotation);
            Debug.Log("Samurai Boss: Shot a projectile!");
        }
    }

    IEnumerator DashTowardsPlayer()
    {
        isDashing = true;

        // Calculate direction to the player
        Vector2 direction = (player.position - transform.position).normalized;

        // Save the original position to calculate dash distance
        Vector2 originalPosition = transform.position;

        float elapsed = 0f;

        while (elapsed < dashDuration)
        {
            // Move the boss in the direction of the player
            transform.position += (Vector3)(direction * dashSpeed * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Debug.Log("Samurai Boss: Finished dashing!");

        // Wait for the dash cooldown
        yield return new WaitForSeconds(dashCooldown);

        isDashing = false;
    }
}