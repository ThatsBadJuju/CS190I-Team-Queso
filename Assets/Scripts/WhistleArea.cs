using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhistleArea : MonoBehaviour
{
    public getGrabbed whistleGrabbed;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = new Color(1f,0f,0f,0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColorGreen()
    {
        GetComponent<Renderer>().material.color = new Color(0f, 1f, 0f, 0.5f);
    }

    public void ChangeColorRed()
    {
        GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f, 0.5f);
    }
}
