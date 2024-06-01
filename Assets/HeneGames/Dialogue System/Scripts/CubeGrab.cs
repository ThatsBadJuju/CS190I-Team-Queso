using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using HeneGames.DialogueSystem;

public class CubeGrab : MonoBehaviour
{
    // private DialogueManager dialogueManager;
    private XRGrabInteractable grabInteractable;

    private void Start()
    {
        // dialogueManager = FindObjectOfType<DialogueManager>(); // Find the DialogueManager in the scene
        grabInteractable = GetComponent<XRGrabInteractable>(); // Get the XRGrabInteractable component

        // if (grabInteractable != null)
        // {
            // grabInteractable.selectEntered.AddListener(OnGrab); // Add listener for the selectEntered event
        // }
    }

    // private void OnDestroy()
    // {
    //     // Clean up the listener when the object is destroyed to avoid memory leaks
    //     if (grabInteractable != null)
    //     {
    //         grabInteractable.selectEntered.RemoveListener(OnGrab);
    //     }
    // }

    // private void OnGrab(SelectEnterEventArgs args)
    // {
    //     if (dialogueManager != null && dialogueManager.IsProcessingDialogue())
    //     {
    //         dialogueManager.NextSentence();
    //     }
    // }
}
