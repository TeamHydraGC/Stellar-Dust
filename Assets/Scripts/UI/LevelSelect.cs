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
        // Log the unlock status being checked for each level
        Debug.Log($"Refreshing buttons...");
        Debug.Log($"Level 2 unlock status: {BountyManager.Instance.IsLevelUnlocked(2)}");
        Debug.Log($"Level 3 unlock status: {BountyManager.Instance.IsLevelUnlocked(3)}");
    
        if (level2Button != null)
        {
            level2Button.interactable = BountyManager.Instance.IsLevelUnlocked(2);
            Debug.Log($"Level 2 button interactable: {level2Button.interactable}");
        }
    
        if (level3Button != null)
        {
            level3Button.interactable = BountyManager.Instance.IsLevelUnlocked(3);
            Debug.Log($"Level 3 button interactable: {level3Button.interactable}");
        }
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