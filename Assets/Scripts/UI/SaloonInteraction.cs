using UnityEngine;
using UnityEngine.SceneManagement;

public class SaloonInteraction : MonoBehaviour
{
    public GameObject interactionPrompt; // UI element for the prompt
    public GameObject bountyBoardPanel;  // Bounty board panel to show

    private bool playerIsNear = false; // Tracks if the player is in range

    void Start()
    {
        // Ensure the interaction prompt is hidden at the start
        if (interactionPrompt != null)
            interactionPrompt.SetActive(false);

        // Attempt to reassign the Bounty Board panel dynamically
        if (bountyBoardPanel == null)
        {
            ReassignBountyBoardPanel();
        }
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
            Debug.LogError("Bounty Board panel is not assigned in the Inspector or dynamically!");
        }
    }

    private void ReassignBountyBoardPanel()
    {
        // Find the parent GameObject (like the Canvas) and search its children
        Transform parent = GameObject.Find("Canvas")?.transform; // Replace "Canvas" with your actual parent name
        if (parent != null)
        {
            Transform panelTransform = parent.Find("BountyBoard"); 
            if (panelTransform != null)
            {
                bountyBoardPanel = panelTransform.gameObject;
                Debug.Log("Bounty Board panel found successfully!");
                return;
            }
        }

        // Log error if panel is still not found
        Debug.LogError("Failed to find Bounty Board panel, even among inactive objects. Check its setup and name.");
    }

    private void OnEnable()
    {
        // Hook into scene load events to dynamically reassign the panel
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unhook from scene load events
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name + ". Attempting to reassign Bounty Board panel...");
        ReassignBountyBoardPanel();
    }
}