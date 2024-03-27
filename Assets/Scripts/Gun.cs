using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public UnityEvent onGunShoot;
    public InputManager inputManager;
    public float FireCooldown;
    
    private float currentCooolDown;

    private InventoryManager inventoryManager;
    public Item item;
    public bool GunEquipped = false;

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
        currentCooolDown = FireCooldown;
        //inputManager.inputMaster.Interaction.Shoot.started += _=> Shoot();
        
    }

    // Update is called once per frame
    void Update()
    {
        checkGunEquipped();
        Shoot();
        
        
    }

    private void checkGunEquipped()
    {
        item = inventoryManager.GetSelectedItem(false);
        if(item.actionType == Item.ActionType.Shoot)
        {
            GunEquipped = true;

        }
        
    }

    void Shoot()
    {
        if(GunEquipped)
        {
            if(item.Automatic)
            {

                    if (inputManager.inputMaster.Interaction.Shoot.ReadValue<float>() > 0)
                    {
                        //Debug.Log("Shoot input detected");
                        if(currentCooolDown <= 0f)
                        {
                            onGunShoot?.Invoke();
                            currentCooolDown = FireCooldown;
                        }

                    
                    }
            }
            else
            {
                if (inputManager.inputMaster.Interaction.Shoot.ReadValue<float>() == 1)
                    {
                        Debug.Log("Shoot input 2 detected");
                        if(currentCooolDown <= 0f)
                        {
                            onGunShoot?.Invoke();
                            currentCooolDown = FireCooldown;
                        }
                    
                    }



            }  
            currentCooolDown -= Time.deltaTime;      
            }


    }
}