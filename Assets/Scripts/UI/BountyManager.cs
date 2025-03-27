// By: Vinny
using UnityEngine;

public class BountyManager : MonoBehaviour
{
    public static BountyManager Instance;

    public int scoreToUnlockLevel2 = 50; 
    public int scoreToUnlockLevel3 = 100; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    private void Update()
    {
        CheckForUnlocks();
    }

    private void CheckForUnlocks()
    {
        int currentScore = PlayerScore.Instance.currentScore; 

        if (currentScore >= scoreToUnlockLevel2 && !IsLevelUnlocked(2))
        {
            UnlockLevel(2);
        }

        if (currentScore >= scoreToUnlockLevel3 && !IsLevelUnlocked(3))
        {
            UnlockLevel(3);
        }
    }

    public void UnlockLevel(int level)
    {
        PlayerPrefs.SetInt($"Level{level}Unlocked", 1);
        Debug.Log($"Level {level} unlocked!");
    }

    public bool IsLevelUnlocked(int level)
    {
        return PlayerPrefs.GetInt($"Level{level}Unlocked", 0) == 1;
    }
}