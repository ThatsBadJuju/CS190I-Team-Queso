using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeneGames.DialogueSystem;

public class changeColor : MonoBehaviour
{
    public Vector3 originalLocalScale;
    public GameObject area;
    public bool isInArea = false;
    public Vector3 origPosition;
    public Quaternion origRotation;
    public Waypoints waypoints;
    public DialogueUI dialogue;

    // Start is called before the first frame update
    void Start()
    {
        originalLocalScale = GetComponent<Transform>().localScale;
        origPosition = transform.localPosition;
        origRotation = transform.localRotation;
    }

    // Update is called once per frame
    void OnEnable()
    {
        getGrabbed.OnGrabbed += ChangeColor;
        getGrabbed.OnReleased += ChangeColorBack;
    }

    void OnDisable()
    {
        getGrabbed.OnGrabbed -= ChangeColor;
        getGrabbed.OnReleased -= ChangeColorBack;
    }

    void ChangeColor()
    {
        //GetComponent<Renderer>().material.color = Color.red;
        //Debug.Log("Picked Up");
        dialogue.NextSentenceIfWhistleGrab();
        area.SetActive(true);
        GetComponent<Rigidbody>().useGravity = true;
        UnFreeze();
    }

    void ChangeColorBack()
    {
        //GetComponent<Renderer>().material.color = Color.blue;
        //Debug.Log("Dropped");
        if (isInArea)
        {
            // do whistle action
            dialogue.NextSentenceIfWhistle();
            Debug.Log("success");
            waypoints.StartWalking();
            ResetPosition();
        }
        else
        {
            // return to starting point
            ResetPosition();
        }
        GetComponent<Rigidbody>().useGravity = false;
        Freeze();
        area.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "whistle_area")
        {
            area.GetComponent<WhistleArea>().ChangeColorGreen();
            isInArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "whistle_area")
        {
            area.GetComponent<WhistleArea>().ChangeColorRed();
            isInArea = false;
        }
    }

    private void ResetPosition()
    {
        transform.localPosition = origPosition;
        transform.localRotation = origRotation;
    }
    private void Freeze()
    {
        GetComponent<Rigidbody>().constraints = 
            RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ |
            RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY |
            RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX
            ;
    }

    private void UnFreeze()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}