using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneChange : MonoBehaviour
{
    
    //Change from Tutorial Scene to Wild West Scene
    void OnCollisionEnter2D()
    {


        


        SceneManager.LoadScene(4);
    }


}
