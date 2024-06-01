using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBuoy : MonoBehaviour
{

    public Transform menuTransform;
    public Transform buoyTransform;
    public bool inMenu;

    private Vector3 startingPosition;
    private Quaternion startingRotation;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = buoyTransform.position - menuTransform.position;
        startingRotation = menuTransform.rotation;
        inMenu = true;
    }

    // Update is called once per frame
    void Update()
    {

        if(inMenu)
        {
            buoyTransform.position = menuTransform.position + startingPosition;
            buoyTransform.rotation = startingRotation;
        }
        
    }
}
