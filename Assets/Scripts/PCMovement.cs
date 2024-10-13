using UnityEngine;

public class PCMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction = Vector2.down;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direction = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction = Vector2.right;
        }

        float dt = Time.deltaTime;
        float speed = 10.0f;
        Vector3 change = direction * speed * dt;
        transform.position = transform.position + change;
    }
}
