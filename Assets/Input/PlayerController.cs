using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public float jumpForce = 10.0f;

    private Vector2 moveInput;
    private bool isFacingRight;
    private bool isOnGround;

    private Rigidbody2D rbody;

    private void Start()
    {
        moveInput = Vector2.zero;
        isFacingRight = true;
        isOnGround = false;

        rbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 velocity = rbody.linearVelocity;
        velocity.x = moveInput.x * movementSpeed;
        rbody.linearVelocity = velocity;

        if ((isFacingRight && moveInput.x < 0) || (!isFacingRight && moveInput.x > 0))
        {
            FlipGameObject();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log("Move Input: " + moveInput);  // Debug log to check input
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isOnGround)
        {
            rbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log("Jump");  // Debug log to check jump
        }
    }

    private void FlipGameObject()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        isFacingRight = !isFacingRight;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }
}
