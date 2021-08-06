using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    //resourceTag CO, UO, RO, CI, UI, RI
    public int commonOrganic;
    public int uncommonOrganic;
    public int rareOrganic;
    public int commonInorganic;
    public int uncommonInorganic;
    public int rareInorganic;

    [SerializeField] TextMeshProUGUI commonOrganicText;
    [SerializeField] TextMeshProUGUI uncommonOrganicText;
    [SerializeField] TextMeshProUGUI rareOrganicText;
    [SerializeField] TextMeshProUGUI commonInorganicText;
    [SerializeField] TextMeshProUGUI uncommonInorganicText;
    [SerializeField] TextMeshProUGUI rareInorganicText;

    //managers
    private StoryManager storyManager;

    // Start is called before the first frame update
    void Start()
    {
        storyManager = FindObjectOfType<StoryManager>();
        SetUpResources();        

        /* 
        * AddResource("OR", "C", 20);
        AddResource("IN", "C", 20);
        AddResource("OR", "U", 20);
        AddResource("IN", "U", 20);
        */
    }

    // Update is called once per frame
    void Update()
    {

        commonOrganicText.text = commonOrganic.ToString();
        uncommonOrganicText.text = uncommonOrganic.ToString();
        rareOrganicText.text = rareOrganic.ToString();
        commonInorganicText.text = commonInorganic.ToString();
        uncommonInorganicText.text = uncommonInorganic.ToString();
        rareInorganicText.text = rareInorganic.ToString();

        UpdateStoryManagerResrouceTracker();

    }

    private void SetUpResources()
    {
        commonOrganic = storyManager.availableResourcesArraySM[0];
        uncommonOrganic = storyManager.availableResourcesArraySM[1];
        rareOrganic = storyManager.availableResourcesArraySM[2];
        commonInorganic = storyManager.availableResourcesArraySM[3];
        uncommonInorganic = storyManager.availableResourcesArraySM[4];
        rareInorganic = storyManager.availableResourcesArraySM[5];
    }

    private void UpdateStoryManagerResrouceTracker()
    {
        if(commonOrganic != storyManager.availableResourcesArraySM[0])
        {
            storyManager.availableResourcesArraySM[0] = commonOrganic;
        }
        if(uncommonOrganic != storyManager.availableResourcesArraySM[1])
        {
            storyManager.availableResourcesArraySM[1] = uncommonOrganic;
        }
        if(rareOrganic != storyManager.availableResourcesArraySM[2])
        {
            storyManager.availableResourcesArraySM[2] = rareOrganic;
        }
        if(commonInorganic != storyManager.availableResourcesArraySM[3])
        {
            storyManager.availableResourcesArraySM[3] = commonInorganic;
        }
        if(uncommonInorganic != storyManager.availableResourcesArraySM[4])
        {
            storyManager.availableResourcesArraySM[4] = uncommonInorganic;
        }
        if(rareInorganic != storyManager.availableResourcesArraySM[5])
        {
            storyManager.availableResourcesArraySM[5] = rareInorganic;
        }
    }

    public void ReduceResource(string resourceTypeTag, string resourceRarityTag, int reductionAmount)
    {
        if(resourceTypeTag == "OR")
        {
            if (resourceRarityTag == "C")
            {
                commonOrganic -= reductionAmount;
            }
            if (resourceRarityTag == "U")
            {
                uncommonOrganic -= reductionAmount;
            }
            if (resourceRarityTag == "R")
            {
                rareOrganic -= reductionAmount;
            }
        }
        if (resourceTypeTag == "IN")
        {
            if (resourceRarityTag == "C")
            {
                commonInorganic -= reductionAmount;
            }
            if (resourceRarityTag == "U")
            {
                uncommonInorganic -= reductionAmount;
            }
            if (resourceRarityTag == "R")
            {
                rareInorganic -= reductionAmount;
            }
        }
    }

    public void AddResource(string resourceTypeTag, string resourceRarityTag, int additionAmount)
    {
        if (resourceTypeTag == "OR")
        {
            if (resourceRarityTag == "C")
            {
                commonOrganic += additionAmount;
            }
            if (resourceRarityTag == "U")
            {
                uncommonOrganic += additionAmount;
            }
            if (resourceRarityTag == "R")
            {
                rareOrganic += additionAmount;
            }
        }
        if (resourceTypeTag == "IN")
        {
            if (resourceRarityTag == "C")
            {
                commonInorganic += additionAmount;
            }
            if (resourceRarityTag == "U")
            {
                uncommonInorganic += additionAmount;
            }
            if (resourceRarityTag == "R")
            {
                rareInorganic += additionAmount;
            }
        }
    }


}
