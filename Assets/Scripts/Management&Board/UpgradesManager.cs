using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradesManager : MonoBehaviour
{
    string actionResearch = "Research";
    string actionBuild = "Build";
    string typeOrganic = "OR";
    string typeInorganic = "IN";

    //managers
    private ResourceManager resourceManager;
    private GameManager gameManager;
    private MenuManager menuManager;
    private StoryManager storyManager;

    //enough resources bool
    public bool enoughResrouces1;
    public bool enoughResrouces2;

    //bool for prompt mode to keep the buttons from reappearing
    private bool promptMode;

    //other buttons
    [SerializeField] GameObject backButton;
    [SerializeField] GameObject powersButton;
    [SerializeField] GameObject resourcesButton;

    //upgrade prompt/alert panel stuff
    [SerializeField] GameObject upgradePromptPanel;
    [SerializeField] GameObject upgradeQuestionText;
    [SerializeField] GameObject yesUpgradeButton;
    [SerializeField] GameObject noUpgradeButton;
    [SerializeField] GameObject notEnoughResroucesAlertText;

    [SerializeField] GameObject emitterTextGO;
    [SerializeField] TextMeshProUGUI emitterButtonText;
    [SerializeField] GameObject emitterButtonGO;
    [SerializeField] TextMeshProUGUI emitterResourceTypeText;
    [SerializeField] GameObject emitterResourceTypeGO;
    [SerializeField] TextMeshProUGUI emitterCostText;
    [SerializeField] GameObject emitterCostGO;
    [SerializeField] GameObject batteryTextGO;
    [SerializeField] TextMeshProUGUI batteryButtonText;
    [SerializeField] GameObject batteryButtonGO;
    [SerializeField] TextMeshProUGUI batteryResourceTypeText;
    [SerializeField] GameObject batteryResourceTypeGO;
    [SerializeField] TextMeshProUGUI batteryCostText;
    [SerializeField] GameObject batteryCostGO;
    [SerializeField] GameObject rainTextGO;
    [SerializeField] TextMeshProUGUI rainButtonText;
    [SerializeField] GameObject rainButtonGO;
    [SerializeField] TextMeshProUGUI rainResourceTypeText;
    [SerializeField] GameObject rainResourceTypeGO;
    [SerializeField] TextMeshProUGUI rainCostText;
    [SerializeField] GameObject rainCostGO;
    [SerializeField] GameObject delugeTextGO;
    [SerializeField] TextMeshProUGUI delugeButtonText;
    [SerializeField] GameObject delugeButtonGO;
    [SerializeField] TextMeshProUGUI delugeResourceTypeText;
    [SerializeField] GameObject delugeResourceTypeGO;
    [SerializeField] TextMeshProUGUI delugeCostText;
    [SerializeField] GameObject delugeCostGO;
    [SerializeField] GameObject stormTextGO;
    [SerializeField] TextMeshProUGUI stormButtonText;
    [SerializeField] GameObject stormButtonGO;
    [SerializeField] TextMeshProUGUI stormResourceTypeText;
    [SerializeField] GameObject stormResourceTypeGO;
    [SerializeField] TextMeshProUGUI stormCostText;
    [SerializeField] GameObject stormCostGO;
    [SerializeField] GameObject freezeTextGO;
    [SerializeField] TextMeshProUGUI freezeButtonText;
    [SerializeField] GameObject freezeButtonGO;
    [SerializeField] TextMeshProUGUI freezeResourceTypeText;
    [SerializeField] GameObject freezeResourceTypeGO;
    [SerializeField] TextMeshProUGUI freezeCostText;
    [SerializeField] GameObject freezeCostGO;


    //bools to indicate when upgrade at max level, so can be turned off
    private bool emitterMaxLevel;
    private bool batteryMaxLevel;
    private bool rainMaxLevel;
    private bool delugeMaxLevel;
    private bool stormMaxLevel;
    private bool freezeMaxLevel;


    //activeUpgrade string
    private string activeUpgrade;

    //Emitter UpgradeCosts
    private string emitterCurrentRarity1;
    private int emitterCurrentCost1;
    private string emitterCurrentRarity2;
    private int emitterCurrentCost2;

    private string r1EmitterRarity1 = "C";
    private int r1EmitterCost1 = 5;
    private string r1EmitterRarity2 = "U";
    private int r1EmitterCost2 = 1;

    private string b1EmitterRarity1 = "C";
    private int b1EmitterCost1 = 5;
    private string b1EmitterRarity2 = "U";
    private int b1EmitterCost2 = 1;

    private string r2EmitterRarity1 = "U";
    private int r2EmitterCost1 = 10;
    private string r2EmitterRarity2 = "R";
    private int r2EmitterCost2 = 1;

    private string b2EmitterRarity1 = "C";
    private int b2EmitterCost1 = 25;
    private string b2EmitterRarity2 = "R";
    private int b2EmitterCost2 = 2;

    private string r3EmitterRarity1 = "U";
    private int rEmitterCost1 = 15;
    private string r3EmitterRarity2 = "R";
    private int r3EmitterCost2 = 5;

    private string b3EmitterRarity1 = "U";
    private int b3EmitterCost1 = 15;
    private string b3EmitterRarity2 = "R";
    private int b3EmitterCost2 = 5;

    //battery Upgrade Costs
    private string batteryCurrentRarity1;
    private int batteryCurrentCost1;
    private string batteryCurrentRarity2;
    private int batteryCurrentCost2;

    private string r1BatteryRarity1 = "C";
    private int r1BatteryCost1 = 5;
    private string r1BatteryRarity2 = "U";
    private int r1BatteryCost2 = 1;

    private string b1BatteryRarity1 = "C";
    private int b1BatteryCost1 = 5;
    private string b1BatteryRarity2 = "U";
    private int b1BatteryCost2 = 1;

    private string r2BatteryRarity1 = "C";
    private int r2BatteryCost1 = 5;
    private string r2BatteryRarity2 = "U";
    private int r2BatteryCost2 = 2;

    private string b2BatteryRarity1 = "U";
    private int b2BatteryCost1 = 5;
    private string b2BatteryRarity2 = "C";
    private int b2BatteryCost2 = 2;

    private string r3BatteryRarity1 = "U";
    private int r3BatteryCost1 = 10;
    private string r3BatteryRarity2 = "R";
    private int r3BatteryCost2 = 1;

    private string b3BatteryRarity1 = "C";
    private int b3BatteryCost1 = 25;
    private string b3BatteryRarity2 = "U";
    private int b3BatteryCost2 = 2;

    private string r4BatteryRarity1 = "U";
    private int r4BatteryCost1 = 10;
    private string r4BatteryRarity2 = "R";
    private int r4BatteryCost2 = 1;

    private string b4BatteryRarity1 = "C";
    private int b4BatteryCost1 = 25;
    private string b4BatteryRarity2 = "U";
    private int b4BatteryCost2 = 2;

    private string r5BatteryRarity1 = "U";
    private int r5BatteryCost1 = 10;
    private string r5BatteryRarity2 = "R";
    private int r5BatteryCost2 = 1;

    private string b5BatteryRarity1 = "C";
    private int b5BatteryCost1 = 25;
    private string b5BatteryRarity2 = "R";
    private int b5BatteryCost2 = 2;


    //rain upgrade costs
    private string rainCurrentRarity1;
    private int rainCurrentCost1;
    private string rainCurrentRarity2;
    private int rainCurrentCost2;

    private string r1RainRarity1 = "C";
    private int r1RainCost1 = 5;
    private string r1RainRarity2 = "U";
    private int r1RainCost2 = 1;

    private string b1RainRarity1 = "C";
    private int b1RainCost1 = 10;
    private string b1RainRarity2 = "R";
    private int b1RainCost2 = 1;

    private string r2RainRarity1 = "U";
    private int r2RainCost1 = 10;
    private string r2RainRarity2 = "R";
    private int r2RainCost2 = 1;

    private string b2RainRarity1 = "C";
    private int b2RainCost1 = 25;
    private string b2RainRarity2 = "R";
    private int b2RainCost2 = 2;

    //deluge upgrade costs
    private string delugeCurrentRarity1;
    private int delugeCurrentCost1;
    private string delugeCurrentRarity2;
    private int delugeCurrentCost2;

    private string r1DelugeRarity1 = "C";
    private int r1DelugeCost1 = 5;
    private string r1DelugeRarity2 = "U";
    private int r1DelugeCost2 = 1;

    private string b1DelugeRarity1 = "C";
    private int b1DelugeCost1 = 10;
    private string b1DelugeRarity2 = "R";
    private int b1DelugeCost2 = 1;

    private string r2DelugeRarity1 = "U";
    private int r2DelugeCost1 = 10;
    private string r2DelugeRarity2 = "R";
    private int r2DelugeCost2 = 1;

    private string b2DelugeRarity1 = "C";
    private int b2DelugeCost1 = 25;
    private string b2DelugeRarity2 = "R";
    private int b2DelugeCost2 = 2;

    //storm upgrade costs
    private string stormCurrentRarity1;
    private int stormCurrentCost1;
    private string stormCurrentRarity2;
    private int stormCurrentCost2;

    private string r1StormRarity1 = "C";
    private int r1StormCost1 = 5;
    private string r1StormRarity2 = "U";
    private int r1StormCost2 = 1;

    private string b1StormRarity1 = "C";
    private int b1StormCost1 = 10;
    private string b1StormRarity2 = "R";
    private int b1StormCost2 = 1;

    private string r2StormRarity1 = "U";
    private int r2StormCost1 = 10;
    private string r2StormRarity2 = "R";
    private int r2StormCost2 = 1;

    private string b2StormRarity1 = "C";
    private int b2StormCost1 = 25;
    private string b2StormRarity2 = "R";
    private int b2StormCost2 = 2;

    //freeze upgrade costs

    private string freezeCurrentRarity1;
    private int freezeCurrentCost1;
    private string freezeCurrentRarity2;
    private int freezeCurrentCost2;

    private string r1FreezeRarity1 = "C";
    private int r1FreezeCost1 = 5;
    private string r1FreezeRarity2 = "U";
    private int r1FreezeCost2 = 1;

    private string b1FreezeRarity1 = "C";
    private int b1FreezeCost1 = 10;
    private string b1FreezeRarity2 = "R";
    private int b1FreezeCost2 = 1;

    private string r2FreezeRarity1 = "U";
    private int r2FreezeCost1 = 10;
    private string r2FreezeRarity2 = "R";
    private int r2FreezeCost2 = 1;

    private string b2FreezeRarity1 = "C";
    private int b2FreezeCost1 = 25;
    private string b2FreezeRarity2 = "R";
    private int b2FreezeCost2 = 2;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        
        storyManager = FindObjectOfType<StoryManager>();


        //turned off for testing
        //ResetAllUpgrades();

        resourceManager = FindObjectOfType<ResourceManager>();

        upgradePromptPanel.SetActive(false);
        upgradeQuestionText.SetActive(false);
        yesUpgradeButton.SetActive(false);
        noUpgradeButton.SetActive(false);
        notEnoughResroucesAlertText.SetActive(false);

        if (storyManager.testingModeActive)
        {
            UpdateTestingUpgradeCosts(storyManager.testingUpgradeTypesSM, storyManager.testingUpgradeCostsSM);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!promptMode)
        {
            CheckWhichUpgradesToDisplay();
        }  
    }

    private void CheckWhichUpgradesToDisplay()
    {

        //*****************************

        if(storyManager.availableUpgradesArraySM[0] >= 0)
        {
            if (!emitterMaxLevel)
            {
                TurnUpgradeOn("emitter");
            }

            if(storyManager.availableUpgradesArraySM[1] == 0)
            {
                emitterButtonText.text = actionResearch;
                emitterResourceTypeText.text = typeOrganic;
            }
            else
            {
                emitterButtonText.text = actionBuild;
                emitterResourceTypeText.text = typeInorganic;
            }

            //update the resource requirements
            if (storyManager.availableUpgradesArraySM[0] == 0 && storyManager.availableUpgradesArraySM[1] == 0)
            {
                emitterCurrentRarity1 = r1EmitterRarity1;
                emitterCurrentCost1 = r1EmitterCost1;
                emitterCurrentRarity2 = r1EmitterRarity2;
                emitterCurrentCost2 = r1EmitterCost2;               
            }
            if(storyManager.availableUpgradesArraySM[0] == 0 && storyManager.availableUpgradesArraySM[1] == 1)
            {
                emitterCurrentRarity1 = b1EmitterRarity1;
                emitterCurrentCost1 = b1EmitterCost1;
                emitterCurrentRarity2 = b1EmitterRarity2;
                emitterCurrentCost2 = b1EmitterCost2;
            }
            if (storyManager.availableUpgradesArraySM[0] == 1 && storyManager.availableUpgradesArraySM[1] == 0)
            {
                emitterCurrentRarity1 = r2EmitterRarity1;
                emitterCurrentCost1 = r2EmitterCost1;
                emitterCurrentRarity2 = r2EmitterRarity2;
                emitterCurrentCost2 = r2EmitterCost2;
            }
            if (storyManager.availableUpgradesArraySM[0] == 1 && storyManager.availableUpgradesArraySM[1] == 1)
            {
                emitterCurrentRarity1 = b2EmitterRarity1;
                emitterCurrentCost1 = b2EmitterCost1;
                emitterCurrentRarity2 = b2EmitterRarity2;
                emitterCurrentCost2 = b2EmitterCost2;
            }

            emitterCostText.text = CostStringBuilder
                (emitterCurrentRarity1, emitterCurrentCost1, emitterCurrentRarity2, emitterCurrentCost2);
        }
        else
        {
            TurnUpgradeOff("emitter");
        }

        //*****************************

        if (storyManager.availableUpgradesArraySM[2] >= 0)
        {
            if (!batteryMaxLevel)
            {
                TurnUpgradeOn("battery");
            }

            if (storyManager.availableUpgradesArraySM[3] == 0)
            {
                batteryButtonText.text = actionResearch;
                batteryResourceTypeText.text = typeOrganic;
            }
            else
            {
                batteryButtonText.text = actionBuild;
                batteryResourceTypeText.text = typeInorganic;
            }

            if (storyManager.availableUpgradesArraySM[2] == 0 && storyManager.availableUpgradesArraySM[3] == 0)
            {
                batteryCurrentRarity1 = r1BatteryRarity1;
                batteryCurrentCost1 = r1BatteryCost1;
                batteryCurrentRarity2 = r1BatteryRarity2;
                batteryCurrentCost2 = r1BatteryCost2;
            }
            if (storyManager.availableUpgradesArraySM[2] == 0 && storyManager.availableUpgradesArraySM[3] == 1)
            {
                batteryCurrentRarity1 = b1BatteryRarity1;
                batteryCurrentCost1 = b1BatteryCost1;
                batteryCurrentRarity2 = b1BatteryRarity2;
                batteryCurrentCost2 = b1BatteryCost2;
            }
            if (storyManager.availableUpgradesArraySM[2] == 1 && storyManager.availableUpgradesArraySM[3] == 0)
            {
                batteryCurrentRarity1 = r2BatteryRarity1;
                batteryCurrentCost1 = r2BatteryCost1;
                batteryCurrentRarity2 = r2BatteryRarity2;
                batteryCurrentCost2 = r2BatteryCost2;
            }
            if (storyManager.availableUpgradesArraySM[2] == 1 && storyManager.availableUpgradesArraySM[3] == 1)
            {
                batteryCurrentRarity1 = b2BatteryRarity1;
                batteryCurrentCost1 = b2BatteryCost1;
                batteryCurrentRarity2 = b2BatteryRarity2;
                batteryCurrentCost2 = b2BatteryCost2;
            }

            batteryCostText.text = CostStringBuilder
                (batteryCurrentRarity1, batteryCurrentCost1, batteryCurrentRarity2, batteryCurrentCost2);
        }
        else
        {
            TurnUpgradeOff("battery");
        }

        //*****************************

        if (storyManager.availableUpgradesArraySM[4] >= 0)
        {
            if (!rainMaxLevel)
            {
                TurnUpgradeOn("rain");
            }

            if (storyManager.availableUpgradesArraySM[5] == 0)
            {
                rainButtonText.text = actionResearch;
                rainResourceTypeText.text = typeOrganic;
            }
            else
            {
                rainButtonText.text = actionBuild;
                rainResourceTypeText.text = typeInorganic;
            }

            if (storyManager.availableUpgradesArraySM[4] == 0 && storyManager.availableUpgradesArraySM[5] == 0)
            {
                rainCurrentRarity1 = r1RainRarity1;
                rainCurrentCost1 = r1RainCost1;
                rainCurrentRarity2 = r1RainRarity2;
                rainCurrentCost2 = r1RainCost2;
            }
            if (storyManager.availableUpgradesArraySM[4] == 0 && storyManager.availableUpgradesArraySM[5] == 1)
            {
                rainCurrentRarity1 = b1RainRarity1;
                rainCurrentCost1 = b1RainCost1;
                rainCurrentRarity2 = b1RainRarity2;
                rainCurrentCost2 = b1RainCost2;
            }
            if (storyManager.availableUpgradesArraySM[4] == 1 && storyManager.availableUpgradesArraySM[5] == 0)
            {
                rainCurrentRarity1 = r2RainRarity1;
                rainCurrentCost1 = r2RainCost1;
                rainCurrentRarity2 = r2RainRarity2;
                rainCurrentCost2 = r2RainCost2;
            }
            if (storyManager.availableUpgradesArraySM[4] == 1 && storyManager.availableUpgradesArraySM[5] == 1)
            {
                rainCurrentRarity1 = b2RainRarity1;
                rainCurrentCost1 = b2RainCost1;
                rainCurrentRarity2 = b2RainRarity2;
                rainCurrentCost2 = b2RainCost2;
            }

            rainCostText.text = CostStringBuilder
                (rainCurrentRarity1, rainCurrentCost1, rainCurrentRarity2, rainCurrentCost2);
        }
        else
        {
            TurnUpgradeOff("rain");
        }

        //*****************************

        if (storyManager.availableUpgradesArraySM[6] >= 0)
        {
            if (!delugeMaxLevel)
            {
                TurnUpgradeOn("deluge");
            }

            if (storyManager.availableUpgradesArraySM[7] == 0)
            {
                delugeButtonText.text = actionResearch;
                delugeResourceTypeText.text = typeOrganic;
            }
            else
            {
                delugeButtonText.text = actionBuild;
                delugeResourceTypeText.text = typeInorganic;
            }

            if (storyManager.availableUpgradesArraySM[6] == 0 && storyManager.availableUpgradesArraySM[7] == 0)
            {
                delugeCurrentRarity1 = r1DelugeRarity1;
                delugeCurrentCost1 = r1DelugeCost1;
                delugeCurrentRarity2 = r1DelugeRarity2;
                delugeCurrentCost2 = r1DelugeCost2;
            }
            if (storyManager.availableUpgradesArraySM[6] == 0 && storyManager.availableUpgradesArraySM[7] == 1)
            {
                delugeCurrentRarity1 = b1DelugeRarity1;
                delugeCurrentCost1 = b1DelugeCost1;
                delugeCurrentRarity2 = b1DelugeRarity2;
                delugeCurrentCost2 = b1DelugeCost2;
            }
            if (storyManager.availableUpgradesArraySM[6] == 1 && storyManager.availableUpgradesArraySM[7] == 0)
            {
                delugeCurrentRarity1 = r2DelugeRarity1;
                delugeCurrentCost1 = r2DelugeCost1;
                delugeCurrentRarity2 = r2DelugeRarity2;
                delugeCurrentCost2 = r2DelugeCost2;
            }
            if (storyManager.availableUpgradesArraySM[6] == 1 && storyManager.availableUpgradesArraySM[7] == 1)
            {
                delugeCurrentRarity1 = b2DelugeRarity1;
                delugeCurrentCost1 = b2DelugeCost1;
                delugeCurrentRarity2 = b2DelugeRarity2;
                delugeCurrentCost2 = b2DelugeCost2;
            }

            delugeCostText.text = CostStringBuilder
                (delugeCurrentRarity1, delugeCurrentCost1, delugeCurrentRarity2, delugeCurrentCost2);
        }
        else
        {
            TurnUpgradeOff("deluge");
        }

        //*****************************

        if (storyManager.availableUpgradesArraySM[8] >= 0)
        {
            if (!stormMaxLevel)
            {
                TurnUpgradeOn("storm");
            }

            if (storyManager.availableUpgradesArraySM[9] == 0)
            {
                stormButtonText.text = actionResearch;
                stormResourceTypeText.text = typeOrganic;
            }
            else
            {
                stormButtonText.text = actionBuild;
                stormResourceTypeText.text = typeInorganic;
            }

            if (storyManager.availableUpgradesArraySM[8] == 0 && storyManager.availableUpgradesArraySM[9] == 0)
            {
                stormCurrentRarity1 = r1StormRarity1;
                stormCurrentCost1 = r1StormCost1;
                stormCurrentRarity2 = r1StormRarity2;
                stormCurrentCost2 = r1StormCost2;
            }
            if (storyManager.availableUpgradesArraySM[8] == 0 && storyManager.availableUpgradesArraySM[9] == 1)
            {
                stormCurrentRarity1 = b1StormRarity1;
                stormCurrentCost1 = b1StormCost1;
                stormCurrentRarity2 = b1StormRarity2;
                stormCurrentCost2 = b1StormCost2;
            }
            if (storyManager.availableUpgradesArraySM[8] == 1 && storyManager.availableUpgradesArraySM[9] == 0)
            {
                stormCurrentRarity1 = r2StormRarity1;
                stormCurrentCost1 = r2StormCost1;
                stormCurrentRarity2 = r2StormRarity2;
                stormCurrentCost2 = r2StormCost2;
            }
            if (storyManager.availableUpgradesArraySM[8] == 1 && storyManager.availableUpgradesArraySM[9] == 1)
            {
                stormCurrentRarity1 = b2StormRarity1;
                stormCurrentCost1 = b2StormCost1;
                stormCurrentRarity2 = b2StormRarity2;
                stormCurrentCost2 = b2StormCost2;
            }

            stormCostText.text = CostStringBuilder
                (stormCurrentRarity1, stormCurrentCost1, stormCurrentRarity2, stormCurrentCost2);
        }
        else
        {
            TurnUpgradeOff("storm");
        }

        //*****************************

        if (storyManager.availableUpgradesArraySM[10] >= 0)
        {
            if (!freezeMaxLevel)
            {
                TurnUpgradeOn("freeze");
            }

            if (storyManager.availableUpgradesArraySM[11] == 0)
            {
                freezeButtonText.text = actionResearch;
                freezeResourceTypeText.text = typeOrganic;
            }
            else
            {
                freezeButtonText.text = actionBuild;
                freezeResourceTypeText.text = typeInorganic;
            }

            if (storyManager.availableUpgradesArraySM[10] == 0 && storyManager.availableUpgradesArraySM[11] == 0)
            {
                freezeCurrentRarity1 = r1FreezeRarity1;
                freezeCurrentCost1 = r1FreezeCost1;
                freezeCurrentRarity2 = r1FreezeRarity2;
                freezeCurrentCost2 = r1FreezeCost2;
            }
            if (storyManager.availableUpgradesArraySM[10] == 0 && storyManager.availableUpgradesArraySM[11] == 1)
            {
                freezeCurrentRarity1 = b1FreezeRarity1;
                freezeCurrentCost1 = b1FreezeCost1;
                freezeCurrentRarity2 = b1FreezeRarity2;
                freezeCurrentCost2 = b1FreezeCost2;
            }
            if (storyManager.availableUpgradesArraySM[10] == 1 && storyManager.availableUpgradesArraySM[11] == 0)
            {
                freezeCurrentRarity1 = r2FreezeRarity1;
                freezeCurrentCost1 = r2FreezeCost1;
                freezeCurrentRarity2 = r2FreezeRarity2;
                freezeCurrentCost2 = r2FreezeCost2;
            }
            if (storyManager.availableUpgradesArraySM[10] == 1 && storyManager.availableUpgradesArraySM[11] == 1)
            {
                freezeCurrentRarity1 = b2FreezeRarity1;
                freezeCurrentCost1 = b2FreezeCost1;
                freezeCurrentRarity2 = b2FreezeRarity2;
                freezeCurrentCost2 = b2FreezeCost2;
            }

            freezeCostText.text = CostStringBuilder
                (freezeCurrentRarity1, freezeCurrentCost1, freezeCurrentRarity2, freezeCurrentCost2);
        }
        else
        {
            TurnUpgradeOff("freeze");
        }
    }

    private string CostStringBuilder(string rarityLetter1, int amount1, string rarityLetter2, int amount2)
    {
        return rarityLetter1 + "-" + amount1.ToString() + " " + rarityLetter2 + "-" + amount2.ToString();
    }

    private void TurnUpgradeOff(string upgrade)
    {
        if(upgrade == "emitter")
        {
            emitterTextGO.SetActive(false);
            emitterButtonGO.SetActive(false);
            emitterResourceTypeGO.SetActive(false);
            emitterCostGO.SetActive(false);
        }
        if (upgrade == "battery")
        {
            batteryTextGO.SetActive(false);
            batteryButtonGO.SetActive(false);
            batteryResourceTypeGO.SetActive(false);
            batteryCostGO.SetActive(false);
        }
        if (upgrade == "rain")
        {
            rainTextGO.SetActive(false);
            rainButtonGO.SetActive(false);
            rainResourceTypeGO.SetActive(false);
            rainCostGO.SetActive(false);
        }
        if (upgrade == "deluge")
        {
            delugeTextGO.SetActive(false);
            delugeButtonGO.SetActive(false);
            delugeResourceTypeGO.SetActive(false);
            delugeCostGO.SetActive(false);
        }
        if (upgrade == "storm")
        {
            stormTextGO.SetActive(false);
            stormButtonGO.SetActive(false);
            stormResourceTypeGO.SetActive(false);
            stormCostGO.SetActive(false);
        }
        if (upgrade == "freeze")
        {
            freezeTextGO.SetActive(false);
            freezeButtonGO.SetActive(false);
            freezeResourceTypeGO.SetActive(false);
            freezeCostGO.SetActive(false);
        }
    }

    private void TurnUpgradeOn(string upgrade)
    {
        if (upgrade == "emitter")
        {
            emitterTextGO.SetActive(true);
            emitterButtonGO.SetActive(true);
            emitterResourceTypeGO.SetActive(true);
            emitterCostGO.SetActive(true);
        }
        if (upgrade == "battery")
        {
            batteryTextGO.SetActive(true);
            batteryButtonGO.SetActive(true);
            batteryResourceTypeGO.SetActive(true);
            batteryCostGO.SetActive(true);
        }
        if (upgrade == "rain")
        {
            rainTextGO.SetActive(true);
            rainButtonGO.SetActive(true);
            rainResourceTypeGO.SetActive(true);
            rainCostGO.SetActive(true);
        }
        if (upgrade == "deluge")
        {
            delugeTextGO.SetActive(true);
            delugeButtonGO.SetActive(true);
            delugeResourceTypeGO.SetActive(true);
            delugeCostGO.SetActive(true);
        }
        if (upgrade == "storm")
        {
            stormTextGO.SetActive(true);
            stormButtonGO.SetActive(true);
            stormResourceTypeGO.SetActive(true);
            stormCostGO.SetActive(true);
        }
        if (upgrade == "freeze")
        {
            freezeTextGO.SetActive(true);
            freezeButtonGO.SetActive(true);
            freezeResourceTypeGO.SetActive(true);
            freezeCostGO.SetActive(true);
        }
    }



    public void EmitterResearchOrUpgrade()
    {
        enoughResrouces1 = false;
        enoughResrouces2 = false;

        //research
        if(storyManager.availableUpgradesArraySM[1] == 0)
        {
            if(emitterCurrentRarity1 == "C")
            {
                if(emitterCurrentCost1 <= resourceManager.commonOrganic)
                {
                    enoughResrouces1 = true;
                }
            }
            if (emitterCurrentRarity1 == "U")
            {
                if (emitterCurrentCost1 <= resourceManager.uncommonOrganic)
                {
                    enoughResrouces1 = true;
                }
            }
            if(emitterCurrentRarity2 == "U")
            {
                if (emitterCurrentCost2 <= resourceManager.uncommonOrganic)
                {
                    enoughResrouces2 = true;
                }
            }
            if (emitterCurrentRarity2 == "R")
            {
                if (emitterCurrentCost2 <= resourceManager.rareOrganic)
                {
                    enoughResrouces2 = true;
                }
            }

            if(enoughResrouces1 && enoughResrouces2)
            {
                activeUpgrade = "emitter";
                OpenUpgradePromtPanel();
            }
            else if(!enoughResrouces1 || !enoughResrouces2)
            {  
                StartCoroutine(NotEnoughResroucesAlert());                
            }
        }
        //build
        else
        {
            if (emitterCurrentRarity1 == "C")
            {
                if (emitterCurrentCost1 <= resourceManager.commonInorganic)
                {
                    enoughResrouces1 = true;
                }
            }
            if (emitterCurrentRarity1 == "U")
            {
                if (emitterCurrentCost1 <= resourceManager.uncommonInorganic)
                {
                    enoughResrouces1 = true;
                }
            }
            if (emitterCurrentRarity2 == "U")
            {
                if (emitterCurrentCost2 <= resourceManager.uncommonInorganic)
                {
                    enoughResrouces2 = true;
                }
            }
            if (emitterCurrentRarity2 == "R")
            {
                if (emitterCurrentCost2 <= resourceManager.rareInorganic)
                {
                    enoughResrouces2 = true;
                }
            }

            if (enoughResrouces1 && enoughResrouces2)
            {
                activeUpgrade = "emitter";
                OpenUpgradePromtPanel();
            }
            else if (!enoughResrouces1 || !enoughResrouces2)
            {
                StartCoroutine(NotEnoughResroucesAlert());
            }
        }
    }

    public void BatteryResearchOrUpgrade()
    {
        enoughResrouces1 = false;
        enoughResrouces2 = false;

        //research
        if (storyManager.availableUpgradesArraySM[3] == 0)
        {
            if (batteryCurrentRarity1 == "C")
            {
                if (batteryCurrentCost1 <= resourceManager.commonOrganic)
                {
                    enoughResrouces1 = true;
                }
            }
            if (batteryCurrentRarity1 == "U")
            {
                if (batteryCurrentCost1 <= resourceManager.uncommonOrganic)
                {
                    enoughResrouces1 = true;
                }
            }
            if (batteryCurrentRarity2 == "U")
            {
                if (batteryCurrentCost2 <= resourceManager.uncommonOrganic)
                {
                    enoughResrouces2 = true;
                }
            }
            if (batteryCurrentRarity2 == "R")
            {
                if (batteryCurrentCost2 <= resourceManager.rareOrganic)
                {
                    enoughResrouces2 = true;
                }
            }

            if (enoughResrouces1 && enoughResrouces2)
            {
                activeUpgrade = "battery";
                OpenUpgradePromtPanel();
            }
            else if (!enoughResrouces1 || !enoughResrouces2)
            {
                StartCoroutine(NotEnoughResroucesAlert());
            }
        }
        //build
        else
        {
            if (batteryCurrentRarity1 == "C")
            {
                if (batteryCurrentCost1 <= resourceManager.commonInorganic)
                {
                    enoughResrouces1 = true;
                }
            }
            if (batteryCurrentRarity1 == "U")
            {
                if (batteryCurrentCost1 <= resourceManager.uncommonInorganic)
                {
                    enoughResrouces1 = true;
                }
            }
            if (batteryCurrentRarity2 == "U")
            {
                if (batteryCurrentCost2 <= resourceManager.uncommonInorganic)
                {
                    enoughResrouces2 = true;
                }
            }
            if (batteryCurrentRarity2 == "R")
            {
                if (batteryCurrentCost2 <= resourceManager.rareInorganic)
                {
                    enoughResrouces2 = true;
                }
            }

            if (enoughResrouces1 && enoughResrouces2)
            {
                activeUpgrade = "battery";
                OpenUpgradePromtPanel();
            }
            else if (!enoughResrouces1 || !enoughResrouces2)
            {
                StartCoroutine(NotEnoughResroucesAlert());
            }
        }
    }

    public void RainResearchOrUpgrade()
    {
        enoughResrouces1 = false;
        enoughResrouces2 = false;

        //research
        if (storyManager.availableUpgradesArraySM[5] == 0)
        {
            if (rainCurrentRarity1 == "C")
            {
                if (rainCurrentCost1 <= resourceManager.commonOrganic)
                {
                    enoughResrouces1 = true;
                }
            }
            if (rainCurrentRarity1 == "U")
            {
                if (rainCurrentCost1 <= resourceManager.uncommonOrganic)
                {
                    enoughResrouces1 = true;
                }
            }
            if (rainCurrentRarity2 == "U")
            {
                if (rainCurrentCost2 <= resourceManager.uncommonOrganic)
                {
                    enoughResrouces2 = true;
                }
            }
            if (rainCurrentRarity2 == "R")
            {
                if (rainCurrentCost2 <= resourceManager.rareOrganic)
                {
                    enoughResrouces2 = true;
                }
            }

            if (enoughResrouces1 && enoughResrouces2)
            {
                activeUpgrade = "rain";
                OpenUpgradePromtPanel();
            }
            else if (!enoughResrouces1 || !enoughResrouces2)
            {
                StartCoroutine(NotEnoughResroucesAlert());
            }
        }
        //build
        else
        {
            if (rainCurrentRarity1 == "C")
            {
                if (rainCurrentCost1 <= resourceManager.commonInorganic)
                {
                    enoughResrouces1 = true;
                }
            }
            if (rainCurrentRarity1 == "U")
            {
                if (rainCurrentCost1 <= resourceManager.uncommonInorganic)
                {
                    enoughResrouces1 = true;
                }
            }
            if (rainCurrentRarity2 == "U")
            {
                if (rainCurrentCost2 <= resourceManager.uncommonInorganic)
                {
                    enoughResrouces2 = true;
                }
            }
            if (rainCurrentRarity2 == "R")
            {
                if (rainCurrentCost2 <= resourceManager.rareInorganic)
                {
                    enoughResrouces2 = true;
                }
            }

            if (enoughResrouces1 && enoughResrouces2)
            {
                activeUpgrade = "rain";
                OpenUpgradePromtPanel();
            }
            else if (!enoughResrouces1 || !enoughResrouces2)
            {
                StartCoroutine(NotEnoughResroucesAlert());
            }
        }
    }

    public void DelugeResearchOrUpgrade()
    {
        enoughResrouces1 = false;
        enoughResrouces2 = false;

        //research
        if (storyManager.availableUpgradesArraySM[7] == 0)
        {
            if (delugeCurrentRarity1 == "C")
            {
                if (delugeCurrentCost1 <= resourceManager.commonOrganic)
                {
                    enoughResrouces1 = true;
                }
            }
            if (delugeCurrentRarity1 == "U")
            {
                if (delugeCurrentCost1 <= resourceManager.uncommonOrganic)
                {
                    enoughResrouces1 = true;
                }
            }
            if (delugeCurrentRarity2 == "U")
            {
                if (delugeCurrentCost2 <= resourceManager.uncommonOrganic)
                {
                    enoughResrouces2 = true;
                }
            }
            if (delugeCurrentRarity2 == "R")
            {
                if (delugeCurrentCost2 <= resourceManager.rareOrganic)
                {
                    enoughResrouces2 = true;
                }
            }

            if (enoughResrouces1 && enoughResrouces2)
            {
                activeUpgrade = "deluge";
                OpenUpgradePromtPanel();
            }
            else if (!enoughResrouces1 || !enoughResrouces2)
            {
                StartCoroutine(NotEnoughResroucesAlert());
            }
        }
        //build
        else
        {
            if (delugeCurrentRarity1 == "C")
            {
                if (delugeCurrentCost1 <= resourceManager.commonInorganic)
                {
                    enoughResrouces1 = true;
                }
            }
            if (delugeCurrentRarity1 == "U")
            {
                if (delugeCurrentCost1 <= resourceManager.uncommonInorganic)
                {
                    enoughResrouces1 = true;
                }
            }
            if (delugeCurrentRarity2 == "U")
            {
                if (delugeCurrentCost2 <= resourceManager.uncommonInorganic)
                {
                    enoughResrouces2 = true;
                }
            }
            if (delugeCurrentRarity2 == "R")
            {
                if (delugeCurrentCost2 <= resourceManager.rareInorganic)
                {
                    enoughResrouces2 = true;
                }
            }

            if (enoughResrouces1 && enoughResrouces2)
            {
                activeUpgrade = "deluge";
                OpenUpgradePromtPanel();
            }
            else if (!enoughResrouces1 || !enoughResrouces2)
            {
                StartCoroutine(NotEnoughResroucesAlert());
            }
        }
    }

    public void StormResearchOrUpgrade()
    {
        enoughResrouces1 = false;
        enoughResrouces2 = false;

        //research
        if (storyManager.availableUpgradesArraySM[9] == 0)
        {
            if (stormCurrentRarity1 == "C")
            {
                if (stormCurrentCost1 <= resourceManager.commonOrganic)
                {
                    enoughResrouces1 = true;
                }
            }
            if (stormCurrentRarity1 == "U")
            {
                if (stormCurrentCost1 <= resourceManager.uncommonOrganic)
                {
                    enoughResrouces1 = true;
                }
            }
            if (stormCurrentRarity2 == "U")
            {
                if (stormCurrentCost2 <= resourceManager.uncommonOrganic)
                {
                    enoughResrouces2 = true;
                }
            }
            if (stormCurrentRarity2 == "R")
            {
                if (stormCurrentCost2 <= resourceManager.rareOrganic)
                {
                    enoughResrouces2 = true;
                }
            }

            if (enoughResrouces1 && enoughResrouces2)
            {
                activeUpgrade = "storm";
                OpenUpgradePromtPanel();
            }
            else if (!enoughResrouces1 || !enoughResrouces2)
            {
                StartCoroutine(NotEnoughResroucesAlert());
            }
        }
        //build
        else
        {
            if (stormCurrentRarity1 == "C")
            {
                if (stormCurrentCost1 <= resourceManager.commonInorganic)
                {
                    enoughResrouces1 = true;
                }
            }
            if (stormCurrentRarity1 == "U")
            {
                if (stormCurrentCost1 <= resourceManager.uncommonInorganic)
                {
                    enoughResrouces1 = true;
                }
            }
            if (stormCurrentRarity2 == "U")
            {
                if (stormCurrentCost2 <= resourceManager.uncommonInorganic)
                {
                    enoughResrouces2 = true;
                }
            }
            if (stormCurrentRarity2 == "R")
            {
                if (stormCurrentCost2 <= resourceManager.rareInorganic)
                {
                    enoughResrouces2 = true;
                }
            }

            if (enoughResrouces1 && enoughResrouces2)
            {
                activeUpgrade = "storm";
                OpenUpgradePromtPanel();
            }
            else if (!enoughResrouces1 || !enoughResrouces2)
            {
                StartCoroutine(NotEnoughResroucesAlert());
            }
        }
    }

    public void FreezeResearchOrUpgrade()
    {
        enoughResrouces1 = false;
        enoughResrouces2 = false;

        //research
        if (storyManager.availableUpgradesArraySM[11] == 0)
        {
            if (freezeCurrentRarity1 == "C")
            {
                if (freezeCurrentCost1 <= resourceManager.commonOrganic)
                {
                    enoughResrouces1 = true;
                }
            }
            if (freezeCurrentRarity1 == "U")
            {
                if (freezeCurrentCost1 <= resourceManager.uncommonOrganic)
                {
                    enoughResrouces1 = true;
                }
            }
            if (freezeCurrentRarity2 == "U")
            {
                if (freezeCurrentCost2 <= resourceManager.uncommonOrganic)
                {
                    enoughResrouces2 = true;
                }
            }
            if (freezeCurrentRarity2 == "R")
            {
                if (freezeCurrentCost2 <= resourceManager.rareOrganic)
                {
                    enoughResrouces2 = true;
                }
            }

            if (enoughResrouces1 && enoughResrouces2)
            {
                activeUpgrade = "freeze";
                OpenUpgradePromtPanel();
            }
            else if (!enoughResrouces1 || !enoughResrouces2)
            {
                StartCoroutine(NotEnoughResroucesAlert());
            }
        }
        //build
        else
        {
            if (freezeCurrentRarity1 == "C")
            {
                if (freezeCurrentCost1 <= resourceManager.commonInorganic)
                {
                    enoughResrouces1 = true;
                }
            }
            if (freezeCurrentRarity1 == "U")
            {
                if (freezeCurrentCost1 <= resourceManager.uncommonInorganic)
                {
                    enoughResrouces1 = true;
                }
            }
            if (freezeCurrentRarity2 == "U")
            {
                if (freezeCurrentCost2 <= resourceManager.uncommonInorganic)
                {
                    enoughResrouces2 = true;
                }
            }
            if (freezeCurrentRarity2 == "R")
            {
                if (freezeCurrentCost2 <= resourceManager.rareInorganic)
                {
                    enoughResrouces2 = true;
                }
            }

            if (enoughResrouces1 && enoughResrouces2)
            {
                activeUpgrade = "freeze";
                OpenUpgradePromtPanel();
            }
            else if (!enoughResrouces1 || !enoughResrouces2)
            {
                StartCoroutine(NotEnoughResroucesAlert());
            }
        }
    }


    IEnumerator NotEnoughResroucesAlert()
    {
        promptMode = true;
        ButtonsOnOff("off");
        gameManager.characterEmote.EmoteSurprise();

        notEnoughResroucesAlertText.SetActive(true);
        upgradePromptPanel.SetActive(true);

        yield return new WaitForSeconds(1f);

        gameManager.characterEmote.EmoteConfused();

        yield return new WaitForSeconds(1.5f);

        upgradePromptPanel.SetActive(false);
        notEnoughResroucesAlertText.SetActive(false);
        ButtonsOnOff("on");
        gameManager.characterEmote.EmoteIdle();
        promptMode = false;
    }

    private void ButtonsOnOff(string action)
    {
        if(action == "off")
        {
            emitterButtonGO.SetActive(false);
            batteryButtonGO.SetActive(false);
            rainButtonGO.SetActive(false);
            delugeButtonGO.SetActive(false);
            stormButtonGO.SetActive(false);
            freezeButtonGO.SetActive(false);
            backButton.SetActive(false);
            powersButton.SetActive(false);
            resourcesButton.SetActive(false);
        }
        if(action == "on")
        {
            if (!emitterMaxLevel)
            {
                emitterButtonGO.SetActive(true);
            }
            if (!batteryMaxLevel)
            {
                batteryButtonGO.SetActive(true);
            }
            if (!rainMaxLevel)
            {
                rainButtonGO.SetActive(true);
            }
            if (!delugeMaxLevel)
            {
                delugeButtonGO.SetActive(true);
            }
            if (!stormMaxLevel)
            {
                stormButtonGO.SetActive(true);
            }
            if (!freezeMaxLevel)
            {
                freezeButtonGO.SetActive(true);
            }
            backButton.SetActive(true);
            powersButton.SetActive(true);
            resourcesButton.SetActive(true);
        }
    }

    private void OpenUpgradePromtPanel()
    {
        promptMode = true;
        ButtonsOnOff("off");
        upgradeQuestionText.SetActive(true);
        yesUpgradeButton.SetActive(true);
        noUpgradeButton.SetActive(true);
        upgradePromptPanel.SetActive(true);
    }

    public void CloseUpgradePromptPanel()
    {
        upgradePromptPanel.SetActive(false);
        upgradeQuestionText.SetActive(false);
        yesUpgradeButton.SetActive(false);
        noUpgradeButton.SetActive(false);
        promptMode = false;
        ButtonsOnOff("on");
        activeUpgrade = null;

    }

    public void ConfirmUpgradeChoice()
    {
        if(activeUpgrade == "emitter")
        {
            if(storyManager.availableUpgradesArraySM[1] == 0)
            {
                resourceManager.ReduceResource(typeOrganic, emitterCurrentRarity1, emitterCurrentCost1);
                resourceManager.ReduceResource(typeOrganic, emitterCurrentRarity2, emitterCurrentCost2);

                storyManager.availableUpgradesArraySM[1] = 1;                
            }
            else
            {
                resourceManager.ReduceResource(typeInorganic, emitterCurrentRarity1, emitterCurrentCost1);
                resourceManager.ReduceResource(typeInorganic, emitterCurrentRarity2, emitterCurrentCost2);

                storyManager.availableUpgradesArraySM[0]++;
                storyManager.availableUpgradesArraySM[1] = 0;

                if(storyManager.availableUpgradesArraySM[0] == 1)
                {
                    emitterTextGO.GetComponent<TextMeshProUGUI>().text = "Emitter3";
                }
                if (storyManager.availableUpgradesArraySM[0] == 2)
                {
                    emitterTextGO.GetComponent<TextMeshProUGUI>().text = "Emitter4";
                }
                if (storyManager.availableUpgradesArraySM[0] >= 3)
                {
                    emitterMaxLevel = true;
                    TurnUpgradeOff("emitter");
                    emitterTextGO.GetComponent<TextMeshProUGUI>().text = "MaxEmitter";
                    emitterTextGO.SetActive(true);                    
                }
            }
        }

        if (activeUpgrade == "battery")
        {
            if (storyManager.availableUpgradesArraySM[3] == 0)
            {
                resourceManager.ReduceResource(typeOrganic, batteryCurrentRarity1, batteryCurrentCost1);
                resourceManager.ReduceResource(typeOrganic, batteryCurrentRarity2, batteryCurrentCost2);

                storyManager.availableUpgradesArraySM[3] = 1;
            }
            else
            {
                resourceManager.ReduceResource(typeInorganic, batteryCurrentRarity1, batteryCurrentCost1);
                resourceManager.ReduceResource(typeInorganic, batteryCurrentRarity2, batteryCurrentCost2);

                storyManager.availableUpgradesArraySM[2]++;
                storyManager.availableUpgradesArraySM[3] = 0;

                if(storyManager.availableUpgradesArraySM[2] == 1)
                {
                    batteryTextGO.GetComponent<TextMeshProUGUI>().text = "Battery3";
                }
                if (storyManager.availableUpgradesArraySM[2] == 2)
                {
                    batteryTextGO.GetComponent<TextMeshProUGUI>().text = "Battery4";
                }
                if (storyManager.availableUpgradesArraySM[2] == 3)
                {
                    batteryTextGO.GetComponent<TextMeshProUGUI>().text = "Battery5";
                }
                if (storyManager.availableUpgradesArraySM[2] == 4)
                {
                    batteryTextGO.GetComponent<TextMeshProUGUI>().text = "Battery6";
                }
                if(storyManager.availableUpgradesArraySM[2] >= 5)
                {
                    batteryMaxLevel = true;
                    TurnUpgradeOff("battery");
                    batteryTextGO.GetComponent<TextMeshProUGUI>().text = "MaxBattery";
                    batteryTextGO.SetActive(true);
                }
            }
        }

        if (activeUpgrade == "rain")
        {
            if (storyManager.availableUpgradesArraySM[5] == 0)
            {
                resourceManager.ReduceResource(typeOrganic, rainCurrentRarity1, rainCurrentCost1);
                resourceManager.ReduceResource(typeOrganic, rainCurrentRarity2, rainCurrentCost2);

                storyManager.availableUpgradesArraySM[5] = 1;
            }
            else
            {
                resourceManager.ReduceResource(typeInorganic, rainCurrentRarity1, rainCurrentCost1);
                resourceManager.ReduceResource(typeInorganic, rainCurrentRarity2, rainCurrentCost2);

                storyManager.availableUpgradesArraySM[4]++;
                storyManager.availableUpgradesArraySM[5] = 0;

                if (storyManager.availableUpgradesArraySM[4] >= 1)
                {
                    rainMaxLevel = true;
                    TurnUpgradeOff("rain");
                    rainTextGO.GetComponent<TextMeshProUGUI>().text = "MaxRain";
                    rainTextGO.SetActive(true);
                }
            }
        }

        if (activeUpgrade == "deluge")
        {
            if (storyManager.availableUpgradesArraySM[7] == 0)
            {
                resourceManager.ReduceResource(typeOrganic, delugeCurrentRarity1, delugeCurrentCost1);
                resourceManager.ReduceResource(typeOrganic, delugeCurrentRarity2, delugeCurrentCost2);

                storyManager.availableUpgradesArraySM[7] = 1;
            }
            else
            {
                resourceManager.ReduceResource(typeInorganic, delugeCurrentRarity1, delugeCurrentCost1);
                resourceManager.ReduceResource(typeInorganic, delugeCurrentRarity2, delugeCurrentCost2);

                storyManager.availableUpgradesArraySM[6]++;
                storyManager.availableUpgradesArraySM[7] = 0;

                if (storyManager.availableUpgradesArraySM[6] >= 1)
                {
                    delugeMaxLevel = true;
                    TurnUpgradeOff("deluge");
                    delugeTextGO.GetComponent<TextMeshProUGUI>().text = "MaxDeluge";
                    delugeTextGO.SetActive(true);
                }
            }
        }

        if (activeUpgrade == "storm")
        {
            if (storyManager.availableUpgradesArraySM[9] == 0)
            {
                resourceManager.ReduceResource(typeOrganic, stormCurrentRarity1, stormCurrentCost1);
                resourceManager.ReduceResource(typeOrganic, stormCurrentRarity2, stormCurrentCost2);

                storyManager.availableUpgradesArraySM[9] = 1;
            }
            else
            {
                resourceManager.ReduceResource(typeInorganic, stormCurrentRarity1, stormCurrentCost1);
                resourceManager.ReduceResource(typeInorganic, stormCurrentRarity2, stormCurrentCost2);

                storyManager.availableUpgradesArraySM[8]++;
                storyManager.availableUpgradesArraySM[9] = 0;

                if (storyManager.availableUpgradesArraySM[8] >= 1)
                {
                    stormMaxLevel = true;
                    TurnUpgradeOff("storm");
                    stormTextGO.GetComponent<TextMeshProUGUI>().text = "MaxStorm";
                    stormTextGO.SetActive(true);
                }
            }
        }

        if (activeUpgrade == "freeze")
        {
            if (storyManager.availableUpgradesArraySM[11] == 0)
            {
                resourceManager.ReduceResource(typeOrganic, freezeCurrentRarity1, freezeCurrentCost1);
                resourceManager.ReduceResource(typeOrganic, freezeCurrentRarity2, freezeCurrentCost2);

                storyManager.availableUpgradesArraySM[11] = 1;
            }
            else
            {
                resourceManager.ReduceResource(typeInorganic, freezeCurrentRarity1, freezeCurrentCost1);
                resourceManager.ReduceResource(typeInorganic, freezeCurrentRarity2, freezeCurrentCost2);

                storyManager.availableUpgradesArraySM[10]++;
                storyManager.availableUpgradesArraySM[11] = 0;

                if (storyManager.availableUpgradesArraySM[10] >= 1)
                {
                    freezeMaxLevel = true;
                    TurnUpgradeOff("freeze");
                    freezeTextGO.GetComponent<TextMeshProUGUI>().text = "MaxFreeze";
                    freezeTextGO.SetActive(true);
                }
            }
        }

        CloseUpgradePromptPanel();
    }

    private void UpdateTestingUpgradeCosts(string[] rarity, int[] cost)
    {
        //Emitter UpgradeCosts
        r1EmitterRarity1 = rarity[0];
        r1EmitterCost1 = cost[0];
        r1EmitterRarity2 = rarity[1];
        r1EmitterCost2 = cost[1];

        b1EmitterRarity1 = rarity[2];
        b1EmitterCost1 = cost[2];
        b1EmitterRarity2 = rarity[3];
        b1EmitterCost2 = cost[3];

        r2EmitterRarity1 = rarity[4];
        r2EmitterCost1 = cost[4];
        r2EmitterRarity2 = rarity[5];
        r2EmitterCost2 = cost[5];

        b2EmitterRarity1 = rarity[6];
        b2EmitterCost1 = cost[6];
        b2EmitterRarity2 = rarity[7];
        b2EmitterCost2 = cost[7];

        r3EmitterRarity1 = rarity[8];
        rEmitterCost1 = cost[8];
        r3EmitterRarity2 = rarity[9];
        r3EmitterCost2 = cost[9];

        b3EmitterRarity1 = rarity[10];
        b3EmitterCost1 = cost[10];
        b3EmitterRarity2 = rarity[11];
        b3EmitterCost2 = cost[11];

        //battery Upgrade Costs
        r1BatteryRarity1 = rarity[12];
        r1BatteryCost1 = cost[12];
        r1BatteryRarity2 = rarity[13];
        r1BatteryCost2 = cost[13];

        b1BatteryRarity1 = rarity[14];
        b1BatteryCost1 = cost[14];
        b1BatteryRarity2 = rarity[15];
        b1BatteryCost2 = cost[15];

        r2BatteryRarity1 = rarity[16];
        r2BatteryCost1 = cost[16];
        r2BatteryRarity2 = rarity[17];
        r2BatteryCost2 = cost[17];

        b2BatteryRarity1 = rarity[18];
        b2BatteryCost1 = cost[18];
        b2BatteryRarity2 = rarity[19];
        b2BatteryCost2 = cost[19];

        r3BatteryRarity1 = rarity[20];
        r3BatteryCost1 = cost[20];
        r3BatteryRarity2 = rarity[21];
        r3BatteryCost2 = cost[21];

        b3BatteryRarity1 = rarity[22];
        b3BatteryCost1 = cost[22];
        b3BatteryRarity2 = rarity[23];
        b3BatteryCost2 = cost[23];

        r4BatteryRarity1 = rarity[24];
        r4BatteryCost1 = cost[24];
        r4BatteryRarity2 = rarity[25];
        r4BatteryCost2 = cost[25];

        b4BatteryRarity1 = rarity[26];
        b4BatteryCost1 = cost[26];
        b4BatteryRarity2 = rarity[27];
        b4BatteryCost2 = cost[27];

        r5BatteryRarity1 = rarity[28];
        r5BatteryCost1 = cost[28];
        r5BatteryRarity2 = rarity[29];
        r5BatteryCost2 = cost[29];

        b5BatteryRarity1 = rarity[30];
        b5BatteryCost1 = cost[30];
        b5BatteryRarity2 = rarity[31];
        b5BatteryCost2 = cost[31];

        //powers upgrade costs

        r1RainRarity1 = rarity[32];
        r1RainCost1 = cost[32];
        r1RainRarity2 = rarity[33];
        r1RainCost2 = cost[33];

        b1RainRarity1 = rarity[34];
        b1RainCost1 = cost[34];
        b1RainRarity2 = rarity[35];
        b1RainCost2 = cost[35];

        r1DelugeRarity1 = rarity[36];
        r1DelugeCost1 = cost[36];
        r1DelugeRarity2 = rarity[37];
        r1DelugeCost2 = cost[37];

        b1DelugeRarity1 = rarity[38];
        b1DelugeCost1 = cost[38];
        b1DelugeRarity2 = rarity[39];
        b1DelugeCost2 = cost[39];

        r1StormRarity1 = rarity[40];
        r1StormCost1 = cost[40];
        r1StormRarity2 = rarity[41];
        r1StormCost2 = cost[41];

        b1StormRarity1 = rarity[42];
        b1StormCost1 = cost[42];
        b1StormRarity2 = rarity[43];
        b1StormCost2 = cost[43];

        r1FreezeRarity1 = rarity[44];
        r1FreezeCost1 = cost[44];
        r1FreezeRarity2 = rarity[45];
        r1FreezeCost2 = cost[45];

        b1FreezeRarity1 = rarity[46];
        b1FreezeCost1 = cost[46];
        b1FreezeRarity2 = rarity[47];
        b1FreezeCost2 = cost[47];
    }
    
}
