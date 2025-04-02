// Authored by AJ, damage portion authored by Nate, Vin - added BossHealth Integration
using UnityEngine;
using System.Collections;
using System.Drawing.Text;

public class BulletScript : MonoBehaviour
{
    private Rigidbody2D rb;

    public float force; // effectively the bullet's speed
    public int damageValue = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Determine bullet direction based on player's facing direction
        bool isFacingRight = FindObjectOfType<PlayerMovement>().isFacingRight; // Reference PlayerMovement
        float facingDirection = isFacingRight ? 1f : -1f; // Right if true, left if false

        // Set bullet velocity
        rb.linearVelocity = new Vector2(facingDirection * force, 0f); // Adjust force for bullet speed

        // Rotate bullet to face the direction of movement
        transform.rotation = Quaternion.Euler(0, 0, facingDirection > 0 ? 90 : -90); // 90 degrees for right, -90 for left
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyHealth>())
        {
            EnemyHealth hp = collision.gameObject.GetComponent<EnemyHealth>();
            hp.enemyTakeDamage(damageValue);
            Debug.Log("Dealt " + damageValue + " to " + collision.gameObject.name);
        }
        else if (collision.gameObject.GetComponent<BossHealth>())
        {
            BossHealth bossHP = collision.gameObject.GetComponent<BossHealth>();
            bossHP.TakeDamage(damageValue);
            Debug.Log("Dealt " + damageValue + " damage to " + collision.gameObject.name);
        }

        Destroy(gameObject);
    }


    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Destroy(gameObject);
    //}
}
