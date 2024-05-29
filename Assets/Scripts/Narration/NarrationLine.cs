using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NarrationLine : MonoBehaviour
{
    
    public Vector3 location;
    public Vector3 rotation;

    public GameObject gameObj;
    public TMP_Text text_line;
    public NarrationNode narration_line;

    // Start is called before the first frame update

    void Start()
    {
        text_line.fontSize = 50;
    }

    // Update is called once per frame
    void Update()
    {
        text_line.text = narration_line.line;
    }
}
