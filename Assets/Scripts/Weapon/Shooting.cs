using UnityEngine;
using UnityEngine.UI; 

// Vinny - Implemented Revolver class with properties, & gun sounds, gun fanning | ENUM from Smiley's Weapon Logic
public class Shooting : MonoBehaviour
{
    private Camera maincam;
    private Vector3 mousepos;
    public GameObject bullet;
    public Transform bulletTransform;
    public AudioSource audioSource;
    public AudioClip fireSound;
    public AudioClip reloadSound; 

    public bool canfire = true; // Start with canfire being true, so shooting can begin
    private float timer;
    public Revolver revolver = new Revolver(); // Instance of the Revolver class

    public Image[] bulletImages; // Array to hold bullet UI images

    public bool gunFanningUnlocked = false; // To track if the ability is unlocked
    private bool isFanning = false; // Prevent overlapping fan executions

    // Revolver enum and properties
    public enum RevolverState
    {
        ReadyToFire, 
        Reloading    
    }

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
        maincam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        UpdateBulletUI(); // Ensure the UI starts in the correct state
    }

    void Update()
    {
        // Convert mouse position to world space
        mousepos = maincam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));

        // Calculate the direction from the object to the mouse
        Vector3 rotation = mousepos - transform.position;

        // Calculate the rotation angle to face the mouse
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        // Apply the rotation to the object
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        // Handle reloading logic
        if (revolver.state == RevolverState.Reloading)
        {
            if (reloadTimer == 0) // Play reload sound only at the beginning of reloading
            {
                audioSource.PlayOneShot(reloadSound); // Play reload sound
            }
            reloadTimer += Time.deltaTime; // Increment reload timer
        
            if (reloadTimer >= revolver.reloadTime) // Check if reload time is complete
            {
                revolver.currentAmmo = revolver.maxAmmo; // Refill ammo
                revolver.state = RevolverState.ReadyToFire; // Return to ready state
                UpdateBulletUI(); // Reset the UI after reloading
            }
            return; // Exit to prevent firing during reload
        }

        // Accumulate time for firing cooldown
        if (!canfire)
        {
            timer += Time.deltaTime; // Add the time elapsed since the last frame to the timer
            if (timer >= revolver.fireCooldown) // If enough time has passed
            {
                canfire = true; // Allow firing again
            }
        }

        // Fire when the left mouse button is pressed, shooting is allowed, revolver is ready, and there is ammo
        if (Input.GetMouseButton(0) && canfire && revolver.state == RevolverState.ReadyToFire && revolver.currentAmmo > 0)
        {
            FireSingleShot();
        }

        // Check for fan fire (right mouse button) and if unlocked
        if (Input.GetMouseButtonDown(1) && gunFanningUnlocked && revolver.currentAmmo > 0 && !isFanning)
        {
            StartCoroutine(FanFire());
        }
    }

    void FireSingleShot()
    {
        canfire = false; // Disable firing until the cooldown is finished
        timer = 0; // Reset the timer after firing
        revolver.currentAmmo--; // Decrease ammo count
        Instantiate(bullet, bulletTransform.position, Quaternion.identity); // Fire the bullet

        audioSource.PlayOneShot(fireSound); 
        UpdateBulletUI(); 

        if (revolver.currentAmmo <= 0) // Check if ammo is depleted
        {
            revolver.state = RevolverState.Reloading; // Change state to reloading
            reloadTimer = 0; 
        }
    }

    System.Collections.IEnumerator FanFire()
    {
        isFanning = true; // Prevent overlapping fan fire executions
        while (revolver.currentAmmo > 0)
        {
            revolver.currentAmmo--; // Decrease ammo count
            Instantiate(bullet, bulletTransform.position, Quaternion.identity); 
            audioSource.PlayOneShot(fireSound); 
            UpdateBulletUI(); // Update the UI after firing
            yield return new WaitForSeconds(revolver.fanFireCooldown); // Wait between fan shots
        }

        revolver.state = RevolverState.Reloading; // Change state to reloading
        reloadTimer = 0; 
        isFanning = false; 
    }

    void UpdateBulletUI()
    {
        for (int i = 0; i < bulletImages.Length; i++)
        {
            Color color = bulletImages[i].color;
            color.a = i < revolver.currentAmmo ? 1f : 0.2f; // Full opacity for remaining bullets
            bulletImages[i].color = color;
        }
    }

    public void UnlockGunFanning()
    {
        gunFanningUnlocked = true; // Call this method after level 1 completion
    }
}
