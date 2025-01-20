using System.Threading.Tasks;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{

    public PlayerHealth playerHealth;

    public int damageValue = 1;

    private void OnCollisionEnter2D(Collision2D collision) // detecting collission
    {
        if(collision.gameObject.tag == "Player") // if colliding with an object tagged as Player, do:
        {
            playerHealth.playerTakeDamage(damageValue);
            Debug.Log("Dealt " + damageValue + " to player.");
            Task.Delay(250); // This is a hack. Pause the execution of this script for 0.250 seconds (effectively a cooldown on taking damage)
            //if(PlayerHealth)
        }
    }

}