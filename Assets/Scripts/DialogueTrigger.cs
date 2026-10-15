using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public DialogueLine[] sceneDialogue;
   

    private bool dialogueTriggered = false;

    // This runs automatically when something touches the trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the thing that touched it is the Player, and if we haven't already played it
        if (other.CompareTag("Player") && !dialogueTriggered)
        {
            dialogueTriggered = true;
            dialogueManager.StartDialogue(sceneDialogue);
        }
    }
}