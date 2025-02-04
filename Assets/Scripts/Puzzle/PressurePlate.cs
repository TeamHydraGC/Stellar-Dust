using System.Drawing.Text;
using System.Security.Cryptography;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{

    public Vector3 originalPos;
    bool moveBack = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveBack)
        {

            if (transform.position.y < originalPos.y)
            {
                transform.Translate(0, 0.01f, 0);
            }
            else
            {
                moveBack = false;
            }
        }
 
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.name == "Player")
        {

            GetComponent<Transform>().position += new Vector3 (0, -0.1f, 0);
            moveBack = false;
        }

        if (collision.transform.name == "PushableCrate")
        {

            transform.Translate(0, -0.02f, 0);
            moveBack = false;
        }


    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Player")
        {
            collision.transform.parent = transform;
            GetComponent<SpriteRenderer>().color = Color.red;
        }

        if (collision.transform.name == "PushableCrate")
        {
            collision.transform.parent = transform;
            GetComponent<SpriteRenderer>().color = Color.red;
        }


    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        moveBack = true;
        collision.transform.parent = null;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

}

