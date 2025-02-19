using UnityEngine;
using UnityEngine.SceneManagement;

public class WWbossToWW : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Player")
        {
            SceneManager.LoadScene(4);
        }


    }
}
