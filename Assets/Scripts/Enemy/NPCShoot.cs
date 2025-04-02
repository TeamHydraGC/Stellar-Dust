// By: Vin
using UnityEngine;

public class NPCShoot : MonoBehaviour
{
    public Transform player; // Reference to the player
    public GameObject projectile; // Projectile prefab
    public Transform shootPoint; // Point from where the NPC will shoot
    public float detectionRange = 10f; // Range within which NPC detects player
    public float shootInterval = 2f; // Time-* between shots
    public AudioClip gunshotSound; // Gunshot sound

    private float shootTimer; // Timer to keep track of shooting interval
    private NPCWanderNinja npcWander; // Reference to the NPCWander script
    private AudioSource audioSource; // Reference to the Audio source component
    public Animator animator;
    void Start()
    {
        npcWander = GetComponent<NPCWanderNinja>(); // Get the NPCWander script
        audioSource = GetComponent<AudioSource>(); // Get AudioSource component

        // Dynamically assign the player at the start of the scene
        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                // Debug.LogWarning("Player not found in the scene. NPCShoot will be inactive.");
            }
        }
    }

    void Update()
    {
        // Check if player exists to avoid accessing a null Transform
        if (player == null)
        {
            // Debug.LogWarning("Player reference is null. NPCShoot will stop tracking.");
            npcWander.enabled = true; // Ensure NPC resumes wandering if player is null
            return; // Exit Update() to avoid errors
        }

        // Calculate distance to player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        // Debug.Log($"NPC distance to player: {distanceToPlayer}");

        // Check if the player is within detection range
        if (distanceToPlayer <= detectionRange)
        {
            // Stop wandering
            // Debug.Log("Player is within detection range. NPC is preparing to shoot.");
            npcWander.enabled = false;
           
            // Determine direction to face the player
            Vector2 direction = (player.position - transform.position).normalized;

            if (direction.x > 0)
            {
                // Face right
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                shootPoint.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if (direction.x < 0)
            {
                // Face left
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                shootPoint.localRotation = Quaternion.Euler(0, 0, 180);
            }

            // Check if it's time to shoot
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval)
            {
                Shoot(direction);
                shootTimer = 0f; // Reset timer
            }
        }
        else
        {
            // Resume wandering
            // Debug.Log("Player is outside detection range. NPC is wandering.");
            npcWander.enabled = true;
        }
    }

    void Shoot(Vector2 direction)
    {
        // Instantiate the projectile at the shootPoint position and rotation
        GameObject newProjectile = Instantiate(projectile, shootPoint.position, shootPoint.rotation);
        newProjectile.GetComponent<Rigidbody2D>().linearVelocity = direction * newProjectile.GetComponent<NPCBulletScript>().force;

        if (audioSource != null && gunshotSound != null)
        {
            audioSource.PlayOneShot(gunshotSound);
        }
    }
}

