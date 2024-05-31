using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion.Fluid;

public class Swimmer : MonoBehaviour
{
    public ComplexFluidInteractor fluidInteractor;
    private Animator animator;
    public bool drown;

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
        if (set)
        {
            fluidInteractor.floatStrength = 0.5f;
        }
        else
        {
            fluidInteractor.floatStrength = 2.0f;
        }
    }

    void UpdateAnimator()
    {
        if (animator != null)
        {
            //Debug.Log("Updating Animator with Drown: " + drown);
            animator.SetBool("drown", drown);
        }
    }
}
