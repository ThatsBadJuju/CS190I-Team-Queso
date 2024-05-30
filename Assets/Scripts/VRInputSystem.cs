using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRInputSystem : MonoBehaviour
{
    private InputDevice targetDevice;
    public bool primaryButtonPressed;
    public bool primaryButtonDown;
    public bool secondaryButtonPressed;
    public bool secondaryButtonDown;
    public bool triggerPressed;
    public bool triggerDown;
    public bool gripPressed;
    public bool gripDown;
    public int touchpadDir;
    public bool touchpadUp;


    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics,devices);

        /*foreach (var item in devices)
        {

        }*/

        if(devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }

    // Update is called once per frame
    void Update()
    {



        if(targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
        {
            //pressing primary button

            //still on previous buttonpress bool
            if (!primaryButtonPressed)
            {
                primaryButtonDown = true;
            } else primaryButtonDown = false;

            primaryButtonPressed = true;
        }
        else
        {
            primaryButtonPressed = false;
            primaryButtonDown = false;
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButtonValue) && secondaryButtonValue)
        {
            //pressing secondary button

            //still on previous buttonpress bool
            if (!secondaryButtonPressed)
            {
                secondaryButtonDown = true;
            }
            else secondaryButtonDown = false;

            secondaryButtonPressed = true;
        }
        else
        {
            secondaryButtonPressed = false;
            secondaryButtonDown = false;
        }


        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f)
        {
            //pressing trigger button
            if (!triggerPressed)
            {
                triggerDown = true;
            }
            else triggerDown = false;

            triggerPressed = true;
        }
        else
        {
            triggerPressed = false;
            triggerDown = false;
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue) && gripValue > 0.1f)
        {
            //pressing trigger button
            if (!gripPressed)
            {
                gripDown = true;
            }
            else gripDown = false;

            gripPressed = true;
        }
        else
        {
            gripPressed = false;
            gripDown = false;
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue) && primary2DAxisValue.x != 0)
        {
            //Primary touchpad
            if (primary2DAxisValue.x > 0) touchpadDir = 1;
            else touchpadDir = -1;
        }
        else{
            if (touchpadDir != 0) touchpadUp = true;
            else
            {
                touchpadUp = false;
            }
            touchpadDir = 0;
        }

    }
}
