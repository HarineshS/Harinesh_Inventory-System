
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public UnityEngine.UI.Image image;
    public Color SelectedColor,NotSelectedColor;
    

    private void Awake()
    {
        Deselect();
    }

    public void Select()
    {
        image.color = SelectedColor; 
    }
    public void Deselect()
    {
        image.color = NotSelectedColor; 
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            draggableItem draggableItem = dropped.GetComponent<draggableItem>();
            draggableItem.parentAfterDrag = transform;

        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
