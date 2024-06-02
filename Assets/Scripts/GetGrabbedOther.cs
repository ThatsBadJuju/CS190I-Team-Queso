using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GetGrabbedOther : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private bool grabbed = false;

    private Rigidbody body;
    private float origDrag;
    private float origAngDrag;

    // Start is called before the first frame update
    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        body = GetComponent<Rigidbody>();
        origDrag = body.drag;
        origAngDrag = body.angularDrag;
    }

    // Update is called once per frame
    void Update()
    {
        if (grabInteractable.isSelected)
        {
            if (!grabbed)
            {
                grabbed = true;
            }
        }
        else
        {
            if (grabbed)
            {
                grabbed = false;
                body.drag = origDrag;
                body.angularDrag = origAngDrag;
            }
        }
    }
}
