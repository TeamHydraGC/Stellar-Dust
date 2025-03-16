using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // Code should be ok. Testing has not been done yet.
    // - AJ

    // Array of locations to jump to
    public Vector3[] jumplocations;

    // Jumping variables
    public float jumpspeed = 1.0f;
    public float jumpheight = 1.0f;
    public float jumpinterval = 1.0f;

    void Start()
    {
        // Start the coroutine to jump to locations
        StartCoroutine(JumpToLocation());
    }

    public IEnumerator JumpToLocation()
    {
        // Loops forever, so the boss will keep jumping at the preset interval
        while (true)
        {
            yield return new WaitForSeconds(jumpinterval);

            // Select a random location to jump to based on the index of the array
            int randomIndex = Random.Range(0, jumplocations.Length);

            // Store the intended location
            Vector3 targetLocation = jumplocations[randomIndex];

            // Jump to the location
            yield return StartCoroutine(Jump(targetLocation));
        }
    }

    public IEnumerator Jump(Vector3 targetLocation)
    {
        // Store the current position of the boss
        Vector3 startPos = transform.position;
        // Calculate the distance between the start and target locations
        float journeyLength = Vector3.Distance(startPos, targetLocation);
        // Record the starting time
        float startTime = Time.time;

        // Keep moving until the boss is close enough to the target position
        while (Vector3.Distance(transform.position, targetLocation) > 0.1f)
        {
            // Calculate how much distance has been covered
            float distanceCovered = (Time.time - startTime) * jumpspeed;

            // Calculate the fraction of the journey completed
            float fractionOfJourney = distanceCovered / journeyLength;

            // Calculate the height of the jump using a sine wave
            float height = Mathf.Sin(fractionOfJourney * Mathf.PI) * jumpheight;
            Vector3 newPosition = Vector3.Lerp(startPos, targetLocation, fractionOfJourney);

            // Apply the jump height
            newPosition.y += height;

            // Update the position of the boss
            transform.position = newPosition;

            yield return null;
        }

        // Ensure the final position is exactly at the intended target
        transform.position = targetLocation;
    }
}
