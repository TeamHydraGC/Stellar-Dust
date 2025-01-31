using UnityEngine;
using UnityEngine.SceneManagement;

public class jponeTOjptwo : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Change the scene to the outdoors
        SceneManager.LoadScene(7);
        


    }


}
