using System.Drawing.Text;
using System.Security.Cryptography;
using UnityEngine;

public class MovingDoor : MonoBehaviour
{

    [SerializeField]
    GameObject door;

    public Vector3 doorPos;
    bool moveBack = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        doorPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveBack)
        {

            if (transform.position.y < doorPos.y)
            {
                transform.Translate(0, 0.01f, 0);
            }
            else
            {
                moveBack = false;
            }
        }


    }
}

