using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject firstButton;
    private EventSystem eventSystem;
    public static bool isGamePaused = false;

    // Event to notify other scripts when the game resumes
    public delegate void GameResumedHandler();
    public static event GameResumedHandler OnGameResumed;

    void Start()
    {
        eventSystem = EventSystem.current;

        if (eventSystem == null)
        {
            Debug.LogWarning("EventSystem not found in the scene. Pause menu navigation may not work properly.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape) || (Gamepad.current != null && Gamepad.current.startButton.wasPressedThisFrame))
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
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isGamePaused = true;

        if (eventSystem != null && firstButton != null)
        {
            eventSystem.SetSelectedGameObject(firstButton);
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isGamePaused = false;

        // Clear the selected object to prevent null reference issues
        if (eventSystem != null)
        {
            eventSystem.SetSelectedGameObject(null);
        }

        // Notify other scripts that the game has resumed
        OnGameResumed?.Invoke();
    }

    public void Menu()
    {
        // Reset game state flags before transitioning to the main menu
        isGamePaused = false;

        SceneManager.LoadScene(0); // Load the main menu scene
        Time.timeScale = 1; // Reset time scale
    }

    public void Restart()
    {
        isGamePaused = false;
        Time.timeScale = 1;
    
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}