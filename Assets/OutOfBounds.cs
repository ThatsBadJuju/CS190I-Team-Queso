using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerZone : MonoBehaviour
{
    public TextMeshProUGUI messageText;

    public string enterMessage = "A lifeguard should be near the pool and attentive at all times!";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DisplayMessage(enterMessage);
        }
    }
    
    private void DisplayMessage(string message)
    {
        if (messageText != null)
        {
            messageText.text = message;
        }
        else
        {
            //Debug.LogWarning("Message Text is not assigned.");
        }
    }
}
