using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public static PlayerScore Instance { get; private set; } // Singleton wizardry

    // Declaring variables
    public int currentScore; // Current score, can be added or subtracted from
    public int totalScore; // Total score, DO NOT TOUCH. I'll find a way to make this write-only later.

    private void Awake() // more singleton wizardry
    {
        Instance = this;
    }

    public void IncreaseScore(int amount)
    {
        currentScore += amount;
        totalScore += amount;
    }
}