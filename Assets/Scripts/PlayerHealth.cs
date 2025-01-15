using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int playerHealth; // current player health, do not touch unless via the TakeDamage function below!
    public int playerMaxHealth = 4; // maximum player health, dont tweak during runtime
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount) // function to deal damage to the player
    {
        playerHealth -= amount; // 
        if (playerHealth <= 0) // player death logic
        {
            Destroy(gameObject); // VERY TEMPORARY, DO NOT SHIP THIS
        }
    }
}
