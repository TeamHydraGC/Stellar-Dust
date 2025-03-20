using UnityEngine;

// By Vinny
public class GoreToggle : MonoBehaviour
{
    public static bool goreEnabled = true;

    public void ToggleGore(bool isOn)
    {
        goreEnabled = isOn;
        // Debug.Log("Gore enabled: " + goreEnabled);
    }
}
