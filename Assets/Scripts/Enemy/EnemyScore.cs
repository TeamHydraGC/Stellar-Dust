// Authored by Nate
// ENSURE EnemyScore.cs RUNS BEFORE EnemyHealth.cs IN UNITY'S SCRIPT EXECUCTION ORDER

using Unity.VisualScripting;
using UnityEngine;

public class EnemyScore : MonoBehaviour
{
    public void Update()
    {
        if (EnemyHealth.Instance.enemyHealth <= 0)
        {
            PlayerScore.Instance.currentScore += EnemyHealth.Instance.scoreValue;
        }
    }
}
