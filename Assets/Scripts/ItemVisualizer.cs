using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemVisualizer : MonoBehaviour
{

    private InventoryManager inventoryManager;
    private Item item;
    [SerializeField]
    private GameObject[] itemslots;

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
        int childcount = transform.childCount;
        for(int i=0;i<childcount;i++)
        {
            itemslots[i] = transform.GetChild(i).gameObject;
            

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        updateItemInHand();

        
        
        
    }

    void updateItemInHand()
{
    if(inventoryManager!= null)
    {
    item = inventoryManager.GetSelectedItem(false);

    if (item != null)
    {
        for (int i = 0; i < itemslots.Length; i++)
        {
            if ((i + 1) == item.id)
            {
                itemslots[i].SetActive(true);
            }
            else
            {
                itemslots[i].SetActive(false);
            }
        }
    }
    else
    {
        // Handle the case when the item is null
       
        foreach (var slot in itemslots)
        {
            slot.SetActive(false);
        }
    }

    }
    
}

}
