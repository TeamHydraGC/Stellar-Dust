// Authored by Nate
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth; // current player health, do not touch unless via the TakeDamage function below!
    public int playerMaxHealth = 4; // maximum player health, dont tweak during runtime
    public bool playerDead = false;

    public void playerTakeDamage(int amount) // Method to deal damage to the player
    {
        playerHealth -= amount; // Sets player health as whatever TakeDamage reports it to be

        if (playerHealth == 1) // Warning for player health being at 1
        {
            Debug.Log("Player health is at 1! Taking any more damage will kill the player!");
        }

        if (playerHealth <= 0) // if player is dead, set playerDead to true; will influence the next if statement
        {
            Debug.Log("Player health <= 0, creating respawn dialogue.");
            playerDead = true;
            SceneManager.LoadScene(sceneBuildIndex: 2);
            playerDead = !playerDead;
        }
    }
}