using System.Collections;
using System.Collections.Generic;
using UnityEngine;


interface IInteractable
{
    public void Interact();
}
public class Interactor : MonoBehaviour
{
    public InputManager inputManager;
    public Transform InteractorSource;
    public float InteractRange;

    // Start is called before the first frame update
    void Start()
    {
        inputManager.inputMaster.Interaction.Use.started += _=> Use();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    void Use()
    {
        Ray r = new Ray(InteractorSource.position,InteractorSource.forward);
        if(Physics.Raycast(r,out RaycastHit hitinfo,InteractRange))
        {
            if(hitinfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                interactObj.Interact();

            }
            
        }
    }
}
