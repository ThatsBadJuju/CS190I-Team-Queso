using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeguardInteraction : MonoBehaviour
{
    public GameObject popupMessageUI; // references the actual popup message ui

    private void Start()
    {
        popupMessageUI.SetActive(false); // initially have popup be invis
    }

    private void OnTriggerEnter(Collider other)
    {
        // if player hits the collider, trigger the popup to appear
        if (other.CompareTag("Player"))
        {
            popupMessageUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            popupMessageUI.SetActive(false);
        }
    }
}