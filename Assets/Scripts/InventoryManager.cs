using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int MaxStackSize;
    public InventorySlot[] inventorySlots; 
    public GameObject inventoryItemPrefab;
    public bool AddItem(Item item)
    {
        for(int i=0;i< inventorySlots.Length;i++)
        {
            InventorySlot slot = inventorySlots[i];
            draggableItem itemInSlot = slot.GetComponentInChildren<draggableItem>(); 
            if(itemInSlot != null && itemInSlot.item == item && itemInSlot.count<MaxStackSize && itemInSlot.item.stackable == true)
            {

                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;

            }
        }



        for(int i=0;i< inventorySlots.Length;i++)
        {
            InventorySlot slot = inventorySlots[i];
            draggableItem itemInSlot = slot.GetComponentInChildren<draggableItem>(); 
            if(itemInSlot == null)
            {
                SpawnNewItem(item,slot);
                return true;

            }
        }

        return false;

    }

    public void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemObject = Instantiate(inventoryItemPrefab,slot.transform);
        draggableItem inventoryItem = newItemObject.GetComponent<draggableItem>();
        inventoryItem.InitializeItem(item);

    }
}
