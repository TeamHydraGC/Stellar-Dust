// Authored by Nate
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth; // current player health, do not touch unless via the TakeDamage function below!
    public int playerMaxHealth = 4; // maximum player health, dont tweak during runtime
    public bool playerDead = false;

    private void Start() // Making sure that playerHealth never goes above playerMaxHealth
    {
        if (playerHealth > playerMaxHealth)
        {
            playerHealth = playerMaxHealth;
        }
    }

    public void playerTakeDamage(int amount) // Method to deal damage to the player
    {
        playerHealth -= amount; // Decreases playerHealth by an int given by playerTakeDamage
        

        if (playerHealth == 1) // Log a warning for player health being at 1
        {
            Debug.LogWarning("Player health is at 1! Taking any more damage will kill the player!");
        }
        else
        {
            Debug.Log("Player health is currently " + playerHealth);
        }


        // Player death management
        if (playerHealth <= 0) // if player is dead, set playerDead to true; will influence the next if statement
        {
            Debug.Log("Player health <= 0, entering Game Over screen.");
            playerDead = true;
            SceneManager.LoadScene(sceneBuildIndex: 2);
            playerDead = !playerDead;
        }
    }
}