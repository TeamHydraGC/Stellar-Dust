using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndSceneChange : MonoBehaviour
{
    // Coordinates for your specific area in the Wild West scene
    public Vector3 teleportPosition = new Vector3(10, 5, 0); // Replace with the desired position

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Load the Wild West scene
        SceneManager.LoadScene(2);

        // Use a coroutine to ensure the player is repositioned after the scene is loaded
        StartCoroutine(WaitForSceneLoad());
    }

    private IEnumerator WaitForSceneLoad()
    {
        // Wait for the scene to load fully
        yield return new WaitUntil(() => SceneManager.GetActiveScene().buildIndex == 2);

        // Find the player object and teleport them
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = teleportPosition; // Move the player to the specific area
            Debug.Log($"Player teleported to position: {teleportPosition}");
        }
        else
        {
            Debug.LogError("Player object not found! Check if your player object has the 'Player' tag.");
        }
    }
}