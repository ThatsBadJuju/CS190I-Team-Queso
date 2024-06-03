using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTeleport : MonoBehaviour
{
    float playerHeight;
    public GameObject XR_Rig;
    public bool teleportationEnabled;
    // Start is called before the first frame update
    void Start()
    {
        playerHeight = 3.54f; // change if necessary
    }

    // Update is called once per frame
    void Update()
    {
        if(teleportationEnabled)
        {
            XR_Rig.transform.position = new Vector3(XR_Rig.transform.position.x, playerHeight, XR_Rig.transform.position.z);
        }
    }

    public void enableTeleportation()
    {
        teleportationEnabled = true;
        XR_Rig.transform.position = new Vector3(2.0f, playerHeight, -9.0f);
    }

    public void disableTeleportation()
    {
        teleportationEnabled = false;
    }
}
