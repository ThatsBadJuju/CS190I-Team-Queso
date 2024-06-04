using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningMan : MonoBehaviour
{
    public Waypoints waypoints;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(15, 30);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0f)
        {
            timer -= Time.deltaTime;
        }
        if(timer < 0f)
        {
            waypoints.StartRunning();
            timer = 0f;
        }
    }

    public void Reset()
    {
        timer = Random.Range(15, 45);
    }
}
