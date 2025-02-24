using UnityEngine;

public class NEWmovement : MonoBehaviour
{
    // Walk Info
    [SerializeField] private float walkSpeed = 1;
    private float xAxis;

    // Jump Info
    [SerializeField] private float jumpForce = 45;
    [SerializeField] private Transform groundCheckpoint;
    [SerializeField] private float groundCheckY = 0.2f;
    [SerializeField] private float groundCheckX = 0.5f;
    [SerializeField] private LayerMask whatIsGround;

    
    //Components
    private Rigidbody2D rb;

    private bool isFacingRight = true;

    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {

        float x = Input.GetAxis("Horizontal");

        GetInputs();
        

        // Flip Object
        if ((x > 0 && !isFacingRight) || (x < 0 && isFacingRight))
        {
            FlipObject();
        }

        animator.SetFloat("Speed", Mathf.Abs(x));

    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    void GetInputs()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
    }

    private void Move()
    {
        rb.linearVelocity = new Vector2(walkSpeed * xAxis, rb.linearVelocity.y);
    }

    public bool Grounded()
    {
        return Physics2D.Raycast(groundCheckpoint.position, Vector2.down, groundCheckY, whatIsGround)
            || Physics2D.Raycast(groundCheckpoint.position + new Vector3(groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround)
            || Physics2D.Raycast(groundCheckpoint.position + new Vector3(-groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("IsJumping", true);

            if (Grounded())
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

                animator.SetBool("IsJumping", false);

            }
            else
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
        }
    }

    private void FlipObject()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    
 

}
