using UnityEngine;
using System.Collections.Generic;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public DialogueLine[] sceneDialogue;

    [Header("Dialogue Replay")]
    public bool playOnlyOnce = true;

    [Tooltip("Type a unique name here (like 'Level1_Intro') so the game can remember it across reloads!")]
    public string dialogueID;
    private bool dialogueTriggered = false;
    private static HashSet<string> playedDialogues = new HashSet<string>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if it's the Player and hasn't triggered locally
        if (other.CompareTag("Player") && !dialogueTriggered)
        {
            // If it's set to play once, check the global memory list
            if (playOnlyOnce && !string.IsNullOrEmpty(dialogueID) && playedDialogues.Contains(dialogueID))
            {
                return; // Silently skip if we already played this ID before dying/reloading
            }

            dialogueTriggered = true;

            // Add this specific ID to the permanent memory
            if (playOnlyOnce && !string.IsNullOrEmpty(dialogueID))
            {
                playedDialogues.Add(dialogueID);
            }

            if (dialogueManager != null)
            {
                dialogueManager.StartDialogue(sceneDialogue);
            }
            else if (DialogueManager.Instance != null)
            {
                DialogueManager.Instance.StartDialogue(sceneDialogue);
            }
        }
    }
}