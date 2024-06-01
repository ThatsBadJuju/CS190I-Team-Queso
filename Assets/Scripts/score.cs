using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class Score : MonoBehaviour
{
    buoy buoy; // Reference to the Text component in Unity Editor
    Whistle whistle;
    int totalScore;
    //used in whistles
    public int fails;
    public TextMeshProUGUI scoreText;
    void Start()
    {
        buoy = GameObject.Find("Menu_Buoy").GetComponent<buoy>();
        whistle = GameObject.Find("Whistle").GetComponent<Whistle>();
        totalScore = 0;
        fails = 0;
        scoreText = GetComponent<TextMeshProUGUI>();
    }
    void Update() {
        totalScore = buoy.score * 5 + whistle.score * 2 - fails;
        string totalText = "<b>Score: </b>" + totalScore + "\n";
        string buoyText = "<size=75%><b>Buoys: </b>" + buoy.score + "</size>\n";
        string whistleText = "<size=75%><b>Whistles: </b>" + whistle.score + "</size>\n\n";
        scoreText.text = totalText + buoyText + whistleText;

    }
}

