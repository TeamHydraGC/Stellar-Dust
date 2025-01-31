using Unity.VisualScripting;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Check if entered collision box
    public void OnCollisionEnter2D(Collision2D collision)
    {

        transform.position = new Vector3(0, -5, 0);
        
        
        
        //Destroy(gameObject);

    }




}
