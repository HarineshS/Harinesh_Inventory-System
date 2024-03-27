using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public InputManager inputManager;
    public Rigidbody rb;
    public float Speed= 10;
    public float runSpeed= 15;
    public float jumpForce = 200;
    bool isGrounded;
    private GameObject BackPack;
    private InventoryManager inventoryManager;
    public Transform handSocketTransform;

    private void Start()
    {
        inputManager.inputMaster.Movement.Jump.started += _=> Jump();
        inputManager.inputMaster.Interaction.Drop.started += _=> Drop();
        BackPack.SetActive(false);
    }

    private void OnEnable()
    {
        // Subscribe to the BackPackUpdate event
        BackPackBroadcaster.OnBackPackUpdate += HandleBackPackUpdate;
        InventoryManager.OnInventoryUpdate += HandleInventoryUpdate;

    }

    private void OnDisable()
    {
        // Unsubscribe from the BackPackUpdate event
        BackPackBroadcaster.OnBackPackUpdate -= HandleBackPackUpdate;
        InventoryManager.OnInventoryUpdate -= HandleInventoryUpdate;
    }

    // Method to handle the event
    private void HandleBackPackUpdate(GameObject obj)
    {
        // Do something with the GameObject received from the event
        Debug.Log("Received Backpack update from: " + obj.name);
        BackPack = obj;
        
    }
    private void HandleInventoryUpdate(InventoryManager invM)
    {
        // Do something with the GameObject received from the event
        
        inventoryManager = invM;
        
    }

    


    void FixedUpdate()
    {
        Move();




        
    }

    private void Update() {
        
        
        if (inputManager.inputMaster.Interaction.Backpack.ReadValue<float>() == 0)
        {
            BackPack.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            if(inventoryManager.haveBackpack)
            {
                BackPack.SetActive(true);
                Cursor.lockState = CursorLockMode.None;

            }
            
        }
        
        
        
            
        
    }
    void Drop()
    {
        inputManager.inventoryManager.GetSelectedItem(true);
    }

    
    void Move()   
    {
        float forward = inputManager.inputMaster.Movement.Forward.ReadValue<float>();
        float right = inputManager.inputMaster.Movement.Right.ReadValue<float>();
        Vector3 move = transform.right*right + transform.forward *forward;


        move *= inputManager.inputMaster.Movement.Sprint.ReadValue<float>()==0?Speed : runSpeed;
        transform.localScale = new Vector3(1,inputManager.inputMaster.Movement.Crouch.ReadValue<float>()==0? 1f:0.72f,1);
        rb.velocity = new Vector3(move.x,rb.velocity.y,move.z);

    }








    void Jump()
    {
        if(isGrounded)
        {
            rb.AddForce(Vector3.up*jumpForce);
            Debug.Log("Jumping");
        }

    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.transform.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision other) 
    {
        if(other.transform.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
