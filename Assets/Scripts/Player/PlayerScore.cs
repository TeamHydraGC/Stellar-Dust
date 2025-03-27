// Authored by Nate
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    // Singleton wizardry
    public static PlayerScore Instance { get; private set; } 
    private void Awake()
    {   
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Preserve the object
        }
        else
        {
            Destroy(gameObject); // Prevents dups
        }
    }

    // Declaring variables
    public int currentScore; // Current score, can be added or subtracted from
    public int totalScore; // Total score, DO NOT TOUCH. I'll find a way to make this write-only later.

    public void IncreaseScore(int amount) // For ScoreTest.cs ONLY
    {
        currentScore += amount;
        totalScore += amount;
    }
}