// By: Devin, edits by Vinny
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public GameObject bountyBoardPanel; // Panel for bounty board
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;
    public Button closeButton; // Button to close the panel

    void Start()
    {
        // PlayerPrefs.DeleteAll(); // KEEP COMMENTED!!! Only use to PrefsReset
        // Debug.Log("PlayerPrefs reset!");


        // Ensure buttons are assigned
        if (level1Button != null)
            level1Button.onClick.AddListener(StartLevel1Bounty);

        if (level2Button != null)
        {
            level2Button.interactable = PlayerPrefs.GetInt("Level2Unlocked", 0) == 1;
            level2Button.onClick.AddListener(() => LoadLevel("Level2"));
        }

        if (level3Button != null)
        {
            level3Button.interactable = PlayerPrefs.GetInt("Level3Unlocked", 0) == 1;
            level3Button.onClick.AddListener(() => LoadLevel("Level3"));
        }

        if (closeButton != null)
            closeButton.onClick.AddListener(() => CloseBountyBoard());
    }

    void StartLevel1Bounty()
    {
        // Debug.Log("Level 1 bounty activated! Kill all bandit enemies.");
    
        // Trigger logic for the first quest
        BountyManager.Instance.ActivateFirstBounty();
    }


    // Function to load a specific level
    void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Function to open the bounty board
    public void OpenBountyBoard()
    {
        if (bountyBoardPanel != null)
            bountyBoardPanel.SetActive(true);
    }

    // Function to close the bounty board
    public void CloseBountyBoard()
    {
        if (bountyBoardPanel != null)
            bountyBoardPanel.SetActive(false);
    }
}