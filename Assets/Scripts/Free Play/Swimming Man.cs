using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingMan : MonoBehaviour
{
    public Swimmer swimmer;
    private float timer = 0f;
    private float drownTimer = 0f;
    private bool decremented = false;
    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(10, 45);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }
        if (timer < 0f)
        {
            swimmer.setDrown(true);
            timer = 0f;
            drownTimer = 30f;
        }

        if (drownTimer > 0f)
        {
            drownTimer -= Time.deltaTime;
        }
        if (drownTimer < 0f)
        {
            drownTimer = 0;
            GameObject.Find("Scoreboard").GetComponent<Score>().fails++;
            GameObject.Find("Scoreboard text").GetComponent<WriteToScoreboard>().drownedTooLong++;
        }
    }

    public void Reset()
    {
        GameObject.Find("Scoreboard text").GetComponent<WriteToScoreboard>().drownedTooLong--;
        timer = Random.Range(30, 60);
    }
}

