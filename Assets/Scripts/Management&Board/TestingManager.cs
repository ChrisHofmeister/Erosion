using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestingManager : MonoBehaviour
{

    //managers
    private SceneLoader sceneLoader;
    private StoryManager storyManager;
       
    [SerializeField] GameObject[] testingPanels;
    public int testingPanelIndex;

    [SerializeField] TextMeshProUGUI[] allTestingTexts;

    public int allTextIndex;

    //**************input fields

    //emitter
    [SerializeField] TextMeshProUGUI emitter2RC1;
    [SerializeField] TextMeshProUGUI emitter2RT1;
    [SerializeField] TextMeshProUGUI emitter2RC2;
    [SerializeField] TextMeshProUGUI emitter2RT2;

    [SerializeField] TextMeshProUGUI emitter2BC1;
    [SerializeField] TextMeshProUGUI emitter2BT1;
    [SerializeField] TextMeshProUGUI emitter2BC2;
    [SerializeField] TextMeshProUGUI emitter2BT2;

    [SerializeField] TextMeshProUGUI emitter3RC1;
    [SerializeField] TextMeshProUGUI emitter3RT1;
    [SerializeField] TextMeshProUGUI emitter3RC2;
    [SerializeField] TextMeshProUGUI emitter3RT2;

    [SerializeField] TextMeshProUGUI emitter3BC1;
    [SerializeField] TextMeshProUGUI emitter3BT1;
    [SerializeField] TextMeshProUGUI emitter3BC2;
    [SerializeField] TextMeshProUGUI emitter3BT2;

    [SerializeField] TextMeshProUGUI emitter4RC1;
    [SerializeField] TextMeshProUGUI emitter4RT1;
    [SerializeField] TextMeshProUGUI emitter4RC2;
    [SerializeField] TextMeshProUGUI emitter4RT2;

    [SerializeField] TextMeshProUGUI emitter4BC1;
    [SerializeField] TextMeshProUGUI emitter4BT1;
    [SerializeField] TextMeshProUGUI emitter4BC2;
    [SerializeField] TextMeshProUGUI emitter4BT2;

    //battery
    [SerializeField] TextMeshProUGUI battery2RC1;
    [SerializeField] TextMeshProUGUI battery2RT1;
    [SerializeField] TextMeshProUGUI battery2RC2;
    [SerializeField] TextMeshProUGUI battery2RT2;

    [SerializeField] TextMeshProUGUI battery2BC1;
    [SerializeField] TextMeshProUGUI battery2BT1;
    [SerializeField] TextMeshProUGUI battery2BC2;
    [SerializeField] TextMeshProUGUI battery2BT2;

    [SerializeField] TextMeshProUGUI battery3RC1;
    [SerializeField] TextMeshProUGUI battery3RT1;
    [SerializeField] TextMeshProUGUI battery3RC2;
    [SerializeField] TextMeshProUGUI battery3RT2;

    [SerializeField] TextMeshProUGUI battery3BC1;
    [SerializeField] TextMeshProUGUI battery3BT1;
    [SerializeField] TextMeshProUGUI battery3BC2;
    [SerializeField] TextMeshProUGUI battery3BT2;

    [SerializeField] TextMeshProUGUI battery4RC1;
    [SerializeField] TextMeshProUGUI battery4RT1;
    [SerializeField] TextMeshProUGUI battery4RC2;
    [SerializeField] TextMeshProUGUI battery4RT2;

    [SerializeField] TextMeshProUGUI battery4BC1;
    [SerializeField] TextMeshProUGUI battery4BT1;
    [SerializeField] TextMeshProUGUI battery4BC2;
    [SerializeField] TextMeshProUGUI battery4BT2;

    [SerializeField] TextMeshProUGUI battery5RC1;
    [SerializeField] TextMeshProUGUI battery5RT1;
    [SerializeField] TextMeshProUGUI battery5RC2;
    [SerializeField] TextMeshProUGUI battery5RT2;

    [SerializeField] TextMeshProUGUI battery5BC1;
    [SerializeField] TextMeshProUGUI battery5BT1;
    [SerializeField] TextMeshProUGUI battery5BC2;
    [SerializeField] TextMeshProUGUI battery5BT2;

    [SerializeField] TextMeshProUGUI batter6RC1;
    [SerializeField] TextMeshProUGUI batter64RT1;
    [SerializeField] TextMeshProUGUI battery6RC2;
    [SerializeField] TextMeshProUGUI battery6RT2;

    [SerializeField] TextMeshProUGUI battery6BC1;
    [SerializeField] TextMeshProUGUI battery6BT1;
    [SerializeField] TextMeshProUGUI battery6BC2;
    [SerializeField] TextMeshProUGUI battery6BT2;

    //powers
    [SerializeField] TextMeshProUGUI rainRC1;
    [SerializeField] TextMeshProUGUI rainRT1;
    [SerializeField] TextMeshProUGUI rainRC2;
    [SerializeField] TextMeshProUGUI rainRT2;

    [SerializeField] TextMeshProUGUI rainBC1;
    [SerializeField] TextMeshProUGUI rainBT1;
    [SerializeField] TextMeshProUGUI rainBC2;
    [SerializeField] TextMeshProUGUI rainBT2;

    [SerializeField] TextMeshProUGUI delugeRC1;
    [SerializeField] TextMeshProUGUI delugeRT1;
    [SerializeField] TextMeshProUGUI delugeRC2;
    [SerializeField] TextMeshProUGUI delugeRT2;

    [SerializeField] TextMeshProUGUI delugeBC1;
    [SerializeField] TextMeshProUGUI delugeBT1;
    [SerializeField] TextMeshProUGUI delugeBC2;
    [SerializeField] TextMeshProUGUI delugeBT2;

    [SerializeField] TextMeshProUGUI stormRC1;
    [SerializeField] TextMeshProUGUI stormRT1;
    [SerializeField] TextMeshProUGUI stormRC2;
    [SerializeField] TextMeshProUGUI stormRT2;

    [SerializeField] TextMeshProUGUI stormBC1;
    [SerializeField] TextMeshProUGUI stormBT1;
    [SerializeField] TextMeshProUGUI stormBC2;
    [SerializeField] TextMeshProUGUI stormBT2;

    [SerializeField] TextMeshProUGUI freezeRC1;
    [SerializeField] TextMeshProUGUI freezeRT1;
    [SerializeField] TextMeshProUGUI freezeRC2;
    [SerializeField] TextMeshProUGUI freezeRT2;

    [SerializeField] TextMeshProUGUI freezeBC1;
    [SerializeField] TextMeshProUGUI freezeBT1;
    [SerializeField] TextMeshProUGUI freezeBC2;
    [SerializeField] TextMeshProUGUI freezeBT2;

    private int[] digits;

    //arrays to hold the data to be passed to storyManager
    public string[] testingUpgradeTypes;
    public int[] testingUpgradeCosts;



    private void Awake()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        storyManager = FindObjectOfType<StoryManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        testingPanelIndex = 0;
        testingUpgradeTypes = new string[48];
        testingUpgradeCosts = new int[48];   
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //button controls
    public void NextButton()
    {
        if(testingPanelIndex < testingPanels.Length - 1)
        {
            testingPanels[testingPanelIndex].SetActive(false);
            testingPanelIndex++;
            testingPanels[testingPanelIndex].SetActive(true);
        }
        else
        {
            testingPanels[testingPanelIndex].SetActive(false);
            testingPanelIndex = 0;
            testingPanels[testingPanelIndex].SetActive(true);
        }
    }

    public void BackButton()
    {
        if (testingPanelIndex > 0)
        {
            testingPanels[testingPanelIndex].SetActive(false);
            testingPanelIndex--;
            testingPanels[testingPanelIndex].SetActive(true);
        }
        else
        {
            testingPanels[testingPanelIndex].SetActive(false);
            testingPanelIndex = 2;
            testingPanels[testingPanelIndex].SetActive(true);
        }
    }

    public void StartButton()
    {
        int allTextIndex = 0;
        int t = 0;
        int c = 0;
        bool isInt;
        
        int placeHolderInt;
        string placeHolderString;
        string cleanString;

        foreach(TextMeshProUGUI text in allTestingTexts)
        {
            if(allTextIndex % 2 == 0)
            {
                testingUpgradeTypes[t] = text.text[0].ToString();
                t++;
            }
            else
            {
                //for(int i = 0; i < text.text.Length)
                placeHolderString = text.text.ToString();

                if (placeHolderString.Length == 2)
                {
                    isInt = int.TryParse(placeHolderString[0].ToString(), out placeHolderInt);
                    testingUpgradeCosts[c] = placeHolderInt;
                }
                if(placeHolderString.Length == 3)
                {
                    cleanString = placeHolderString[0].ToString();
                    cleanString = cleanString + placeHolderString[1].ToString();
                    isInt = int.TryParse(cleanString, out placeHolderInt);
                    testingUpgradeCosts[c] = placeHolderInt;
                }
                
                c++;
            }

            allTextIndex++;
        }

        storyManager.testingUpgradeTypesSM = testingUpgradeTypes;
        storyManager.testingUpgradeCostsSM = testingUpgradeCosts;
        storyManager.testingModeActive = true;

        sceneLoader.LoadStoryMapScreen();
    }
}
