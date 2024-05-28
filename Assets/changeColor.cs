using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColor : MonoBehaviour
{
    public Vector3 originalLocalScale;
    public GameObject area;
    public bool isInArea = false;

    // Start is called before the first frame update
    void Start()
    {
        originalLocalScale = GetComponent<Transform>().localScale;
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
        area.SetActive(true);
        //TODO: turn on gravity
    }

    void ChangeColorBack()
    {
        //GetComponent<Renderer>().material.color = Color.blue;
        //Debug.Log("Dropped");
        if (isInArea)
        {
            // do whistle action
            Debug.Log("success");
        }
        else
        {
            // return to starting point
        }
        //TODO: turn off gravity
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
}