using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteractible : MonoBehaviour, IInteractable
{
    public Animator chestAnimator; // Reference to the Animator component
    private bool isOpen = false; 
    private InventoryManager inventoryManager;
    public Item key;
    

    private void OnEnable()
    {
        // Subscribe to the BackPackUpdate event
        InventoryManager.OnInventoryUpdate += HandleInventoryUpdate;
        

    }

    private void OnDisable()
    {
        // Unsubscribe from the BackPackUpdate event
        InventoryManager.OnInventoryUpdate -= HandleInventoryUpdate;
        
    }
    
    private void HandleInventoryUpdate(InventoryManager invM)
    {
        // Do something with the GameObject received from the event
        
        inventoryManager = invM;
        
    }

    public void Interact()
    {
        if (!isOpen && inventoryManager.CheckItemInInventory(key))
        {
            OpenChest();
            inventoryManager.GetSelectedItem(true);

        }
        else
        {
            //CloseChest();
        }
    }

    void OpenChest()
    {
        // Play the "Open" animation
        chestAnimator.SetTrigger("Open");
        isOpen = true;
        Debug.Log("Chest opened");
    }

    void CloseChest()
    {
        // Play the "Close" animation (if needed)
        // chestAnimator.SetTrigger("Close");
        isOpen = false;
        Debug.Log("Chest closed");
    }
}
