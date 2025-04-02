using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Required for UI navigation
using UnityEngine.InputSystem; // For controller support

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu; // The pause menu object
    [SerializeField] private GameObject firstButton; // The default button to be selected
    private EventSystem eventSystem; // Reference to the Event System
    public static bool isGamePaused = false; // Global flag to track if the game is paused

    void Start()
    {
        // Get the EventSystem in the scene
        eventSystem = EventSystem.current;
    }

    void Update()
    {
        // Check for pause inputs (ESC, P, or Start button)
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape) || Gamepad.current.startButton.wasPressedThisFrame)
        {
            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        // Handle D-Pad navigation when paused
        if (isGamePaused)
        {
            if (Gamepad.current.dpad.up.wasPressedThisFrame)
            {
                eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp()?.Select();
            }
            else if (Gamepad.current.dpad.down.wasPressedThisFrame)
            {
                eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown()?.Select();
            }

            // Select with X button
            if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                eventSystem.currentSelectedGameObject?.GetComponent<Button>()?.onClick.Invoke();
            }

            // Go back with Circle button
            if (Gamepad.current.buttonEast.wasPressedThisFrame)
            {
                Resume();
            }
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0; // Freeze the game
        isGamePaused = true; // Mark the game as paused

        // Automatically set the first button as selected in the menu
        eventSystem.SetSelectedGameObject(firstButton);
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1; // Resume the game
        isGamePaused = false; // Mark the game as unpaused

        // Clear the selected object to prevent UI interactions while playing
        eventSystem.SetSelectedGameObject(null);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}