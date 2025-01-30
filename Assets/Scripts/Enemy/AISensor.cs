using UnityEngine;

public class AISensor : MonoBehaviour
{
    public float distance = 10;
    public float angle = 30;
    public float height = 1.0f;

    //https://www.youtube.com/results?search_query=line%20of%20sight%20enemy%20ai%20detection
    // Youtube link. 3:10 is where you were at last.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Mesh CreateWedgeMesh()
    {
        Mesh mesh = new Mesh();
        int numTriangles = 8;
        int numVertices = numTriangles * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 BottomCentre = Vector3.zero;
        Vector3 Bottomleft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance;
        Vector3 Bottomright = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;

        Vector3 Topcentre = BottomCentre + Vector3.up * height;
        Vector3 Topright = Bottomright + Vector3.up * height;
        Vector3 Topleft = Bottomleft + Vector3.up * height;

        int vert = 0;

        return mesh;
    }
}
