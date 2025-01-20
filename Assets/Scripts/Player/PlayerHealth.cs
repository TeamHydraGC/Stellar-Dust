using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth; // current player health, do not touch unless via the TakeDamage function below!
    public int playerMaxHealth = 4; // maximum player health, dont tweak during runtime

    public void playerTakeDamage(int amount) // function to deal damage to the player
    {
        playerHealth -= amount; // Sets player health as whatever TakeDamage reports it to be

        if (playerHealth == 1) // Warning for player health being at 1
        {
            Debug.Log("Player health is at 1! Taking damage again will destroy the player object and this WILL BREAK THINGS, so be careful!");
        }
        
        if (playerHealth <= 0) // player death logic, DO NOT SHIP THIS
        {
            Debug.Log("Player health <= 0, destroying player! THIS BREAKS A LOT OF THINGS, DO NOT SHIP!");
            Destroy(gameObject); // VERY TEMPORARY, DO NOT SHIP THIS
        }
    }
}
