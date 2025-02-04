using UnityEngine;

public class JAPANmovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private int moveSpeed;
    [SerializeField] private float jumpForce = 4f;
    public bool useTransformMovement;
    private bool isGrounded = false;
    public AudioSource audioSource;
    public AudioClip jumpSound;

    private bool isFacingRight = true;

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

        // Flip Object
        if ((x > 0 && !isFacingRight) || (x < 0 && isFacingRight))
        {
            FlipObject();
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
            audioSource.PlayOneShot(jumpSound);

            if (!isGrounded)
            {
                jumpForce = 0f;



            }
            else
            {
                jumpForce = 4f;

            }


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
        if (collision.contacts[0].normal.y > 0.01f)
        {
            isGrounded = true;
            jumpForce = 4f;
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
