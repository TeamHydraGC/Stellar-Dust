using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static Shooting;

// Vinny - Implemented Revolver class with properties, & gun sounds, gun fanning | ENUM from Smiley's Weapon Logic
public class Shooting : MonoBehaviour
{
    // private Camera maincam; // To calculate mouse position for aiming
    // private Vector3 mousepos; // Store mouse position in world space
    // private Vector2 aimInput; // Store aiming input from controller's right stick

    // Bullet prefabs
    public GameObject bullet;
    public GameObject explosivebullet;
    public GameObject piercebullet;

    public Transform bulletTransform; // Spawn point for bullets
    public AudioSource audioSource; // Audio source for playing sounds
    public AudioClip fireSound; // Sound for firing the gun
    public AudioClip reloadSound; // Sound for reloading the gun

    public bool canfire = true; // Start with canfire being true, so shooting can begin
    private float timer; // Timer for managing cooldown between shots
    public Revolver revolver = new Revolver(); // Instance of the Revolver class

    public Image[] bulletImages; // Array to hold bullet UI images

    public bool gunFanningUnlocked = false; // To track if the ability is unlocked
    private bool isFanning = false; // Prevent overlapping fan executions

    public InputActionAsset inputActions; // Reference to the Input Actions asset

    // Revolver enum and properties
    public enum RevolverState
    {
        ReadyToFire,  // Gun is ready to fire
        Reloading     // Gun is in the process of reloading
    }

    // Bullets enum
    public enum BulletTypes
    {
        Standard,     // Regular bullet
        Explosive,    // Explosive bullet
        Pierce        // Bullet that pierces through objects
    }

    // Set initial type to be default bullet type
    public BulletTypes bulletType = BulletTypes.Standard;

    [System.Serializable]
    public class Revolver
    {
        public RevolverState state = RevolverState.ReadyToFire; // Initial state
        public int maxAmmo = 6; // Maximum ammo capacity
        public int currentAmmo = 6; // Start with a full cylinder
        public float fireCooldown = 0.5f; // Time between shots
        public float reloadTime = 2f; // Time to reload after emptying the cylinder
        public float fanFireCooldown = 0.1f; // Cooldown between each fan shot
    }

    private float reloadTimer; // Timer for managing reload duration

    void Start()
    {
        // maincam = Camera.main; // Initialize the main camera reference
        UpdateBulletUI(); // Ensure the UI starts in the correct state

        if (inputActions == null)
        {
            Debug.LogError("InputActions asset is not assigned!");
            return;
        }

        // Setup input actions for controller support
        var playerMap = inputActions.FindActionMap("Player");

        playerMap.FindAction("Shoot").performed += ctx => FireSingleShot(); // R2 for shooting
        playerMap.FindAction("FanFire").performed += ctx => StartCoroutine(FanFire()); // R1 for gun fanning
        playerMap.FindAction("Reload").performed += ctx => Reload(); // Triangle for reloading
        // playerMap.FindAction("Aim").performed += ctx => OnAim(ctx); // Right stick for aiming
    }

    void Update()
    {
        // Convert mouse position to world space for mouse aiming
        // mousepos = maincam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));

        // Handle aiming with controller's right stick
        // if (aimInput != Vector2.zero)
        // {
        //     Vector3 aimDirection = new Vector3(aimInput.x, aimInput.y, 0).normalized;
        //     float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        //     transform.rotation = Quaternion.Euler(0, 0, angle);
        // }

        // Handle reloading logic
        if (revolver.state == RevolverState.Reloading)
        {
            ReloadLogic(); // Manage reload logic
            return;
        }

        // Accumulate time for firing cooldown
        if (!canfire)
        {
            timer += Time.deltaTime;
            if (timer >= revolver.fireCooldown)
            {
                canfire = true; // Allow firing again
            }
        }
    }

    // public void OnAim(InputAction.CallbackContext context)
    // {
    //     aimInput = context.ReadValue<Vector2>();
    //     Debug.Log("Aim Input: " + aimInput); // Log the right stick values
    // }

