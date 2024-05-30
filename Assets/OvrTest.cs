using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OvrTest : MonoBehaviour
{
    public XRController rightHand;
    public GameObject currentInteractor;
    public InputHelpers.Button button;

    void Update()
    {

        if (OVRInput.Get(OVRInput.Button.One)){
            Debug.Log("A button pressed");
        }

    }
}
