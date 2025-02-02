using UnityEngine;
using UnityEngine.SceneManagement;

public class WWtoWWbossScene : MonoBehaviour
{
    //Change from WildWest Scene to WildWestBoss Scene
    void OnCollisionEnter2D()
    {


        


        SceneManager.LoadScene(5);
    }
}
