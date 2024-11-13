using System.Drawing.Text;
using System.Security.Cryptography;
using UnityEngine;

public class MovingDoor : MonoBehaviour
{

    [SerializeField]
    GameObject door;

    bool isOpened = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isOpened)
        {
            isOpened = true;
            door.transform.position += new Vector3(0, 4, 0);
        }
    }




}

