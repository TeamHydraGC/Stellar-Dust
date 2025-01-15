using System.Threading.Tasks;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{

    public PlayerHealth playerHealth;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision) // detecting collission
    {
        if(collision.gameObject.tag == "Player") // if colliding with an object tagged as Player, deal damage
        {
            playerHealth.TakeDamage(1);
            Task.Delay(250); // This is a hack. Pause the execution of this script for 0.250 seconds (effectively a cooldown on taking damage)
        }
    }

}