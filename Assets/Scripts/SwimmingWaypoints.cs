using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingWaypoints : MonoBehaviour
{
    public GameObject[] waypoints;
    public SwimmingMan swimmingMan;
    private int current = 0;
    private float rotSpeed;
    public float speed;
    private float WPradius = 1;

    void Start()
    {
        speed = 1;
    }

    void Update()
    {
        if (Vector3.Distance(waypoints[current].transform.position, transform.position) < WPradius)
        {
            current++;
            if (current >= waypoints.Length)
            {
                current = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
        transform.LookAt(waypoints[current].transform.position);
    }

}
