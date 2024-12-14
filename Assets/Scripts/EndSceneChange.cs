using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneChange : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnCollisionEnter2D()
    {
        SceneManager.LoadScene(1);
    }


}
