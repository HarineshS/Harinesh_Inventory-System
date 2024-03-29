using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public TMP_Text questStateText;

    // Example quest states
    private bool foundObject1 = false;
    private bool foundObject2 = false;
    private bool foundObject3 = false;
    private bool foundObject4 = false;
    private int questnumber = 0;
    public delegate void QuestUpdate(QuestManager questManager);
    public static event QuestUpdate OnQuestUpdate;

    public static QuestManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize quest state text
        //UpdateQuestStateText();
    }

    // Update is called once per frame
    void Update()
    {
        if(OnQuestUpdate != null)
        {
            OnQuestUpdate(QuestManager.instance);
        }
    }

    void UpdateQuestStateText()
    {
        // Construct a string based on quest states
        string questState = "Quest "+questnumber+" :\n";
        questState += "Find a Torch.. : " + (foundObject1 ? "Complete" : "Incomplete") + "\n";
        questState += "Find Pistol :" + (foundObject2 ? "Complete" : "Incomplete")+ "\n";
        questState += "Find Gold Key :" + (foundObject3 ? "Complete" : "Incomplete") + "\n";
        questState += "Loot the Chest  :" + (foundObject4 ? "Complete" : "Incomplete");

        // Update the TMP Text element
        questStateText.text = questState;
    }

    
    public void FoundObject1()
    {
        foundObject1 = true;
        UpdateQuestStateText();
    }

    
    public void FoundObject2()
    {
        foundObject2 = true;
        UpdateQuestStateText();
    }
    public void FoundObject3()
    {
        foundObject3 = true;
        UpdateQuestStateText();
    }
    public void FoundObject4()
    {
        foundObject4 = true;
        UpdateQuestStateText();
        checkcompletion();
    }

    public void checkcompletion()
    {
        if(foundObject1&&foundObject2&&foundObject3&&foundObject4)
        {
            questStateText.text = "All Quests completed..! Congratulations";
        }
    }

    
    

    
    public void StartQuest1()
    {
        Debug.Log("Quest 1 started!");
        questnumber =1;
        UpdateQuestStateText();
        
    }

    public void StartQuest2()
    {
        Debug.Log("Quest 2 started!");
        questnumber =2;
        UpdateQuestStateText();
        
    }
}
