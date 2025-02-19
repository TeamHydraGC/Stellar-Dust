using UnityEngine;

public class NPCBulletScript : MonoBehaviour
{
    public float force; // Bullet speed
    public int damageValue = 1;
    private Rigidbody2D rb;
    private Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Calculate direction towards the player
        Vector2 direction = (player.position - transform.position).normalized;

        // Set the velocity of the bullet
        rb.linearVelocity = direction * force;

        // Calculate rotation to face the direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.playerTakeDamage(damageValue);
                Debug.Log("Dealt " + damageValue + " to " + collision.gameObject.name);
            }
        }
        Destroy(gameObject);
    }
}
