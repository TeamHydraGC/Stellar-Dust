using UnityEngine;

public class ScoreTest : MonoBehaviour
{
    public int scoreValue = 1;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerScore.Instance.IncreaseScore(scoreValue);
    }
}
