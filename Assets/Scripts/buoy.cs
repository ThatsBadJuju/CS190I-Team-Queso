using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeneGames.DialogueSystem;

public class buoy : MonoBehaviour
{
    // Start is called before the first frame update
    public int score;

    public Vector3 originalLocalScale;
    public GameObject[] areas;
    public bool[] isInArea; //defaults to false
    public Vector3 origPosition;
    public Quaternion origRotation;
    public DialogueUI dialogue;
    public MenuBuoy menuBuoy;
    public Swimmer[] swimmers;

    private float buoyTimer;

    void Start()
    {
        originalLocalScale = GetComponent<Transform>().localScale;
        origPosition = transform.localPosition;
        origRotation = transform.localRotation;

        score = 0;

        buoyTimer = 0f;
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
        if(buoyTimer > 0f)
        {
            buoyTimer -= Time.deltaTime;
        }
        if (buoyTimer < 0f)
        {
            ResetPosition();
            //GetComponent<Rigidbody>().useGravity = false;
            Freeze();
            for(int i = 0; i < areas.Length; i++) 
            {
                GameObject area = areas[i];
                area.SetActive(false);
                area.GetComponent<BuoyArea>().ChangeColorRed();
                isInArea[i] = false;
            }
            menuBuoy.inMenu = true;
            buoyTimer = 0f;
        }

        for (int i = 0; i < swimmers.Length; i++)
        {
            Swimmer swimmer = swimmers[i];
            GameObject area = areas[i];
            if (isInArea[i] && area.activeSelf)
            {
                dialogue.NextSentenceIfBuoy();
                swimmer.setDrown(false);
                score += 1;
                Debug.Log("SCORE: " + score);
                ResetPosition();
                //GetComponent<Rigidbody>().useGravity = false;
                Freeze();
                area.SetActive(false);
                menuBuoy.inMenu = true;

                area.GetComponent<BuoyArea>().ChangeColorRed();
                isInArea[i] = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "buoy_area")
        {
            GameObject area = other.gameObject;
            area.GetComponent<BuoyArea>().ChangeColorGreen();
            for(int i = 0; i < areas.Length; i++)
            {
                if(areas[i].GetInstanceID() == area.GetInstanceID())
                {
                    isInArea[i] = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "buoy_area")
        {
            GameObject area = other.gameObject;
            area.GetComponent<BuoyArea>().ChangeColorRed();
            for (int i = 0; i < areas.Length; i++)
            {
                if (areas[i].GetInstanceID() == area.GetInstanceID())
                {
                    isInArea[i] = false;
                }
            }
        }
    }

    
    void ChangeColor()
    {
        //GetComponent<Renderer>().material.color = Color.red;
        Debug.Log("Picked Up");
        dialogue.NextSentenceIfBuoyGrab();
        for (int i = 0; i < swimmers.Length; i++)
        {
            Swimmer swimmer = swimmers[i];
            GameObject area = areas[i];
            if (swimmer.drown)
            {
                area.SetActive(true);
                Debug.Log("Set Area " + i + " to drowning");
            }
        }
        GetComponent<Rigidbody>().useGravity = true;
        UnFreeze();
        menuBuoy.inMenu = false;
        buoyTimer = 0f;
    }

    void ChangeColorBack()
    {
        //GetComponent<Renderer>().material.color = Color.blue;
        Debug.Log("Dropped");
        buoyTimer = 3f;
        //if (isInArea)
        //{
        //    Debug.Log("success");
        //    dialogue.NextSentenceIfBuoy();
        //    score += 1;
        //    Debug.Log("SCORE: " + score);
        //    ResetPosition();
        //}
        //else
        //{
        //    // return to starting point
        //    ResetPosition();
        //}
        //GetComponent<Rigidbody>().useGravity = false;
        //Freeze();
        //area.SetActive(false);
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
