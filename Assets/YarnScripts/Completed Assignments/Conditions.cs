using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conditions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int currenthealth = 200;

        int currentmana = 26;
        int spellcost = 25;

        float value = 200.0f;
        float maxhealth = value * 0.75f;

        if (currenthealth >  maxhealth)
        {
            Debug.Log("Ready for Battle!");
        }
        else
        {
            Debug.Log("You must rest..");
        }

        if (currentmana > spellcost)
        {
            Debug.Log("You're a wizard, Harry!");
        }
        else
        {
            Debug.Log("You need more potions..");
        }
    }
    
}