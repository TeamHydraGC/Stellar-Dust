using UnityEngine;

public class NPCShoot : MonoBehaviour
{
    public Transform player; 
    public GameObject projectile; 
    public Transform shootPoint; 
    public float detectionRange = 10f; 
    public float shootInterval = 2f; 
    public AudioClip gunshotSound; 

    private float shootTimer; 
    private NPCWanderNinja npcWander; 
    private AudioSource audioSource; 

    void Start()
    {
        npcWander = GetComponent<NPCWanderNinja>();
        audioSource = GetComponent<AudioSource>();

        if (npcWander == null)
        {
            Debug.LogWarning("NPCWanderNinja component not found on NPC.");
        }
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource component not found on NPC.");
        }

        // Attempt to assign the player reference at the start
        FindPlayer();
    }

    void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("Player reference is null. NPCShoot will stop tracking.");
            if (npcWander != null) npcWander.enabled = true;
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            if (npcWander != null) npcWander.enabled = false;

            Vector2 direction = (player.position - transform.position).normalized;

            if (direction.x > 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                if (shootPoint != null) shootPoint.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                if (shootPoint != null) shootPoint.localRotation = Quaternion.Euler(0, 0, 180);
            }

            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval)
            {
                Shoot(direction);
                shootTimer = 0f;
            }
        }
        else
        {
            if (npcWander != null) npcWander.enabled = true;
        }
    }

    void Shoot(Vector2 direction)
    {
        if (projectile != null && shootPoint != null)
        {
            GameObject newProjectile = Instantiate(projectile, shootPoint.position, shootPoint.rotation);
            Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
            NPCBulletScript bulletScript = newProjectile.GetComponent<NPCBulletScript>();

            if (rb != null && bulletScript != null)
            {
                rb.linearVelocity = direction * bulletScript.force;
            }

            if (audioSource != null && gunshotSound != null)
            {
                audioSource.PlayOneShot(gunshotSound);
            }
        }
        else
        {
            Debug.LogWarning("Projectile or ShootPoint is missing. Cannot fire.");
        }
    }

    void OnEnable()
    {
        PauseMenu.OnGameResumed += HandleGameResumed;
    }

    void OnDisable()
    {
        PauseMenu.OnGameResumed -= HandleGameResumed;
    }

    void HandleGameResumed()
    {
        if (player == null)
        {
            FindPlayer();
        }
    }

    void FindPlayer()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            Debug.Log("Player reference successfully assigned.");
        }
        else
        {
            Debug.LogWarning("Player not found in the scene.");
        }
    }
}