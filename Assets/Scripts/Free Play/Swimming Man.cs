using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingMan : MonoBehaviour
{
    public Swimmer swimmer;
    private float timer = 0f;
    private float drownTimer = 0f;
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
            drownTimer = 5f;
        }

        //if(drownTimer > 0f)
        //{
        //    drownTimer -= Time.deltaTime;
        //}
        //if(drownTimer < 0f)
        //{
        //    Time.timeScale = 0.001f;
        //}
    }

    public void Reset()
    {
        timer = Random.Range(20, 60);
    }
}
