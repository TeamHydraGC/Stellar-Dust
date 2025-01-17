using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    // This method is called when the collider enters the collision
    private void OnCollisionEnter(Collision collision)
    {
        // Check if it collided with a specific object
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Destroy the current game object (the one this script is attached to)
            Destroy(gameObject);
        }
    }
}