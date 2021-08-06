using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChecklistManager : MonoBehaviour
{

    [SerializeField] GameObject batteryUpgradeCompleteText;
    [SerializeField] GameObject emitterUpgradeCompleteText;
    [SerializeField] GameObject powerUpgradeCompleteText;
    [SerializeField] GameObject correctExitCompleteText;

    [SerializeField] GameObject ChecklistCompletePanel;
    [SerializeField] GameObject thanksForPlayingPanel;
    

    private StoryManager storyManager;
    private MenuManager menuManager;

    //only reason the below GOs are handles here is that other managers exist in both map and stage states, but this is only map. 
    //& upgrade maanger is too crowded
    //explainer texts for powers
    [SerializeField] GameObject rainExplainerText;
    [SerializeField] GameObject delugeExplainerText;
    [SerializeField] GameObject stormExplainerText;
    [SerializeField] GameObject freezeExplainerText;


    // Start is called before the first frame update
    void Start()
    {
        storyManager = FindObjectOfType<StoryManager>();
        menuManager = FindObjectOfType<MenuManager>();
        UpdateCompletedItems();
    }

    // Update is called once per frame
    void Update()
    {        
        CheckForCompletedChecklist();
        UpdatePowerExplainerText();
    }

    public void UpdateCompletedItems()
    {
        if (storyManager.availableUpgradesArraySM[0] == 1 + storyManager.mapProgress)
        {
            emitterUpgradeCompleteText.SetActive(true);
            storyManager.checklistComplete[0] = true;
        }
        else if(storyManager.mapProgress == 3)
        {
            emitterUpgradeCompleteText.SetActive(true);
            storyManager.checklistComplete[0] = true;
        }
        else
        {
            emitterUpgradeCompleteText.SetActive(false);
            storyManager.checklistComplete[0] = false;
        }

        if (storyManager.availableUpgradesArraySM[2] >= 1 + storyManager.mapProgress)
        {
            batteryUpgradeCompleteText.SetActive(true);
            storyManager.checklistComplete[1] = true;
        }
        else
        {
            batteryUpgradeCompleteText.SetActive(false);
            storyManager.checklistComplete[1] = false;
        }

        if (storyManager.availableUpgradesArraySM[4 + 2 * storyManager.mapProgress] == 1)
        {
            powerUpgradeCompleteText.SetActive(true);
            storyManager.checklistComplete[2] = true;
        }
        else
        {
            powerUpgradeCompleteText.SetActive(false);
            storyManager.checklistComplete[2] = false;
        }

        if (storyManager.stageProgress == 5 + 5 * storyManager.mapProgress)
        {
            correctExitCompleteText.SetActive(true);
            storyManager.checklistComplete[3] = true;
        }
        else
        {
            correctExitCompleteText.SetActive(false);
            storyManager.checklistComplete[3] = false;
        }
    }

    private void CheckForCompletedChecklist()
    {
        if(storyManager.allChecklistItemsComplete && storyManager.stageProgress == 20)
        {
            menuManager.TurnOffMapButtonsAndMenu();
            thanksForPlayingPanel.SetActive(true);            
        }
        else if (storyManager.allChecklistItemsComplete)
        {
            menuManager.TurnOffMapButtonsAndMenu();
            ChecklistCompletePanel.SetActive(true);           
        }
    }

    public void GoToNextMap()
    {
        storyManager.mapProgress++;
        UpdateCompletedItems();
        menuManager.TurnOnMapButtons();
        ChecklistCompletePanel.SetActive(false);

    }

    private void UpdatePowerExplainerText()
    {
        if(storyManager.availableUpgradesArraySM[4] == 1)
        {
            rainExplainerText.SetActive(true);
        }
        if (storyManager.availableUpgradesArraySM[6] == 1)
        {
            delugeExplainerText.SetActive(true);
        }
        if (storyManager.availableUpgradesArraySM[8] == 1)
        {
            stormExplainerText.SetActive(true);
        }
        if (storyManager.availableUpgradesArraySM[10] == 1)
        {
            freezeExplainerText.SetActive(true);
        }
    }
}
