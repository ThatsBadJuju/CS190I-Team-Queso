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
    public Waypoints[] waypoints;
    public DialogueUI dialogue;
    private bool whistleIncorrectlyBlown;
    float startTime;
    float currTime;


    // Start is called before the first frame update
    void Start()
    {
        originalLocalScale = GetComponent<Transform>().localScale;
        origPosition = transform.localPosition;
        origRotation = transform.localRotation;

        startTime = Time.time;
        currTime = Time.time;
        whistleIncorrectlyBlown = false;
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
        bool inArea = false;
        bool foundRunner = false;
        if(isInArea && area.activeSelf)
        {
            foreach (Waypoints waypoint in waypoints)
            {
                if (waypoint.isActiveAndEnabled && waypoint.isRunning())
                {
                    // do whistle action
                    waypoint.StartWalking();
                    isInArea = false;
                    inArea = true;

                    GameObject.Find("Whistle").GetComponent<Whistle>().score++;
                    foundRunner = true;
                }
            }
            if(!foundRunner) {
                whistleIncorrectlyBlown = true;

                GameObject.Find("Scoreboard").GetComponent<Score>().fails++;

            }
        }
        
        if(inArea)
        {
            dialogue.NextSentenceIfWhistle();
            area.GetComponent<WhistleArea>().ChangeColorRed();
            
        }
        ResetPosition();
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
    
    void Update() {
        
        if (whistleIncorrectlyBlown) {
            Debug.Log("UPDATING HELLO");
            if(currTime - startTime < 5) {
                GameObject.Find("Scoreboard").GetComponent<Score>().scoreText.text += "<b>Don't whistle if there are no runners!</b>\n";
                currTime = Time.time;
            } else {
                whistleIncorrectlyBlown = false;
            }

        } else {
            startTime = Time.time;
            currTime = Time.time;
        }
    }

}