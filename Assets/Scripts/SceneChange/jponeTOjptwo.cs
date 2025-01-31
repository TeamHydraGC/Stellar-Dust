using UnityEngine;
using UnityEngine.SceneManagement;

public class jponeTOjptwo : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {

            SceneManager.LoadScene(7);

        }


    }


}
