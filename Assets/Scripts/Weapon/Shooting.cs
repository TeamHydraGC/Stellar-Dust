using UnityEngine;

// Vinny - Implemented Revolver class with properties | ENUM from Smiley's Weapon Logic
public class Shooting : MonoBehaviour
{
    private Camera maincam;
    private Vector3 mousepos;
    public GameObject bullet;
    public Transform bulletTransform;
    public AudioSource audioSource;
    public AudioClip fireSound;

    public bool canfire = true; // Start with canfire being true, so shooting can begin
    private float timer;
    public Revolver revolver = new Revolver(); // Instance of the Revolver class

    // Revolver enum and properties
    public enum RevolverState
    {
        ReadyToFire, // Ready to shoot
        Reloading    // Currently reloading
    }

    [System.Serializable]
    public class Revolver
    {
        public RevolverState state = RevolverState.ReadyToFire; // Initial state
        public int maxAmmo = 6; // Maximum ammo capacity
        public int currentAmmo = 6; // Start with a full cylinder
        public float fireCooldown = 0.5f; // Time between shots
        public float reloadTime = 2f; // Time to reload after emptying the cylinder
    }

    private float reloadTimer; // Timer for managing reload duration

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maincam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
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
            reloadTimer += Time.deltaTime; // Increment reload timer
            if (reloadTimer >= revolver.reloadTime) // Check if reload time is complete
            {
                revolver.currentAmmo = revolver.maxAmmo; // Refill ammo
                revolver.state = RevolverState.ReadyToFire; // Return to ready state
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
            canfire = false; // Disable firing until the cooldown is finished
            timer = 0; // Reset the timer after firing
            revolver.currentAmmo--; // Decrease ammo count
            Instantiate(bullet, bulletTransform.position, Quaternion.identity); // Fire the bullet

            audioSource.PlayOneShot(fireSound); // Play fire sound

            if (revolver.currentAmmo <= 0) // Check if ammo is depleted
            {
                revolver.state = RevolverState.Reloading; // Change state to reloading
                reloadTimer = 0; // Reset reload timer
            }
        }
    }
}
