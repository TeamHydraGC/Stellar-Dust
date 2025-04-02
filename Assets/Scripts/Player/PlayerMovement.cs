using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private int moveSpeed; // Speed of horizontal movement
    [SerializeField] private float jumpForce = 10f; // Jump force value
    public bool useTransformMovement; // Toggle for Rigidbody or Transform movement
    private bool isGrounded = false; // Check if player is on the ground
    public AudioSource audioSource;
    public AudioClip jumpSound;
    public Animator animator;

    public bool isFacingRight = true; 
    private Vector2 moveInput; // Store input direction from controller or keyboard

    [SerializeField] private Transform groundCheck; // Position for ground detection
    [SerializeField] private float groundCheckRadius = 0.1f; // Radius for ground check
    [SerializeField] private LayerMask groundLayer; // Layer to detect ground



    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        // Horizontal Movement
        if (!useTransformMovement)
        {
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            transform.position = new Vector3(transform.position.x + moveInput.x * Time.deltaTime * moveSpeed,
                                             transform.position.y,
                                             transform.position.z);
        }

        // Flip Object if changing direction
        if ((moveInput.x > 0 && !isFacingRight) || (moveInput.x < 0 && isFacingRight))
        {
            FlipObject();
        }

        // Update animator with movement speed
        animator.SetFloat("Speed", Mathf.Abs(moveInput.x));
    }

    void FixedUpdate()
    {
        // Ground detection using OverlapCircle
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player is grounded
        if (collision.contacts[0].normal.y > 0.1f)
        {
            isGrounded = true;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Read movement input from keyboard or controller
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Apply jump force
            audioSource.PlayOneShot(jumpSound); // Play jump sound
            isGrounded = false; // Player is no longer on the ground
        }
    }

    private void FlipObject()
    {
        isFacingRight = !isFacingRight; // Toggle the facing direction
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale; // Flip the object horizontally
    }

        private void OnDrawGizmosSelected()
    {
        // Visualize the ground check in the Editor
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

}