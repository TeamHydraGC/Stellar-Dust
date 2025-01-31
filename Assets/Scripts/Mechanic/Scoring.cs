using UnityEngine;

public class Scoring : MonoBehaviour
{
    // Declaring variables
    public static int currentScore; // Current score, can be added or subtracted from
    public static int totalScore; // Total score, DO NOT TOUCH. I'll find a way to make this write-only later.

    private void Start()
    {
        currentScore = 0;
    }

    public void addScore(int amount)
    {
        currentScore += amount;
        totalScore += amount;
    }
}