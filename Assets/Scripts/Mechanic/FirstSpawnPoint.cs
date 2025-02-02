using UnityEngine;

public class FirstSpawnPoint : MonoBehaviour
{
    public Transform initialSpawn;
    void Start()
    {
        gameObject.transform.position = initialSpawn.position;
    }
}
