// Authored by AJ, damage portion authored by Nate, Vin - added BossHealth Integration
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float force; // Bullet speed
    public int damageValue = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) // Null check for Rigidbody2D
        {
            Debug.LogError("Rigidbody2D is missing on " + gameObject.name);
            return;
        }

        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement == null) // Null check for PlayerMovement
        {
            Debug.LogError("PlayerMovement component not found in the scene!");
            return;
        }

        // Determine bullet direction based on player's facing direction
        bool isFacingRight = playerMovement.isFacingRight;
        float facingDirection = isFacingRight ? 1f : -1f;

        // Set bullet velocity
        rb.linearVelocity = new Vector2(facingDirection * force, 0f);

        // Rotate bullet to face the direction of movement
        transform.rotation = Quaternion.Euler(0, 0, facingDirection > 0 ? 90 : -90);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyHealth enemyHealth)) // Safely get EnemyHealth
        {
            enemyHealth.enemyTakeDamage(damageValue);
            Debug.Log("Dealt " + damageValue + " damage to " + collision.gameObject.name);
        }
        else if (collision.gameObject.TryGetComponent(out BossHealth bossHealth)) // Safely get BossHealth
        {
            bossHealth.TakeDamage(damageValue);
            Debug.Log("Dealt " + damageValue + " damage to " + collision.gameObject.name);
        }
        else
        {
            Debug.Log("Collision object does not have EnemyHealth or BossHealth.");
        }

        Destroy(gameObject); // Destroy bullet after collision
    }
}