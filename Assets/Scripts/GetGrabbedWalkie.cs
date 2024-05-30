using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GetGrabbedWalkie : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private bool grabbed = false;
    public delegate void Grabbed();
    public delegate void Released();
    public static event Released OnReleased;
    public static event Grabbed OnGrabbed;

    // Start is called before the first frame update
    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (grabInteractable.isSelected)
        {
            if (!grabbed)
            {
                //Debug.Log("Grabbed");
                if (OnGrabbed != null) OnGrabbed();
                grabbed = true;
            }
        }
        else
        {
            if (grabbed)
            {
                //Debug.Log("Released");
                if (OnReleased != null) OnReleased();
                grabbed = false;
            }
        }
    }
}
