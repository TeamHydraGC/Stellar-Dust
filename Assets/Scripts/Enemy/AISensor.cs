using UnityEngine;
using System.Collections.Generic;

public class AISensor : MonoBehaviour
{
    // READ BEFORE CHANGING!!!!

    // In order for the script to work:

    // The object that the AI is sensing [moving towards] must be on the same LAYER as the AI is. Both objects in the TestingScene are on the Default layer, for reference.
    // Objects NOT in the same layer will block the AI's FOV (The blocker object in the TestingScene, which is on a different layer.)

    // SETUP:
    // 1. Set the layer you want the AI to scan.
    // 2. Set the layer you want the AI to use as a blocker [the "occlusionlayers" variable in the Inspector.
    // 3. In the Objects dropdown menu, click the "+" and click and drag the GameObject which you want the AI to be able to scan. The AI will always move towards the GameObject first in order.

    // HOW TO REMOVE THE COLORS:
    // Open the MeshColor tab in the Inspector and drag the bottom-most slider to the left [this sets the opacity to 0].

    // https://www.youtube.com/results?search_query=line%20of%20sight%20enemy%20ai%20detection
    // Partial credit excluding some of the bottom-most code goes to @TheKiwiCoder on YouTube for the guide video.

    // Adjustable aspects of the "cone" the AI uses to detect what is in its FOV
    public float distance = 10;
    public float angle = 30;
    public float height = 1.0f;

    // Sets movement speed
    public float moveSpeed = 5f;

    // Sets color
    public Color meshColor = Color.red;

    public int scanfrequency = 30;
    public LayerMask layers;
    public LayerMask occlusionlayers;
    public List<GameObject> Objects = new List<GameObject>();

    Collider[] colliders = new Collider[50];

    Mesh mesh;
    int count;
    float scaninterval;
    float scantimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scaninterval = 1.0f / scanfrequency;
    }

    // Update is called once per frame
    void Update()
    {
        scantimer -= Time.deltaTime;
        if (scantimer < 0)
        {
            scantimer += scaninterval;
            Scan();
        }

        MoveTowardsTarget();
    }

    // Makes the AI scan for something in its FOV.
    private void Scan()
    {
        count = Physics.OverlapSphereNonAlloc(transform.position, distance, colliders, layers, QueryTriggerInteraction.Collide);

        Objects.Clear();
        for (int i = 0; i < count; ++i)
        {
            GameObject obj = colliders[i].gameObject;
            if (IsInSight(obj))
            {
                Objects.Add(obj);
            }
        }
    }

    // Checks if something is in sight.
    public bool IsInSight(GameObject obj)
    {
        Vector3 origin = transform.position;
        Vector3 dest = obj.transform.position;
        Vector3 direction = dest - origin;

        if (direction.y < 0 || direction.y > height)
        {
            return false;
        }

        direction.y = 0;
        float deltaAngle = Vector3.Angle(direction, transform.forward);
        if (deltaAngle > angle)
        {
            return false;
        }

        origin.y += height / 2;
        if (Physics.Linecast(origin, dest, occlusionlayers))
        {
            return false;
        }
        return true;
    }

    // DO NOT TOUCH. Creates the mesh needed to do FOV scanning.
    Mesh CreateWedgeMesh()
    {
        Mesh mesh = new Mesh();
        int segments = 10;
        int numTriangles = (segments * 4) + 2 + 2;
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

        //left side
        vertices[vert++] = BottomCentre;
        vertices[vert++] = Bottomleft;
        vertices[vert++] = Topleft;

        vertices[vert++] = Topleft;
        vertices[vert++] = Topcentre;
        vertices[vert++] = BottomCentre;


        //right side
        vertices[vert++] = BottomCentre;
        vertices[vert++] = Topcentre;
        vertices[vert++] = Topright;

        vertices[vert++] = Topright;
        vertices[vert++] = Bottomright;
        vertices[vert++] = BottomCentre;

        float currentangle = -angle;
        float deltaangle = (angle * 2) / segments;
        for (int i = 0; i < segments; i++)
        {
            Bottomleft = Quaternion.Euler(0, currentangle, 0) * Vector3.forward * distance;
            Bottomright = Quaternion.Euler(0, currentangle + deltaangle, 0) * Vector3.forward * distance;

            Topright = Bottomright + Vector3.up * height;
            Topleft = Bottomleft + Vector3.up * height;

            //far side
            vertices[vert++] = Bottomleft;
            vertices[vert++] = Bottomright;
            vertices[vert++] = Topright;

            vertices[vert++] = Topright;
            vertices[vert++] = Topleft;
            vertices[vert++] = Bottomleft;

            //top
            vertices[vert++] = Topcentre;
            vertices[vert++] = Topleft;
            vertices[vert++] = Topright;

            //bottom
            vertices[vert++] = BottomCentre;
            vertices[vert++] = Bottomright;
            vertices[vert++] = Bottomleft;

            currentangle += deltaangle;
        }


        for (int i = 0; i < numVertices; i++)
        {
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }

    private void OnValidate()
    {
        mesh = CreateWedgeMesh();
        scaninterval = 1.0f / scanfrequency;
    }

    private void OnDrawGizmos ()
    {
        // DO NOT DELETE THIS.
        if (mesh)
        {
            Gizmos.color = meshColor;
            Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
        }        
        
        // This makes a green circle indicating that the object can be seen. Can be removed to hide the circle.
        Gizmos.DrawWireSphere(transform.position, distance);
        for (int i = 0; i < count; i++)
        {
            Gizmos.DrawSphere(colliders[i].transform.position, 0.2f);
        }

        // This makes a green circle indicating that the object can be seen. Can be removed to hide the circle.
        Gizmos.color = Color.green;
        foreach (var obj in Objects)
        {
            Gizmos.DrawSphere(obj.transform.position, 0.2f);
        }
    }

    private void MoveTowardsTarget()
    {
        // Check if there are any objects detected in sight
        if (Objects.Count > 0)
        {
            // Pick the first object (you can also modify this to pick the closest one, etc.)
            GameObject target = Objects[0];

            // Calculate the direction to move towards the target.
            Vector3 direction = target.transform.position - transform.position;

            // Moves the object towards the target.
            if (direction.magnitude > 0.1f)
            {
                // Normalize the direction vector and move the object.
                Vector3 moveDirection = direction.normalized;
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
            }
        }
    }
}
