// Authored by Nate
using Unity.VisualScripting;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth; // current player health, DO NOT TOUCH unless via the TakeDamage function below!
    public int playerMaxHealth = 4; // maximum player health, dont tweak during runtime
    public int invulnTimeInSeconds = 1; // How long invulnerability should last, as an int
    public bool playerInvulnerable = false; // Used for invulnerability after taking damage, DO NOT TOUCH
    public bool playerDead = false; // If true, player goes to GameOver scene

    private void Start() // Set playerHealth = playerMaxHealth on start 
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
            Debug.Log("Player now invulnerable.");
        }


        if (playerHealth == 1) // Log a warning for player health being at 1
        {
            Debug.LogWarning("Player health is at 1! Taking any more damage will kill the player!");
        }
        else
        {
            Debug.Log("Player health is currently " + playerHealth);
        }


        // Player death management
        if (playerHealth <= 0) // if player is dead, set playerDead to true, and load the GameOver scene
        {
            Debug.Log("Player health <= 0, entering Game Over screen.");
            playerDead = true;
            SceneManager.LoadScene(sceneBuildIndex: 2);
            playerDead = !playerDead;
        }
    }
    IEnumerator InvulnAfterDamageTaken() // i-frame shit 
    {
        if (playerInvulnerable)
        {
            yield return new WaitForSeconds(invulnTimeInSeconds);
            playerInvulnerable = false;
            Debug.Log("Player now vulnerable.");
        }
    }
}