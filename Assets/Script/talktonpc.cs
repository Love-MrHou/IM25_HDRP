using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkToNPC : MonoBehaviour
{
    public GameObject uiElement; // Assign the UI element you want to show in the Inspector

    // Start is called before the first frame update
    void Start()
    {
        // Hide the UI element at the start
        uiElement.SetActive(false);
    }

    // When the camera enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Show the UI element
            uiElement.SetActive(true);
        }
    }

    // When the camera exits the trigger collider
    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Hide the UI element
            uiElement.SetActive(false);
        }
    }
}
