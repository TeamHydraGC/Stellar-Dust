// By: Vinners
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform spawnPoint; 
    public GameObject playerPrefab; 

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player"); 

        if (player != null)
        {
            // Move the existing player to the spawn point
            player.transform.position = spawnPoint.position;
            player.transform.rotation = spawnPoint.rotation;
        }
        else if (playerPrefab != null)
        {
            Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("No Player found and no prefab assigned to SpawnManager.");
        }
    }
}