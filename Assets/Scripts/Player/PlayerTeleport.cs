// By: Vinners
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerTeleport : MonoBehaviour
{
    public int teleportSceneIndex = 4; 
    public float teleportDelay = 5f; 
    private bool teleportTriggered = false; 

    // Trigger teleportation logic
    public void TriggerTeleport()
    {
        if (!teleportTriggered)
        {
            teleportTriggered = true; 
            StartCoroutine(TeleportAfterDelay());
        }
    }

    private IEnumerator TeleportAfterDelay()
    {
        Debug.Log("Teleport triggered. Waiting " + teleportDelay + " seconds...");
        yield return new WaitForSeconds(teleportDelay);

        Debug.Log("Teleporting player to scene index: " + teleportSceneIndex);
        SceneManager.LoadScene(teleportSceneIndex);

        // Wait for the scene to finish loading
        yield return new WaitUntil(() => SceneManager.GetActiveScene().buildIndex == teleportSceneIndex);
        Debug.Log("Scene loaded successfully.");

        // Find the spawn point in the new scene
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position;
            Debug.Log("Player teleported to spawn point at: " + spawnPoint.transform.position);
        }
        else
        {
            Debug.LogError("SpawnPoint with tag 'SpawnPoint' not found!");
        }
    }
}