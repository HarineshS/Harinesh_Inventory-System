using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public Item[] StartItems;
    public int MaxStackSize;
    public InventorySlot[] inventorySlots; 
    public GameObject inventoryItemPrefab;
    int selectedSlot = -1;

    private void Awake()
    {
        instance = this;
    }

    private void Start() 
    {
        ChangeSelectedSlot(0);
        foreach(var item in StartItems)
        {
            AddItem(item);
        }
    }
    private void Update() 
    {
        if(Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString,out int number);
            if(isNumber && number > 0 && number <9)
            {
                ChangeSelectedSlot(number - 1);
                
            }
        }

        
    }

    void ChangeSelectedSlot(int newValue)
    {
        if(selectedSlot>=0)
        {
            inventorySlots[selectedSlot].Deselect();

        }
        
        inventorySlots[newValue].Select();
        selectedSlot = newValue;

    }
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


    public Item GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        draggableItem itemInSlot = slot.GetComponentInChildren<draggableItem>();
        if(itemInSlot != null)
        {
            Item item = itemInSlot.item;
            if(use==true)
            {
                itemInSlot.count--;
                if(itemInSlot.count<=0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.RefreshCount();

                }

            }
            return item;

        }
        return null;
    }

    public void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemObject = Instantiate(inventoryItemPrefab,slot.transform);
        draggableItem inventoryItem = newItemObject.GetComponent<draggableItem>();
        inventoryItem.InitializeItem(item);

    }
}
