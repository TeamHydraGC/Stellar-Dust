using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private int moveSpeed;
    [SerializeField] private float jumpForce = 10f;
    public bool useTransformMovement;
    private bool isGrounded = false;
    public AudioSource audioSource;
    public AudioClip jumpSound;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        // Horizontal Movement
        float x = Input.GetAxis("Horizontal");

        if (useTransformMovement == false)
        {
            rb.linearVelocity = new Vector2(x * moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            transform.position = new Vector3(transform.position.x + x * Time.deltaTime * moveSpeed,
                                             transform.position.y,
                                             transform.position.z);
        }


        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
            
            if (!isGrounded)
            {
                jumpForce = 0f;
            }
            else
            {
                jumpForce = 10f;
            }

            audioSource.PlayOneShot(jumpSound);

           
        }
    }

    void FixedUpdate()
    {
        // Check if the player is on the ground using raycast
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player is grounded
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
            jumpForce = 10f;
        }
    }
}
