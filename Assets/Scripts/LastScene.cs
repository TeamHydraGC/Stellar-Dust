using UnityEngine;
using UnityEngine.SceneManagement;

public class LastScene : MonoBehaviour
{
    public void LastSceneIndex()
    {
        int lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Last scene has an index value of " + lastSceneIndex);
    }
}
