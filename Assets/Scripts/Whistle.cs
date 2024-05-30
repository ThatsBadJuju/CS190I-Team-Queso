using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whistle : MonoBehaviour
{
    public delegate void WhistleGrabbedEventHandler();
    public static event WhistleGrabbedEventHandler OnWhistleGrabbed;

    void OnEnable()
    {
        getGrabbed.OnGrabbed += HandleGrabbed;
    }

    void OnDisable()
    {
        getGrabbed.OnGrabbed -= HandleGrabbed;
    }

    private void HandleGrabbed()
    {
        Debug.Log("Whistle grabbed!");
        OnWhistleGrabbed?.Invoke();
    }
}
