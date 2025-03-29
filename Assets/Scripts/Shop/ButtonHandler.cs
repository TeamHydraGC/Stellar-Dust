using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    // Script authored by AJ.
    public Button damageButton; // Reference to the button
    private bool buttonPressed = false; // Ensure the button is pressed only once

    void Start()
    {
        // Make sure the button is not pressed initially
        damageButton.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        if (!buttonPressed)
        {
            buttonPressed = true;
            BulletScript[] bullets = FindObjectsOfType<BulletScript>();

            foreach (BulletScript bullet in bullets)
            {
                bullet.damageValue *= 2;
                Debug.Log("Bullet damage multiplied by 2!");
            }

            damageButton.interactable = false;
        }
    }
}
