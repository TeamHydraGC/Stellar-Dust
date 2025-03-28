// By: Devin, edits by Vinny
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public GameObject bountyBoardPanel; // Panel for bounty board UI
    public Button level1Button; // Button for Level 1
    public Button level2Button; // Button for Level 2
    public Button level3Button; // Button for Level 3
    public Button closeButton; // Button to close the bounty board

    void Start()
    {
        // Ensure Level 1 button is assigned and activates the first bounty
        if (level1Button != null)
            level1Button.onClick.AddListener(StartLevel1Bounty);

        // Ensure Level 2 button loads Level 2
        if (level2Button != null)
            level2Button.onClick.AddListener(() => LoadLevel("Level2Scene"));

        // Placeholder for Level 3 button
        if (level3Button != null)
        {
            level3Button.interactable = BountyManager.Instance.IsLevelUnlocked(3);
            level3Button.onClick.AddListener(() => LoadLevel("Level3Scene")); // Update when Level 3 is ready
        }

        RefreshButtons();

        if (closeButton != null)
            closeButton.onClick.AddListener(() => CloseBountyBoard());
    }

    public void RefreshButtons()
    {
        if (level2Button != null)
            level2Button.interactable = BountyManager.Instance.IsLevelUnlocked(2);

        if (level3Button != null)
            level3Button.interactable = BountyManager.Instance.IsLevelUnlocked(3);

        // Debug.Log("Button states refreshed!");
    }

    void StartLevel1Bounty()
    {
        BountyManager.Instance.ActivateFirstBounty();
    }

    public void HideBountyBoardOnSceneChange()
    {
        if (bountyBoardPanel != null)
        {
            bountyBoardPanel.SetActive(false); // Hide the BountyBoard
        }
    }

    void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(5);
        HideBountyBoardOnSceneChange();
    }

    public void OpenBountyBoard()
    {
        if (bountyBoardPanel != null)
            bountyBoardPanel.SetActive(true);
    }

    public void CloseBountyBoard()
    {
        if (bountyBoardPanel != null)
            bountyBoardPanel.SetActive(false);
    }
}