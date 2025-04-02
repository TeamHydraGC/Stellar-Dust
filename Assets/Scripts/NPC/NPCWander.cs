// Made By Vinny
// Reference: NightRunStudio (https://www.youtube.com/watch?v=bj_tJMiUut0)
using UnityEngine;
using System.Collections;

public class NPCWander : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float leftPatrolX, rightPatrolX;

    [SerializeField] private float minPauseTime, maxPauseTime;
    [SerializeField] private float minWalkTime, maxWalkTime;

    [SerializeField] private int facingDirection = -1;

    private float randomTime, timer;
    private bool isWalking = true;
    public bool isFlipping;
    public Animator animator;
    public bool ismoving = false;

    private void Start()
    {
        randomTime = Random.Range(minWalkTime, maxWalkTime);
    }

    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= randomTime)
            StateChange();

        if (!isFlipping && (transform.position.x > rightPatrolX || transform.position.x < leftPatrolX))
            StartCoroutine(Flip());

        if (isWalking)
            rb.linearVelocity = new Vector2(facingDirection * speed, rb.linearVelocity.y);
            //rb.linearVelocity = new Vector2(2, 0);




        if (isWalking == true)
      

        // animator.SetBool("ismoving", true);

        if (isWalking == false)

          animator.SetBool("ismoving", false);

        // Debug.Log($"NPC Position: {transform.position.x}, Left Patrol: {leftPatrolX}, Right Patrol: {rightPatrolX}");
    }

    IEnumerator Flip()
    {
        isFlipping = true;
        transform.Rotate(0, 180, 0);
        facingDirection *= -1;
        yield return new WaitForSeconds(2.0f);
        isFlipping = false;
    }

    IEnumerator Moving()
    {

        Vector3 positioncheck1 = transform.position;
        yield return new WaitForSeconds(1f);
        Vector3 positioncheck2 = transform.position;


        if (positioncheck1 == positioncheck2)
        {
            ismoving = false;
        }
        else
        {
            ismoving = true;
        }

    }

    void StateChange()
    {
        isWalking = !isWalking;
        randomTime = isWalking ? Random.Range(minWalkTime, maxWalkTime) : Random.Range(minPauseTime, maxPauseTime);
        timer = 0;
    }
}
