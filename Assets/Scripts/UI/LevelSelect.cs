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
        // Ensure buttons are assigned
        if (level1Button != null)
            level1Button.onClick.AddListener(() => LoadLevel("Level1"));

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