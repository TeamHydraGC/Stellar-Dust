// Authored by AJ, damage portion authored by Nate
using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    private Vector3 mousepos;
    private Camera maincam;
    private Rigidbody2D rb;
    public EnemyHealth enemyHealth;
    public float force; // effectively the bullet's speed
    public int damageValue = 1;

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
        //why is this here
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemyHealth.enemyTakeDamage(damageValue);
            Debug.Log("Dealt " + damageValue + " to " + gameObject.name);
        }
        
    }
}
