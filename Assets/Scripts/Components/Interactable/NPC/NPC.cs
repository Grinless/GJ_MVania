using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RepeatableInteracter))]
public class NPC : MonoBehaviour, IInteractionResult
{
    public DialogueDisplay dialogueDisplay;
    public string dialogue;

    public void CompleteInteraction()
    {
        if (dialogueDisplay.Open)
            return;
        else
        {
            dialogueDisplay.ShowDialogue(dialogue, true, 2f);
            StartCoroutine(Close());
        }
    }

    private IEnumerator Close()
    {
        while (dialogueDisplay.Completing)
        {
            yield return new WaitForSeconds(0.25f);
        }
        dialogueDisplay.HideDialogue();
    }
}