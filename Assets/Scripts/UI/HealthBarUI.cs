// Made By: Vinny
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarImage;
    public PlayerHealth playerHealth;

    void Start()
    {
        healthBarImage.fillAmount = (float)playerHealth.playerHealth / playerHealth.playerMaxHealth;
    }

    void Update()
    {
        healthBarImage.fillAmount = (float)playerHealth.playerHealth / playerHealth.playerMaxHealth;
    }
}
