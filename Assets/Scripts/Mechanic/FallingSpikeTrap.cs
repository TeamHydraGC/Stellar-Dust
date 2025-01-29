using UnityEngine;

public class FallingSpikeTrap : MonoBehaviour
{
    public float fallSpeed = 5f;
    public int damageAmount = 2; 
    private bool playerNearby = false;
    private bool hasFallen = false;

    void Update()
    {
        // Move the spike downwards if it has started falling and hasn't fallen completely yet
        if (playerNearby && !hasFallen)
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) // Damages player and removes the spike from the scene
    {
        if (collision.collider.CompareTag("Player"))
        {
            //Call the TakeDamage method on the player's health script
            PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.playerTakeDamage(damageAmount);
                Debug.Log("Player took " + damageAmount + " damage.");
            }

            // Stop the spike from falling further after hitting the player
            hasFallen = true;

            // Destroy the spike after collision with the player
            Destroy(gameObject);
        }
    }
}
