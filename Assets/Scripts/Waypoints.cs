using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public GameObject[] waypoints;
    private int current = 0;
    private float rotSpeed;
    public float walkSpeed = 2f;
    public float runSpeed = 6f; // Speed for running
    private float speed;
    private float WPradius = 1;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the GameObject.");
        }
        speed = walkSpeed; // Start with walking speed
        UpdateAnimator();
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

        UpdateAnimator();
    }

    public void StartWalking()
    {
        Debug.Log("Start Walking");
        speed = walkSpeed;
        UpdateAnimator();
    }

    public void StartRunning()
    {
        Debug.Log("Start Running");
        speed = runSpeed;
        UpdateAnimator();
    }

    void UpdateAnimator()
    {
        if (animator != null)
        {
            //Debug.Log("Updating Animator with Speed: " + speed);
            animator.SetFloat("Speed", speed);
        }
    }
}
