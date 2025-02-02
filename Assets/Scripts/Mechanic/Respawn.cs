// Authored by Vinny I think -- Nate

using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    public AudioSource audioSource;
    public AudioClip respawnSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = respawnPoint.position;
            audioSource.PlayOneShot(respawnSound);
        }
    }

    private void Start()
    {

    }
}
