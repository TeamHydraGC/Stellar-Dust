using UnityEngine;

public class AISensor2D : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    private GameObject player;

    private bool haslineofsight = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (haslineofsight)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
        if (ray.collider != null)
        {
            haslineofsight = ray.collider.CompareTag("Player");
        }
    }
}
