using UnityEngine;

public class ElevatorPlatform : MonoBehaviour
{
    public float maxHeight = -1.00f;

    public float minHeight = -4.44f;

    public float speedUp = 0.8f;

    public float speedDown = 0.5f;

    private float step = 0.0f;

    private bool movingUp = true;

    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 currentPostion = rb.position;

        if(movingUp)
        {
            step = speedUp * Time.fixedDeltaTime;
            currentPostion.y += step;

            if(currentPostion.y >= maxHeight)
            {
                currentPostion.y = maxHeight;
                movingUp = false;
            }

        }
        else
        {
            step = speedDown * Time.fixedDeltaTime;
            currentPostion.y -= step;

            if(currentPostion.y <= minHeight)
            {
                currentPostion.y = minHeight;
                movingUp = true;
            }
        }

        rb.MovePosition(currentPostion);
        
    }
}