    public void FireSingleShot()
    {
        // Check if the gun is ready to fire and there is ammo
        if (!canfire || revolver.state != RevolverState.ReadyToFire || revolver.currentAmmo <= 0) return;
        if (PauseMenu.isGamePaused) return;

        canfire = false; // Disable firing until cooldown finishes
        timer = 0; // Reset cooldown timer
        revolver.currentAmmo--; // Decrease ammo count

        GameObject BulletToFire = bullet; // Determine bullet type
        switch (bulletType)
        {
            case BulletTypes.Standard:
                BulletToFire = bullet;
                break;
            case BulletTypes.Explosive:
                BulletToFire = explosivebullet;
                break;
            case BulletTypes.Pierce:
                BulletToFire = piercebullet;
                break;
        }

        // Determine direction based on the player's facing direction
        bool isFacingRight = FindObjectOfType<PlayerMovement>().isFacingRight; // Reference `PlayerMovement`
        float facingDirection = isFacingRight ? 1f : -1f; // Right if true, left if false

        // Instantiate bullet and set velocity
        GameObject instantiatedBullet = Instantiate(BulletToFire, bulletTransform.position, Quaternion.identity);
        instantiatedBullet.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(facingDirection * 10f, 0f); // Adjust speed

        audioSource.PlayOneShot(fireSound); // Play gunshot sound
        UpdateBulletUI(); // Update ammo UI

        // Check if out of ammo
        if (revolver.currentAmmo <= 0)
        {
            revolver.state = RevolverState.Reloading; // Start reload process
            reloadTimer = 0; // Reset reload timer
        }
    }

    System.Collections.IEnumerator FanFire()
    {
        if (!gunFanningUnlocked || isFanning || revolver.currentAmmo <= 0) yield break;
    
        isFanning = true;
        while (revolver.currentAmmo > 0)
        {
            revolver.currentAmmo--; // Decrease ammo count
    
            GameObject BulletToFire = bullet; // Determine bullet type
            switch (bulletType)
            {
                case BulletTypes.Standard:
                    BulletToFire = bullet;
                    break;
                case BulletTypes.Explosive:
                    BulletToFire = explosivebullet;
                    break;
                case BulletTypes.Pierce:
                    BulletToFire = piercebullet;
                    break;
            }
    
            // Determine direction based on facing direction
            bool isFacingRight = FindObjectOfType<PlayerMovement>().isFacingRight;
            float facingDirection = isFacingRight ? 1f : -1f;
    
            // Instantiate bullet and set velocity
            GameObject instantiatedBullet = Instantiate(BulletToFire, bulletTransform.position, Quaternion.identity);
            instantiatedBullet.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(facingDirection * 10f, 0f);
    
            audioSource.PlayOneShot(fireSound); // Play sound
            UpdateBulletUI(); // Update ammo UI
            yield return new WaitForSeconds(revolver.fanFireCooldown); // Wait between fan shots
        }

        revolver.state = RevolverState.Reloading; // Start reload process
        reloadTimer = 0; // Reset reload timer
        isFanning = false; // Allow fan fire again
    }

    public void TriggerFanFire()
    {
        StartCoroutine(FanFire()); // Start the coroutine from this wrapper method
    }

    public void Reload()
    {
        // Check if reloading is already in progress or ammo is full
        if (revolver.state == RevolverState.Reloading || revolver.currentAmmo == revolver.maxAmmo) return;

        revolver.state = RevolverState.Reloading; // Start reloading
        reloadTimer = 0; // Reset reload timer
    }

    void ReloadLogic()
    {
        if (reloadTimer == 0)
        {
            audioSource.PlayOneShot(reloadSound); // Play reload sound
        }

        reloadTimer += Time.deltaTime;

        if (reloadTimer >= revolver.reloadTime)
        {
            revolver.currentAmmo = revolver.maxAmmo; // Refill ammo
            revolver.state = RevolverState.ReadyToFire; // Return to ready state
            UpdateBulletUI(); // Update the UI
        }
    }

    void UpdateBulletUI()
    {
        // Update the UI based on current ammo
        for (int i = 0; i < bulletImages.Length; i++)
        {
            Color color = bulletImages[i].color;
            color.a = i < revolver.currentAmmo ? 1f : 0.2f;
            bulletImages[i].color = color;
        }
    }
}