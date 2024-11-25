using UnityEngine;

public class BattleTest : MonoBehaviour
{
    // Homework -- Create a script called Conditions that does the following:
    // 1. Store a current health value and a maximum health value (make up the numbers).
    // if current health is at least 75% of maximum health, log "Ready for battle!"
    // otherwise, log "You must rest..."
    // 2. Store a mana value and a spell cost value (make up the numbers).
    // if mana is greater than the spell cost, log "You're a wizard Harry!"
    // otherwise, log "You need more potions..."

    void Start()
    {
        Debug.Log("Perpare for battle!");

        int playerLevel = 100;
        int enemyLevel = 500;

        if (playerLevel > enemyLevel)
        {
            // If the comparison is true, we log Begone foul fiend!!
            Debug.Log("Begone foul fiend!!");
        }
        else
        {
            // If the comparison is else, we log Begone pathetic player!!
            Debug.Log("Begone pathetic player...");
        }
    }
}
