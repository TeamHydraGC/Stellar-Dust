using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector3 mousepos;
    private Camera maincam;
    private Rigidbody2D rb;
    public float force;

    void Start()
    {
        maincam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousepos = maincam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));

        Vector3 BulletDirection = mousepos - transform.position;
        Vector3 Rotation = transform.position - mousepos;

        rb.linearVelocity = new Vector2(BulletDirection.x, BulletDirection.y).normalized * force;

        float rot = Mathf.Atan2(Rotation.y, Rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    void Update()
    {
        
    }
}
