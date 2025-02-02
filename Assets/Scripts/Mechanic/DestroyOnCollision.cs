using UnityEngine;
using System.Collections;
//THIS SCRIPT NEEDS TO BE REMOVED AND ENEMY HEALTH NEEDS TO BE REFACTORED
public class DestroyBall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}