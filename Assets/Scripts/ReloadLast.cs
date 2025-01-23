using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadLast : MonoBehaviour
{

    LastScene lastSceneIndex;
    int index;
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(sceneBuildIndex:1);
        }
    }
}
