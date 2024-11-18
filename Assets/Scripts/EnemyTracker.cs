using UnityEngine;

public class AJEnemyTrackerScript : MonoBehaviour
{
    private Transform target;
    public float speed;
    public float detectionRange = 5f;  // Distance to detect obstacles
    public float avoidanceStrength = 5f;  // Strength of the avoidance force
    public float detectionAngle = 45f;  // Angle range for detection

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Declare directionToPlayer to avoid that stupid fucking error
        Vector2 directionToPlayer = (target.position - transform.position).normalized;

        // Raycast, check if there are obstacles in the way
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Quaternion.Euler(0, 0, -detectionAngle) * directionToPlayer, detectionRange);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Quaternion.Euler(0, 0, detectionAngle) * directionToPlayer, detectionRange);

        // If obstacles are detected, steer around them
        if (hitLeft.collider != null || hitRight.collider != null)
        {
            Vector2 avoidanceDirection = Vector2.zero;

            // If an obstacle is detected on the left, steer right
            if (hitLeft.collider != null)
                avoidanceDirection += Vector2.right;

            // If an obstacle is detected on the right, steer left
            if (hitRight.collider != null)
                avoidanceDirection += Vector2.left;

            // Apply the avoidance force to the original direction
            directionToPlayer += avoidanceDirection * avoidanceStrength;

            // Normalize the new direction to avoid movement being too fast
            directionToPlayer = directionToPlayer.normalized;
        }

        // Move the enemy towards the adjusted direction
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + directionToPlayer, speed * Time.deltaTime);
    }
}
