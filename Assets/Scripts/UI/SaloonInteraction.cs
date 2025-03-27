using UnityEngine;

public class SaloonInteraction : MonoBehaviour
{
    public GameObject interactionPrompt; // UI element for the prompt
    public GameObject bountyBoardPanel;  // Bounty board panel to show

    private bool playerIsNear = false; // Tracks if the player is in range

    void Start()
    {
        if (interactionPrompt != null)
            interactionPrompt.SetActive(false); // Hide prompt at start
    }

    void Update()
    {
        // Check for player input when near the saloon
        if (playerIsNear && Input.GetKeyDown(KeyCode.E))
        {
            OpenBountyBoard();
        }
    }

    void OnTriggerEnter2D(Collider2D other) // 2D-specific trigger method
    {
        // Check if the player enters the trigger zone
        if (other.CompareTag("Player"))
        {
            playerIsNear = true;
            if (interactionPrompt != null)
                interactionPrompt.SetActive(true); // Show prompt
        }
    }

    void OnTriggerExit2D(Collider2D other) // 2D-specific trigger method
    {
        // Check if the player leaves the trigger zone
        if (other.CompareTag("Player"))
        {
            playerIsNear = false;
            if (interactionPrompt != null)
                interactionPrompt.SetActive(false); // Hide prompt
        }
    }

    void OpenBountyBoard()
    {
        if (bountyBoardPanel != null)
        {
            bountyBoardPanel.SetActive(true); // Open bounty board UI
            Debug.Log("Bounty Board opened!");
        }
        else
        {
            Debug.LogError("Bounty Board panel is not assigned in the Inspector!");
        }
    }
}