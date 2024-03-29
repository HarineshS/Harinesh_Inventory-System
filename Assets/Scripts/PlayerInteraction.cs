using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private QuestManager questManager;
    private InventoryManager inventoryManager;
    public Item item1, item2,item3,item4;




    private void OnEnable()
    {
        // Subscribe to the QuestUpdate event
        QuestManager.OnQuestUpdate += HandleQuestUpdate;
        InventoryManager.OnInventoryUpdate += HandleInventoryUpdate;
        

    }

    private void OnDisable()
    {
        // Unsubscribe from the BackPackUpdate event
        QuestManager.OnQuestUpdate -= HandleQuestUpdate;
        InventoryManager.OnInventoryUpdate -= HandleInventoryUpdate;
        
    }
    
    private void HandleQuestUpdate(QuestManager qM)
    {
        // Do something with the GameObject received from the event
        
        questManager = qM;
        
    }

    private void HandleInventoryUpdate(InventoryManager invM)
    {
        // Do something with the GameObject received from the event
        
        inventoryManager = invM;
        
    }

    private void Start() 
    {
        

        
    }

    void Update()
    {
        if (inventoryManager == null || questManager == null)
        {
            Debug.LogWarning("InventoryManager or QuestManager is not assigned!");
            return;
        }

        if (inventoryManager.CheckItemInInventory(item1))
        {
            questManager.FoundObject1();
        }

        if (inventoryManager.CheckItemInInventory(item2))
        {
            questManager.FoundObject2();
        }

        if (inventoryManager.CheckItemInInventory(item3))
        {
            questManager.FoundObject3();
        }
        if (inventoryManager.CheckItemInInventory(item4))
        {
            questManager.FoundObject4();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("QuestTrigger1"))
        {
            questManager.StartQuest1();
        }
        else if (other.CompareTag("QuestTrigger2"))
        {
            questManager.StartQuest2();
        }
    }
}
