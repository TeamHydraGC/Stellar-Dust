using UnityEngine;
using UnityEngine.SceneManagement;

public class jptwoTOjpthree : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Change the scene to the outdoors
        
        if (collision.transform.name == "Player")
        {
            SceneManager.LoadScene(9);
        }
        



    }
}
