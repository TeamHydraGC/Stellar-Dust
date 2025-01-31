using UnityEngine;

public class Scoring : MonoBehaviour
{
    // Declaring variables
    public int currentScore = 0; // Current score, can be added or subtracted from
    public int totalScore; // Total score, DO NOT TOUCH. I'll find a way to make this write-only later.
    public void addScore(int amount)
    {
        currentScore += amount;
        totalScore += amount;
    }
}