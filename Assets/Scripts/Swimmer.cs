using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swimmer : MonoBehaviour
{
    private Animator animator;
    bool drown;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the GameObject.");
        }
        drown = false; // Start with walking speed
        

        // StartCoroutine(ToggleDrown());
    }
    void Update() {
        UpdateAnimator();
    }
    // IEnumerator ToggleDrown()
    // {
    //     while (true)
    //     {
    //         // Wait for 5 seconds
    //         yield return new WaitForSeconds(5);

    //         // // Toggle drown
    //          drown = !drown;

    //         // Update the animator
    //         UpdateAnimator();
    //     }
    // }

    public void setDrown(bool set) {
        drown = set;
    }

    void UpdateAnimator()
    {
        if (animator != null)
        {
            // Debug.Log("Updating Animator with Drown: " + drown);
            animator.SetBool("drown", drown);
        }
    }
}
