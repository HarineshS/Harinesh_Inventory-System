using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance; // Singleton instance

    // Initial items to populate inventory
    public Item[] StartItems;

    // Maximum stack size for items
    public int MaxStackSize;

    // Array of inventory slots
    public InventorySlot[] inventorySlots;

    // Prefab for inventory items
    public GameObject inventoryItemPrefab;

    private GameObject playerObject; // Reference to the player object
    int selectedSlot = -1; // Currently selected inventory slot

    public bool haveBackpack = false;
    // Event for inventory update
    public delegate void InventoryUpdate(InventoryManager inventoryManager);
    public static event InventoryUpdate OnInventoryUpdate;

    private void Awake()
    {
        instance = this; // Assign instance on Awake
    }

    private void Start() 
    {
        PlayerBroadcaster.OnPlayerUpdate += HandlePlayerUpdate; // Subscribe to player update event
        
        // Set the first slot as selected and add starting items
        ChangeSelectedSlot(0);
        foreach(var item in StartItems)
        {
            AddItem(item);
        }
    }

    // Handle player update event
    void HandlePlayerUpdate(GameObject obj)
    {
        playerObject = obj; // Update player object reference
    }

    void OnDestroy()
    {
        PlayerBroadcaster.OnPlayerUpdate -= HandlePlayerUpdate; // Unsubscribe from player update event on destruction
    }

    private void Update() 
    {
        // Handle input for selecting inventory slots
        if(Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString,out int number);
            if(isNumber && number > 0 && number <9)
            {
                ChangeSelectedSlot(number - 1);
            }
        }

        // Trigger inventory update event
        if (OnInventoryUpdate != null)
        {
            OnInventoryUpdate(InventoryManager.instance);
        }
    }

    // Change selected inventory slot
    void ChangeSelectedSlot(int newValue)
    {
        if(selectedSlot>=0)
        {
            inventorySlots[selectedSlot].Deselect(); // Deselect previous slot
        }
        
        inventorySlots[newValue].Select(); // Select new slot
        selectedSlot = newValue;
    }

    

    public bool AddItem(Item item)
        {
            int slotlength;
            if (!haveBackpack)
            {
                slotlength = 8;
            }
            else
            {
                slotlength = inventorySlots.Length;
            }

            bool hasWeapon = false;

            // Check existing slots for stackable items and weapons
            for (int i = 0; i < slotlength; i++)
            {
                InventorySlot slot = inventorySlots[i];
                draggableItem itemInSlot = slot.GetComponentInChildren<draggableItem>();
                
                if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < MaxStackSize && itemInSlot.item.stackable)
                {
                    // Increment stack count and refresh display
                    itemInSlot.count++;
                    itemInSlot.RefreshCount();
                    return true;
                }

                if (itemInSlot != null && itemInSlot.item.type == Item.ItemType.Weapon)
                {
                    hasWeapon = true;
                }
            }

            // If a weapon is already in inventory, throw a log message
            if (hasWeapon && item.type == Item.ItemType.Weapon)
            {
                Debug.Log("A weapon is already in inventory, remove it to pick a new one");
                return false;
            }

            // If no stackable slot found, look for empty slot to spawn new item
            for (int i = 0; i < slotlength; i++)
            {
                InventorySlot slot = inventorySlots[i];
                draggableItem itemInSlot = slot.GetComponentInChildren<draggableItem>();
                if (itemInSlot == null)
                {
                    // Spawn new item in the empty slot
                    SpawnNewItem(item, slot);
                    return true;
                }
            }

            return false; // Inventory full, unable to add item
        }


    // Get selected item from the inventory
    public Item GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        draggableItem itemInSlot = slot.GetComponentInChildren<draggableItem>();
        if(itemInSlot != null)
        {
            Item item = itemInSlot.item;
            if(use==true)
            {
                if(item.actionType == Item.ActionType.Consume)
                {
                    itemInSlot.count--;
                    if(itemInSlot.count<=0)
                    {
                        Destroy(itemInSlot.gameObject);
                    }
                    else
                    {
                        itemInSlot.RefreshCount(); // Refresh count display
                    }
                    return item;

                }
                // Instantiate item in the scene
                Instantiate(item.Asset,playerObject.transform.position,Quaternion.identity);
                // Decrease item count and destroy if count reaches zero
                itemInSlot.count--;
                if(itemInSlot.count<=0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.RefreshCount(); // Refresh count display
                }
            }
            return item;
        }
        else
        {
            return null;

        }
        
        //return null;
    }

    public bool CheckItemInInventory(Item item)
    {
        int slotLength;
        if (!haveBackpack)
        {
            slotLength = 8;
        }
        else
        {
            slotLength = inventorySlots.Length;
        }

        for (int i = 0; i < slotLength; i++)
        {
            InventorySlot slot = inventorySlots[i];
            draggableItem itemInSlot = slot.GetComponentInChildren<draggableItem>();

            // Check if itemInSlot is not null before accessing its properties
            if (itemInSlot != null && itemInSlot.item != null && itemInSlot.item.id == item.id)
            {
                return true;
            }
        }

        // If the item was not found in any inventory slot, return false
        return false;
    }



    // Spawn a new item in the specified inventory slot
    public void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemObject = Instantiate(inventoryItemPrefab,slot.transform);
        draggableItem inventoryItem = newItemObject.GetComponent<draggableItem>();
        inventoryItem.InitializeItem(item);
    }
}
