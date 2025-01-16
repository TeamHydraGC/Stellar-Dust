using UnityEngine;

public class RotatorIHardlyKnowHer : MonoBehaviour
{
    public float rotationSpeed = 1000f;  // Degrees per second

    void Update()
    {
        // Rotate the object around its Y-axis at the specified speed
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
