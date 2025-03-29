// By: Vinny
using UnityEngine;
using UnityEngine.SceneManagement;

public class BountyManager : MonoBehaviour
{
    public static BountyManager Instance;
    public GameObject bountyScreen;

    public int scoreToUnlockLevel2 = 100; 
    public int scoreToUnlockLevel3 = 200; 

    private bool isLevel2Unlocked = false; 
    private bool isLevel3Unlocked = false; 

    public int totalBanditsInLevel = 5; 
    private int killedBandits = 0; 


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

        // Call InitializeBandits inside Awake
        InitializeBandits();
    }

    private void InitializeBandits()
    {
        int currentLevel = GetCurrentLevel();

        if (currentLevel == 1)
        {
            totalBanditsInLevel = 5; // Set total bandits for Level 1
            killedBandits = 0; // Reset killed bandits count
        }
    }

    private void Update()
    {
        CheckForUnlocks();
    }

    private void CheckForUnlocks()
    {
        int currentScore = PlayerScore.Instance.currentScore;

        if (currentScore >= scoreToUnlockLevel2 && !isLevel2Unlocked)
        {
            CompleteBounty(1);
        }

        if (currentScore >= scoreToUnlockLevel3 && !isLevel3Unlocked)
        {
            CompleteBounty(2); 
        }
    }

    private int GetCurrentLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Wild West") 
        {
            return 1;
        }
        else if (currentScene == "WW_BossArea") 
        {
            return 2;
        }

        return 0; // Default or invalid level
    }

    public void OnBanditKilled()
    {
        int currentLevel = GetCurrentLevel();
    
        // Only handle bandit kills for Level 1
        if (currentLevel == 1)
        {
            killedBandits++;
            MissionManager missionManager = Object.FindFirstObjectByType<MissionManager>();
    
            if (missionManager != null)
            {
                missionManager.ShowMissionObjective($"Bandits left: {totalBanditsInLevel - killedBandits}");
            }
    
            // Complete Level 1's mission when all bandits are defeated
            if (killedBandits >= totalBanditsInLevel)
            {
                if (missionManager != null)
                {
                    missionManager.CompleteMission(); // Show "Mission Complete!"
                }
                CompleteBounty(1); // Mark Level 1's bounty as complete
            }
        }
    }


    public void UnlockLevel(int level)
    {
        if (level == 2)
        {
            isLevel2Unlocked = true; 
            // Debug.Log("Level 2 unlocked!");
        }
        else if (level == 3)
        {
            isLevel3Unlocked = true; 
        }
    }

    // Check if a specific level is unlocked
    public bool IsLevelUnlocked(int level)
    {
        if (level == 2)
        {
            // Debug.Log($"IsLevelUnlocked called for Level 2. Result: {isLevel2Unlocked}");
            return isLevel2Unlocked;
        }
        if (level == 3)
        {
            // Debug.Log($"IsLevelUnlocked called for Level 3. Result: {isLevel3Unlocked}");
            return isLevel3Unlocked;
        }
        return false;
    }

    public void ActivateFirstBounty()
    {
        // Debug.Log("ActivateFirstBounty() called!");

        MissionManager missionManager = Object.FindFirstObjectByType<MissionManager>();
        if (missionManager != null)
        {
            missionManager.ShowMissionObjective("Kill all the Bandits in the cave below!");
            missionManager.StartCoroutine(missionManager.HideMissionTextAfterDelay(3f)); // Hide text after 3 seconds
        }

        // Hide the BountyScreen UI
        if (bountyScreen != null)
        {
            bountyScreen.SetActive(false); 
        }

    }

    public void UnlockLevel3AfterSandbeast()
    {
        UnlockLevel(3); // Unlock the third bounty
        Debug.Log("Third bounty unlocked after defeating the Sandbeast!");

        LevelSelect levelSelect = Object.FindFirstObjectByType<LevelSelect>();
        if (levelSelect != null)
        {
            levelSelect.RefreshButtons();
        }
    }

    // Complete a bounty and unlock the next level
    public void CompleteBounty(int completedLevel)
    {
        UnlockLevel(completedLevel + 1); // Unlock the next level
        Debug.Log($"Level {completedLevel + 1} unlocked!");

        // Refresh buttons immediately
        LevelSelect levelSelect = Object.FindFirstObjectByType<LevelSelect>();
        if (levelSelect != null)
        {
            levelSelect.RefreshButtons();
        }
        else
        {
            // Debug.LogWarning("LevelSelect not found. Buttons could not be refreshed.");
        }
    }
}