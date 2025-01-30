// Authored by Nate
using Unity.VisualScripting;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Declaring variables
    public int playerHealth; // current player health, DO NOT TOUCH unless via the playerTakeDamage function below!
    public int playerMaxHealth = 4; // maximum player health, dont tweak during runtime
    public int invulnTimeInSeconds = 1; // How long invulnerability should last, as an int
    public bool playerInvulnerable = false; // Used for invulnerability after taking damage, DO NOT TOUCH
    public bool playerDead = false; // If true, player goes to GameOver scene

    private void Start() // Set playerHealth to playerMaxHealth on start 
    {
        if (playerHealth > playerMaxHealth) 
        {
            playerHealth = playerMaxHealth;
        }
    }

    public void playerTakeDamage(int amount) // Method to deal damage to the player
    {
        if (!playerInvulnerable) // if not invulnerable, deal damage
        {
            playerHealth -= amount; // Decreases playerHealth by an int given by playerTakeDamage
            playerInvulnerable = !playerInvulnerable;
            StartCoroutine(InvulnAfterDamageTaken()); // Starting coroutine for i-frame timer
            Debug.Log(gameObject.name + " is now invulnerable.");
        }

        if (playerHealth == 1) // If health at 1, log a warning, otherwise log current health normally
        {
            Debug.LogWarning(gameObject.name + " health is at 1! Taking any more damage will kill " + gameObject.name);
        }
        else
        {
            Debug.Log(gameObject.name + " health is currently " + playerHealth);
        }

        // Player death management
        if (playerHealth <= 0) // if player is dead, set playerDead to true, and load the GameOver scene
        {
            Debug.Log(gameObject.name + " health <= 0, entering Game Over screen.");
            playerDead = true;
            SceneManager.LoadScene(sceneBuildIndex: 3);
            playerDead = !playerDead;
        }
    }
    IEnumerator InvulnAfterDamageTaken() // i-frame shit SPECIFIC TO THE PLAYER
    {
        yield return new WaitForSeconds(invulnTimeInSeconds);
        playerInvulnerable = !playerInvulnerable;
        Debug.Log(gameObject.name + " is now vulnerable.");
    }
}