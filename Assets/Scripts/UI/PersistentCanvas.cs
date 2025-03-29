using UnityEngine;

public class PersistentCanvas : MonoBehaviour
{
    void Awake()
    {
        // Check for duplicate Canvases based on specific names
        if (gameObject.name == "MainCanvas" || gameObject.name == "MissionCanvas")
        {
            // Check if another Canvas with the same name exists
            if (FindObjectsByType<Canvas>(FindObjectsSortMode.None).Length > 1)
            {
                Canvas[] allCanvases = FindObjectsByType<Canvas>(FindObjectsSortMode.None);
                
                foreach (Canvas canvas in allCanvases)
                {
                    if (canvas.gameObject.name == gameObject.name && canvas.gameObject != gameObject)
                    {
                        Destroy(canvas.gameObject); // Destroy duplicates with the same name
                    }
                }
            }
    
            DontDestroyOnLoad(gameObject); // Keep the original Canvas
            Debug.Log($"{gameObject.name} marked as persistent and duplicates removed.");
        }
        else
        {
            Destroy(gameObject); // Remove unintended duplicate Canvases
        }
    }
}