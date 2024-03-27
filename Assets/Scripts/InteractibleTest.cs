using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleTest : MonoBehaviour, IInteractable
{

    private InventoryManager inventoryManager;
    public Item item;

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

    // Method to handle the event
    
    private void HandleInventoryUpdate(InventoryManager invM)
    {
        // Do something with the GameObject received from the event
        
        inventoryManager = invM;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact()
    {
        if(gameObject.CompareTag("BackPack"))
        {
            Debug.Log("Backpack equipped");
            inventoryManager.haveBackpack = true;
            Destroy(gameObject);

        }
        else
        {
        Debug.Log("Interacting With : "+ this.gameObject.name);
        inventoryManager.AddItem(item);
        Destroy(gameObject);

        }
        
    }
}
