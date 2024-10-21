using UnityEngine;

public class PlayerPush : MonoBehaviour
{

    public float distance = 1f;
    public LayerMask boxMask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance, boxMask);
    }
}
