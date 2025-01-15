using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int playerHealth;
    public int playerMaxHealth = 4;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    {
        playerHealth -= amount;
        if (playerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
