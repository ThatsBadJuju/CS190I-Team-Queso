using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whistle : MonoBehaviour
{
    public delegate void WhistleGrabbedEventHandler();
    public static event WhistleGrabbedEventHandler OnWhistleGrabbed;
    public int score;
    public AudioSource audioSource;

    void Start() {
        score = 0;
    }

    void OnEnable()
    {
        getGrabbed.OnGrabbed += HandleGrabbed;
    }

    void OnDisable()
    {
        getGrabbed.OnGrabbed -= HandleGrabbed;
    }

    private void HandleGrabbed()
    {
        Debug.Log("Whistle grabbed!");
        OnWhistleGrabbed?.Invoke();
    }
}
