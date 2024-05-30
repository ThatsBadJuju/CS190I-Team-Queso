using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NarrationNode
{
    public string line;
    
    public string[] choice_lines;
    public NarrationNode[] choices;
}
