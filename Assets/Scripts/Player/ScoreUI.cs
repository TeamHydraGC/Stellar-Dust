using UnityEngine;
using UnityEngine.UI; 
using TMPro;

// Vinny - ScoreUI Script calls Nate's Scoring Script for the UI
public class ScoreUI : MonoBehaviour
{
    public TMP_Text scoreText; 
    public Image coinImage; 

    void Start()
    {
        UpdateScoreUI(); 
    }

    public void UpdateScoreUI()
    {
        scoreText.text = PlayerScore.Instance.currentScore.ToString(); // Update score number
    }

    // Increase Score after an event like killing an enemy or whatever
    public void AddScore(int amount)
    {
        PlayerScore.Instance.IncreaseScore(amount); // Update the score in PlayerScore
        UpdateScoreUI(); 
    }
}
