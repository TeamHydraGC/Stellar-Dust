// Authored by Nate
// ENSURE EnemyScore.cs RUNS BEFORE EnemyHealth.cs IN UNITY'S SCRIPT EXECUCTION ORDER

using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    // Declaring variables
    public int enemyHealth; // current enemy health, DO NOT TOUCH UNLESS VIA TakeDamage
    public int enemyMaxHealth; // maximum enemy health, dont tweak during runtime
    public int scoreValue = 1; // Amount of score eliminating the object this script is attached to will award
    public float enemyInvulnTime = 0.016f; // How long invulnerability should last, as a float
    public bool enemyInvulnerable = false; // Used for invulnerability after taking damage, DO NOT TOUCH UNLESS VIA InvulnAfterDamageTaken COROUTINE
    public bool enemyDead = false; // unused for now, will be used to handle death animations in the future


    public static EnemyHealth Instance { get; private set; } // instantiating public variabels for use elsewhere
    private void Awake()
    {
        Instance = this;
    }

    private void Start() // Set enemyHealth to enemyMaxHealth on script start 
    {
        if (enemyHealth != enemyMaxHealth)
        {
            enemyHealth = enemyMaxHealth;
        }
    }
    public void enemyTakeDamage(int amount) // Method to deal damage to the enemy this script is attached to
    {
        if (enemyInvulnerable == false) // if not invulnerable, deal damage
        {
            enemyHealth -= amount; // Decreases enemyHealth by an int given by enemyTakeDamage
            enemyInvulnerable = !enemyInvulnerable;
            StartCoroutine(InvulnAfterDamageTaken()); // Starting coroutine for i-frame timer
            Debug.Log(gameObject.name + " is now invulnerable.");
        }
        else if (enemyHealth <= 0) // if health reaches zero, destroy the object
        {
            Debug.Log(gameObject.name + " has died, destroying object.");
            Object.Destroy(gameObject);
        }
        else
        {
            Debug.Log(gameObject.name + " health is currently " + enemyHealth);
        }

        //Debug logging separate from health logic
        if (enemyHealth == 1) // If health at 1, log a warning, otherwise log current health normally
        {
            Debug.LogWarning(gameObject.name + " is at 1 health!");
        }
        IEnumerator InvulnAfterDamageTaken() // i-frame shit SPECIFIC TO THE ENEMY THIS SCRIPT IS ATTACHED TO 
        {
            if (enemyHealth >= 0)
            {
                yield return new WaitForSeconds(enemyInvulnTime);
                enemyInvulnerable = !enemyInvulnerable;
                Debug.Log(gameObject.name + " now vulnerable.");
            }
        }
    }
}
