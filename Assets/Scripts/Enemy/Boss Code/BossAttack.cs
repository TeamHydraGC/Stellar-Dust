using UnityEngine;

public class ProjectileFirer : MonoBehaviour
{
    // The attack to be fired
    public GameObject projectilePrefab;

    // The range within which the projectile will be fired
    public float fireRange = 20f;

    // Maximum distance a projectile can travel before being destroyed
    public float maxProjectileDistance = 50f;

    // Time between firing projectiles
    public float fireRate = 1f;

    // The player object
    public Transform player;

    // Layer mask to determine which objects can be collided with
    public LayerMask collisionMask; 

    private void Start()
    {
        // Searches for GameObject with tag "Player"
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }
    }

    private void Update()
    {
        // Checks if the player is within firing range
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= fireRange)
        {
            FireProjectile();
        }
    }

    void FireProjectile()
    {
        // Instantiate the projectile
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Get which direction which is towards the player
        Vector3 direction = (player.position - transform.position).normalized;

        // Set velocity towards the player
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        if (projectileRb != null)
        {
            // Velocity of the projectile
            projectileRb.linearVelocity = direction * 10f;
        }

        projectile.AddComponent<ProjectileBehavior>().Initialize(maxProjectileDistance, collisionMask);
    }
}

public class ProjectileBehavior : MonoBehaviour
{
    private float maxDistance;
    private LayerMask collisionMask;
    private Vector3 spawnPosition;

    public void Initialize(float maxDistance, LayerMask collisionMask)
    {
        this.maxDistance = maxDistance;
        this.collisionMask = collisionMask;
        spawnPosition = transform.position;
    }

    private void Update()
    {
        // Check the distance traveled
        if (Vector3.Distance(spawnPosition, transform.position) >= maxDistance)
        {
            // Destroy projectile if too far
            Destroy(gameObject); 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check for collision with any object in the collision mask or "layer"
        if ((collisionMask.value & (1 << collision.gameObject.layer)) > 0)
        {
            // Destroys the projectile if it hits something in the specified collision mask or "layer"
            Destroy(gameObject);
        }
    }
}