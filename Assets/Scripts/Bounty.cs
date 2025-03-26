using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;

    void Start()
    {
        // Ensure buttons are assigned
        if (level1Button != null)
            level1Button.onClick.AddListener(() => LoadLevel("Level1"));

        if (level2Button != null)
            level2Button.onClick.AddListener(() => LoadLevel("Level2"));

        if (level3Button != null)
            level3Button.onClick.AddListener(() => LoadLevel("Level3"));
    }

    // Function to load a specific level
    void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
