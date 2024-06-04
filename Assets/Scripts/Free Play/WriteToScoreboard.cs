using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WriteToScoreboard : MonoBehaviour
{
    public int drownedTooLong;
    // Start is called before the first frame update
    void Start()
    {
        drownedTooLong = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (drownedTooLong > 0)
        {
            GameObject.Find("Scoreboard").GetComponent<Score>().scoreText.text += "<b>Save the drowning person!</b>\n";
            //Debug.Log("drowned!");
        }
    }
}
