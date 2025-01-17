using UnityEngine;

public class SkelBirdScript : MonoBehaviour
{
    //This script ISN'T FINISHED!!! Don't touch!
    //Due to the script being unfinished, it stops you from opening the game sim in Unity; will uncomment this when it's done.
    // - AJ


    // ** public Transform[] points;
    // 3 points (platforms) for boss to move to
    // ** public float BossMovementSpeed;
    // ** private float t = 0f;
    // Time variable
    // ** private bool moving = true;
    // ** public AnimationCurve parabolacurve;
    // Parabola movement pattern, as in an "arc"

    void Update()
    {
        //if (moving)
        //{
            //t += time.deltaTime * BossMovementSpeed;
            //if (t > 1f)
            //{
                //t = 1f;
                //moving = false;
                // Makes boss stop when it reaches one of the points
            //}
            //Vector3 position = GetParabolicPosition(t);
            //transform.position = position;
        //}

        // Meat and potatoes below;
        //Vector3 GetParabolicPosition(float t)
        //{
            //Vector3 pt0 = points[0].position;
            //Vector3 pt1 = points[1].position;
            //Vector3 pt2 = points[2].position;

            //More to be added beneath with time
        //}
    }
}
