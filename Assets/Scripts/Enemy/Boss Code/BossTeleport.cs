using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BossTeleport : MonoBehaviour
{
    public string bossName; // Name of the boss to check (e.g., "Sandbeast")
    public string teleportSceneName = "Saloon"; // Name of the scene to teleport to
    public float teleportDelay = 5f; // Time to wait before teleporting
    public string spawnPointTag = "SpawnPoint"; // Tag for the spawn point in the target scene

    private BossHealth bossHealth;

    void Start()
    {
        bossHealth = GetComponent<BossHealth>();

        if (bossHealth == null)
        {
            Debug.LogError("BossHealth component missing on " + gameObject.name);
        }
    }

    void Update()
    {
        // Check if the boss has died
        if (bossHealth != null && bossHealth.bossDead && bossHealth.gameObject.name == bossName)
        {
            StartCoroutine(TeleportPlayer());
            enabled = false; // Disable this script to avoid repeated calls
        }
    }

    private IEnumerator TeleportPlayer()
    {
        Debug.Log("Defeated " + bossName + "! Teleporting to " + teleportSceneName + " in " + teleportDelay + " seconds...");
        yield return new WaitForSeconds(teleportDelay);

        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Load the saloon scene
        SceneManager.LoadScene(teleportSceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == teleportSceneName)
        {
            // Find the spawn point in the scene
            GameObject spawnPoint = GameObject.FindGameObjectWithTag(spawnPointTag);
            if (spawnPoint != null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    player.transform.position = spawnPoint.transform.position;
                    Debug.Log("Player teleported to spawn point at: " + spawnPoint.transform.position);
                }
                else
                {
                    Debug.LogError("Player not found in the scene!");
                }
            }
            else
            {
                Debug.LogError("Spawn point with tag '" + spawnPointTag + "' not found in the scene!");
            }

            // Unsubscribe from the sceneLoaded event
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}