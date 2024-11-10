using UnityEngine;
using Yarn.Unity;

public class NPC_Interaction : MonoBehaviour
{
    public GameObject interactPrompt;
    public string startNode = "";
    public DialogueRunner runner;
    bool canInteract = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactPrompt.SetActive(false);
        runner = FindAnyObjectByType<DialogueRunner>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canInteract)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                runner.StartDialogue(startNode);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();

        if(player != null)
        {
            interactPrompt.SetActive(true);
            canInteract = true;
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();

        if (player != null)
        {
            interactPrompt.SetActive(false);
            canInteract = false;
            runner.Stop();
        }

    }
}
