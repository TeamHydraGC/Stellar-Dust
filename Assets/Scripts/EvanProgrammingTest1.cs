using UnityEngine;

public class EvanTest : MonoBehaviour
{
    // Circle
    int circleRadius = 5;

    // Rectangle
    int rectWidth = 2;
    int rectHeight = 3;

    // Triangle
    int triBase = 3;
    int triHeight = 5;

    // Order Floats
    float rectOrder = 3;
    float triOrder = 3;
    float circleOrder = 3;
    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
     void Start()
     {

        // Areas
        var rectArea = rectWidth * rectHeight;
        var circleArea = circleRadius*circleRadius*3.14;
        var triArea = triBase*triHeight/2;

        // Area Calculation Displays
        //Debug.Log("Circle Area = " +  circleArea);

        //Debug.Log("Rectangle Area = " + rectArea);

        //Debug.Log("Triangle Area = " +  triArea);


        // if Statements
        // Rectangle Order
        if (rectArea >= triArea)
        {
            rectOrder -= 1;
        }

        if (rectArea >= circleArea)
        {
            rectOrder -= 1;
        }

        // Triangle Order
        if (triArea >= rectArea)
        {
            triOrder -= 1;
        }

        if (triArea >= circleArea)
        {
            triOrder -= 1;
        }

        // Circle Order
        if (circleArea >= rectArea)
        {
            circleOrder -= 1;
        }

        if (circleArea >= triArea)
        {
            circleOrder -= 1;
        }

        // Order Display
        Debug.Log("Circle Place = " + circleOrder);
        Debug.Log("Triangle Place = " + triOrder);
        Debug.Log("Rectangle Place = " + rectOrder);
        

     }


}
