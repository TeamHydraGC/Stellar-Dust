using UnityEngine;

public class PlayerAreaTrigger : MonoBehaviour
{
    public TMPro.TextMeshProUGUI titleText;
    public TMPro.TextMeshProUGUI byLineText;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        AreaTrigger area = collision.GetComponent<AreaTrigger>();

        if (area == null)
        {
            return;
        }
        
        titleText.text = area.title;
        byLineText.text = "By: " + area.author;

    }

}
