using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeneGames.DialogueSystem;

public class buoy : MonoBehaviour
{
    // Start is called before the first frame update
    public int score;

    public Vector3 originalLocalScale;
    public GameObject area;
    public bool isInArea = false;
    public Vector3 origPosition;
    public Quaternion origRotation;
    public DialogueUI dialogue;

    void Start()
    {
        originalLocalScale = GetComponent<Transform>().localScale;
        origPosition = transform.localPosition;
        origRotation = transform.localRotation;

        score = 0;
        
    }

    void OnEnable()
    {
        GetGrabbedBuoy.OnGrabbed += ChangeColor;
        GetGrabbedBuoy.OnReleased += ChangeColorBack;
    }

    void OnDisable()
    {
        GetGrabbedBuoy.OnGrabbed -= ChangeColor;
        GetGrabbedBuoy.OnReleased -= ChangeColorBack;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "buoy_area")
        {
            area.GetComponent<BuoyArea>().ChangeColorGreen();
            isInArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "buoy_area")
        {
            area.GetComponent<BuoyArea>().ChangeColorRed();
            isInArea = false;
        }
    }

    
    void ChangeColor()
    {
        //GetComponent<Renderer>().material.color = Color.red;
        //Debug.Log("Picked Up");
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
            Debug.Log("success");
            dialogue.NextSentenceIfBuoy();
            score += 1;
            Debug.Log("SCORE: " + score);
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