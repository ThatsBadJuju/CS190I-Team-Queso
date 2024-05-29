using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class score : MonoBehaviour
{
    buoy buoy; // Reference to the Text component in Unity Editor
    int totalScore;
    TextMeshProUGUI scoreText;
    void Start()
    {
        buoy = GameObject.Find("Lifeguard_Buoy").GetComponent<buoy>();
        totalScore = 0;
        scoreText = GetComponent<TextMeshProUGUI>();
        
    }
    void Update() {
        totalScore = buoy.score * 3;
        string totalText = "Score: " + 5 * buoy.score + "\n";
        string buoyText = "Buoys: " + buoy.score + "\n";
        scoreText.text = totalText + buoyText;

    }
}

