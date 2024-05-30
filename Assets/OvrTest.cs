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

        if (currentInteractor.GetComponent<ActionBasedController>().activateAction.action.ReadValue<float>() > 0.5f)
        {
            Debug.Log("trigger pressed");
        }
    }
}
