// Authored by Nate
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void LoadNextScene()
    {
        SceneManager.LoadScene(0); // Load scene 0 
    }
}