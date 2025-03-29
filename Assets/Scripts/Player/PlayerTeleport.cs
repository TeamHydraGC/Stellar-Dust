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

        // Save the current scene as the last scene
        GameManager.Instance.SetLastScene(SceneManager.GetActiveScene().name);

        Debug.Log("Teleporting player to scene index: " + teleportSceneIndex);
        SceneManager.LoadScene(teleportSceneIndex);

        // Wait for the scene to finish loading
        yield return new WaitUntil(() => SceneManager.GetActiveScene().buildIndex == teleportSceneIndex);
        Debug.Log("Scene loaded successfully.");

        SwitchToSceneSpecificCharacter();
    }

    private void SwitchToSceneSpecificCharacter()
    {
        GameObject characterPrefab;
    
        if (GameManager.Instance.lastScene == "TutorialScene")
        {
            Debug.Log("Switching to Wild West character...");
            characterPrefab = Resources.Load<GameObject>("WildWestCharacterPrefab"); // Adjust path/name
        }
        else
        {
            Debug.Log("Retaining default character...");
            characterPrefab = Resources.Load<GameObject>("DefaultCharacterPrefab"); // Adjust path/name
        }
    
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Destroy(player); // Remove the old character
    
            GameObject newPlayer = Instantiate(characterPrefab);
            newPlayer.transform.position = GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;
            Debug.Log("New character loaded at spawn point.");
        }
        else
        {
            Debug.LogError("Player object not found! Check setup.");
        }
    }
}