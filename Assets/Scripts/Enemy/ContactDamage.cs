// Authored by Nate
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    public PlayerHealth playerHealth; // import playerHealth, ATTACH PLAYERHEALTH TO OBJECT IN EDITOR
    public int damageValue = 1; // how much damage the object this script is attached to deals, can be tweaked in-editor

    private void OnTriggerEnter2D(Collider2D other) // detecting collission
    {
        if(other.gameObject.tag == "Player") // if colliding with an object tagged as Player, do:
        {
            playerHealth.playerTakeDamage(damageValue);
            Debug.Log("Dealt " + damageValue + " to player.");
            //Task.Delay(250); // This is a hack, being replaced by the InvulnAfterDamageTaken coroutine in PlayerHealth.cs. Re-enable only if shit really goes sideways.
        }
    }

}