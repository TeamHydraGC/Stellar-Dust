using UnityEngine;
using UnityEngine.SceneManagement;

public class jponeTOjptwo : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player") 
        {
            SceneManager.LoadScene(5); 
        }
    }
}