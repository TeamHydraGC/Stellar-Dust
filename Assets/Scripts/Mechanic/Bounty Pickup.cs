using JetBrains.Annotations;
using UnityEngine;
using Yarn.Unity;

public class BountyPickup : MonoBehaviour
{
    public void Update()
    {

        



        if (Input.GetMouseButtonDown(1))
        {


            Debug.Log("Picked up bounty!!!!!");






        }






    }

    public void OnCollisionStay(Collision collision)
    {

        Debug.Log("Picked up bounty!");

        if (Input.GetMouseButtonDown(1) == true)
        {


            Debug.Log("Picked up bounty!!!");






        }




    }



    
}
