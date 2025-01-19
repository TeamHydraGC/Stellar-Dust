using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int currPlayerHealth; // current player health, do not touch unless via the TakeDamage function below!
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
        currPlayerHealth -= amount; // setting player health as whatever TakeDamage reports it to be

        if (currPlayerHealth == 1)
        {
            Debug.Log("Player health is at 1! Taking damage again will destroy the player object and this WILL BREAK THINGS, so be careful!");
        }
;
        
        if (currPlayerHealth <= 0) // player death logic
        {
            Debug.Log("Player health <= 0, destroying player! THIS BREAKS A LOT OF THINGS, DO NOT SHIP!");
            Destroy(gameObject); // VERY TEMPORARY, DO NOT SHIP THIS
        }
    }
}
