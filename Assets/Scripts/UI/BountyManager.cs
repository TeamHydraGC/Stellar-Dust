// By: Vinny
using UnityEngine;

public class BountyManager : MonoBehaviour
{
    public static BountyManager Instance;

    public int scoreToUnlockLevel2 = 100; 
    public int scoreToUnlockLevel3 = 200; 

    private bool isLevel2Unlocked = false; 
    private bool isLevel3Unlocked = false; 

    public int totalBanditsInLevel = 5; // Total number of bandits in Level 1
    private int killedBandits = 0; // Number of bandits killed


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    private void Update()
    {
        // Check if levels should be unlocked based on the score
        CheckForUnlocks();
    }

    private void CheckForUnlocks()
    {
        // The player's current score
        int currentScore = PlayerScore.Instance.currentScore;

        // Unlock Level 2 if the score is met 
        if (currentScore >= scoreToUnlockLevel2 && !isLevel2Unlocked)
        {
            CompleteBounty(1);
        }

        // Unlock Level 3 if the score is met 
        if (currentScore >= scoreToUnlockLevel3 && !isLevel3Unlocked)
        {
            CompleteBounty(2); 
        }
    }

    public void OnBanditKilled()
    {
        killedBandits++;
        Debug.Log($"Bandit killed! Total killed: {killedBandits}/{totalBanditsInLevel}");

        if (killedBandits >= totalBanditsInLevel)
        {
            CompleteBounty(1); // Mark the bounty as complete
        }
    }   


    public void UnlockLevel(int level)
    {
        if (level == 2)
        {
            isLevel2Unlocked = true; 
            Debug.Log("Level 2 unlocked!");
        }
        else if (level == 3)
        {
            isLevel3Unlocked = true; 
            Debug.Log("Level 3 unlocked!");
        }
    }

    // Check if a specific level is unlocked
    public bool IsLevelUnlocked(int level)
    {
        if (level == 2) return isLevel2Unlocked;
        if (level == 3) return isLevel3Unlocked;
        return false;
    }

    public void ActivateFirstBounty()
    {
        Debug.Log("First bounty activated! Kill all bandits inside the cave below.");
    }

    // Complete a bounty and unlock the next level
    public void CompleteBounty(int completedLevel)
    {
        UnlockLevel(completedLevel + 1);
        Debug.Log($"Level {completedLevel + 1} unlocked!");
    }
}