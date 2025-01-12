using JetBrains.Annotations;
using UnityEngine;

public class BountyPickup : MonoBehaviour
{

    //private var mouseClick = Input.GetMouseButton(1);




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnMouseEnter()
    {
        
        if  (Input.GetMouseButton(1) == true)
        {
            Debug.Log("Picked up bounty!");
        }






    }








}
