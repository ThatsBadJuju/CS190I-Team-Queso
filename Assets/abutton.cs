using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class abutton : MonoBehaviour
{
     private XRGrabInteractable grabInteractable;
    private bool isGrabbed = false;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        isGrabbed = true;
        Debug.Log("Object grabbed!");
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        isGrabbed = false;
        Debug.Log("Object released!");
    }

    public bool IsGrabbed()
    {
        return isGrabbed;
    }
}
