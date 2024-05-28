using UnityEngine;

public class score : MonoBehaviour
{
    private GUIStyle boldStyle;
    private GUIStyle normalStyle;
    buoy buoy; // Reference to the Text component in Unity Editor
    int totalScore;
    void Start()
    {
        buoy = GameObject.Find("Lifeguard_Buoy").GetComponent<buoy>();
        totalScore = 0;
        
    }

    void OnGUI()
    {
        // Define the width and height of the box
        // Initialize the GUIStyle for bold text
        boldStyle = new GUIStyle(GUI.skin.label);
        boldStyle.fontStyle = FontStyle.Bold;
        boldStyle.fontSize = 20; // Adjust the font size as needed

        // Initialize the GUIStyle for normal text
        normalStyle = new GUIStyle(GUI.skin.label);
        normalStyle.fontSize = 16; // Adjust the font size as needed

        float boxWidth = 150f;
        float boxHeight = 150f;

        // Calculate the x and y positions for the bottom-right corner
        float xPosition = Screen.width - boxWidth - 10; // 10 units from the right edge
        float yPosition = Screen.height - boxHeight - 10; // 10 units from the bottom edge

        // Create the GUI box
        GUI.Box(new Rect(xPosition, yPosition, boxWidth, boxHeight), GUIContent.none);
        //+ other scores as well
        totalScore = buoy.score * 5;
        // Draw the bold "Score" text
        GUI.Label(new Rect(xPosition + 10, yPosition + 10, boxWidth - 20, 40), "Score: " + totalScore, boldStyle);

        // Draw the normal text below "Score"
        GUI.Label(new Rect(xPosition + 10, yPosition + 40, boxWidth - 20, 30), "Buoys: " + buoy.score, normalStyle);
        GUI.Label(new Rect(xPosition + 10, yPosition + 70, boxWidth - 20, 30), "Whistled: ", normalStyle);
        GUI.Label(new Rect(xPosition + 10, yPosition + 100, boxWidth - 20, 30), "Saves: ", normalStyle);


    }
}
