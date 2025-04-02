// Made By Vinny
// Reference: NightRunStudio (https://www.youtube.com/watch?v=bj_tJMiUut0)
using UnityEngine;
using System.Collections;

public class NPCWanderNinja : MonoBehaviour
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
            //rb.linearVelocity = new Vector2(facingDirection * speed, rb.linearVelocity.y);
            rb.linearVelocity = new Vector2(2, 0);




    

    }

    IEnumerator Flip()
    {
        isFlipping = true;
        transform.Rotate(0, 180, 0);
        facingDirection *= -1;
        yield return new WaitForSeconds(2.0f);
        isFlipping = false;
    }

  

    void StateChange()
    {
        isWalking = !isWalking;
        randomTime = isWalking ? Random.Range(minWalkTime, maxWalkTime) : Random.Range(minPauseTime, maxPauseTime);
        timer = 0;
    }
}
