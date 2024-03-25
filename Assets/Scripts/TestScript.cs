using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;

    public void PickupItem(int id)
    {
       bool result = inventoryManager.AddItem(itemsToPickup[id]);
       if(result == true)
       {
        Debug.Log("Item added to inventory");
       }
       else
       {
        Debug.Log("Item not added to inventory");

       }
    }


    public void GetSelectedItem()
    {
        Item recievedItem = inventoryManager.GetSelectedItem(false);
        if(recievedItem != null)
        {
            Debug.Log("Recieved : "+ recievedItem);
            
        }
        else
        {
            Debug.Log("Item Not Recieved");

        }
    }

    public void UseSelectedItem()
    {
        Item recievedItem = inventoryManager.GetSelectedItem(true);
        if(recievedItem != null)
        {
            Debug.Log("Used : "+ recievedItem);
            
        }
        else
        {
            Debug.Log("Item Not Recieved");

        }
    }
}
