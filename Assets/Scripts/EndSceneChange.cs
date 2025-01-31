using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneChange : MonoBehaviour
{
    
    void OnCollisionEnter2D()
    {
        SceneManager.LoadScene(4);
    }


}
