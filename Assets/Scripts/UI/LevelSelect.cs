// By: Devin, edits by Vinny
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public GameObject bountyBoardPanel; 
    public Button level1Button; 
    public Button level2Button;
    public Button level3Button; 
    public Button closeButton; 

    void Start()
    {
        if (level1Button != null)
            level1Button.onClick.AddListener(StartLevel1Bounty);

        // Refresh buttons to reflect the current unlocked states
        RefreshButtons();

        if (closeButton != null)
            closeButton.onClick.AddListener(() => CloseBountyBoard());
    }

    // Refresh button states to reflect unlocked levels dynamically
    public void RefreshButtons()
    {
        if (level2Button != null)
            level2Button.interactable = BountyManager.Instance.IsLevelUnlocked(2); // Enable Level 2 button if unlocked

        if (level3Button != null)
            level3Button.interactable = BountyManager.Instance.IsLevelUnlocked(3); // Enable Level 3 button if unlocked

        Debug.Log("Button states refreshed!");
    }

    // Trigger the first bounty (quest) for Level 1
    void StartLevel1Bounty()
    {
        BountyManager.Instance.ActivateFirstBounty();
    }

    // Load a specific level scene
    void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Open the bounty board UI
    public void OpenBountyBoard()
    {
        if (bountyBoardPanel != null)
            bountyBoardPanel.SetActive(true);
    }

    // Close the bounty board UI
    public void CloseBountyBoard()
    {
        if (bountyBoardPanel != null)
            bountyBoardPanel.SetActive(false);
    }
}