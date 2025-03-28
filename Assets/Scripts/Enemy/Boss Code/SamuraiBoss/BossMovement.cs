// By: Vindeko, simple movement script follows player when in range
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float moveSpeed = 2f; // Movement speed (adjustable for testing)
    public float detectionRange = 10f; // Range within which the boss detects the player

    private Transform player; // Reference to the player's position
    private Rigidbody2D rb; // Rigidbody for movement
    private bool playerDetected = false; // Check if the player is detected

    void Start()
    {
        // Find the player using the "Player" tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (PlayerInRange())
        {
            playerDetected = true;
            FacePlayer(); // Make the boss face the player
        }
        else
        {
            playerDetected = false;
            rb.linearVelocity = Vector2.zero; // Stop movement if the player is out of range
        }
    }

    void FixedUpdate()
    {
        if (playerDetected)
        {
            MoveTowardsPlayer();
        }
    }

    bool PlayerInRange()
    {
        // Check if the player is within detection range
        return Vector2.Distance(transform.position, player.position) <= detectionRange;
    }

    void FacePlayer()
    {
        // Calculate the direction to the player
        Vector2 direction = (player.position - transform.position).normalized;
    
        // Flip the sprite horizontally while maintaining its original scale
        if (direction.x < 0) // Player is to the left of the boss
        {
            transform.localScale = new Vector3(-0.6f, 0.6f, 0.6f); // Flip horizontally
        }
        else // Player is to the right of the boss
        {
            transform.localScale = new Vector3(0.6f, 0.6f, 0.6f); // Maintain normal scale
        }
    }



    void MoveTowardsPlayer()
    {
        // Move the boss towards the player
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }
}