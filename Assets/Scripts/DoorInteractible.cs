using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractible : MonoBehaviour, IInteractable
{
    public bool isOpen = false;
    

    public void Interact()
    {
        if (!isOpen)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    void OpenDoor()
    {
        // Rotate the door model to open it
        transform.Rotate(0f, -90f, 0f);
        isOpen = true;
        Debug.Log("Door opened");
    }

    void CloseDoor()
    {
        // Rotate the door model to close it
        transform.Rotate(0f, 90f, 0f);
        isOpen = false;
        Debug.Log("Door closed");
    }
}
