using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{

    public int stageProgress;
    public bool[] stageProgressArray;
    public int mapProgress;
    public int stageIndex;

    //save data bool
    public bool saveDataExists;

    [SerializeField] GameObject[] stageButtonsArray;

    [SerializeField] GameObject[] availableEarthTiles;
    public GameObject[] stageTiles;

    public Vector2 stageRiverStartPos;
    public Vector2 stageRiverEndPos;
    public int boardSizeSM;
    public int batterySizeSM;

    //variables for startend river
    private string riverStartSide;
    private int riverStartPos;
    private string riverEndSide;
    private int riverEndPos;

    //managers
    private SceneLoader sceneLoader;


    //bools for map mode and stage mode
    public bool mapModeActive;
    public bool stageModeActive;

    //vars for testing mode
    public bool testingModeActive;
    public string[] testingUpgradeTypesSM;
    public int[] testingUpgradeCostsSM;

    //bool to see if save data was loaded
    public bool dataLoadedFromSave;

    //arrays of data from managers to be updated when they are recreated

    // even are  the upgrades level and odd is the research or build mode indicators
    //0-emitter, 2-battery, 4-rain, 6-deluge, 8-storm, 10-freeze 
    public int[] availableUpgradesArraySM;

    //0-CO, 1-UO, 2-RO, 3-CI, 4-UI, 5-RI
    public int[] availableResourcesArraySM;

    //checklist bool array
    public bool[] checklistComplete;
    public bool allChecklistItemsComplete;

    private void Awake()
    {
        SetUpSingleton();
        availableUpgradesArraySM = new int[12];
        availableResourcesArraySM = new int[6];
        stageProgressArray = new bool[20];
        testingUpgradeTypesSM = new string[48];
        testingUpgradeCostsSM = new int[48];
        checklistComplete = new bool[4];

        if (!dataLoadedFromSave)
        {
            ResetAllUpgrades();
        }

        stageProgress = 0;
        mapProgress = 0;

        CheckForSaveData();
    }
    // Start is called before the first frame update
    void Start()
    {

        sceneLoader = FindObjectOfType<SceneLoader>();

        if (!dataLoadedFromSave)
        {
            availableUpgradesArraySM[0] = 0;
            availableUpgradesArraySM[2] = 0;
        }

        mapModeActive = true;
        stageModeActive = false;


       

    }

    // Update is called once per frame
    void Update()
    {
        boardSizeSM = (availableUpgradesArraySM[0] + 3) * (availableUpgradesArraySM[0] + 3);
        batterySizeSM = 10 + (availableUpgradesArraySM[2] * 5);
        CheckForCompletedChecklist();
    }

    private void SetUpSingleton()
    {
        if(FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void UpgradeProgress()
    {
        if(stageProgress == 0)
        {
            availableUpgradesArraySM[4] = 0;
        }
        if(stageProgress == 6)
        {
            availableUpgradesArraySM[6] = 0;
        }
        if (stageProgress == 11)
        {
            availableUpgradesArraySM[8] = 0;
        }
        if (stageProgress == 16)
        {
            availableUpgradesArraySM[10] = 0;
        }

    }

    public void SetUpStageLoad(int stageNumber, Vector2 riverStart, Vector2 riverEnd)
    {
        stageIndex = stageNumber - 1;
        stageRiverStartPos = riverStart;
        stageRiverEndPos = riverEnd;

        SwitchMapStageMode();

    }

    
    public void SwitchMapStageMode()
    {
        if(mapModeActive == false)
        {
            mapModeActive = true;
        }
        else
        {
            mapModeActive = false;
        }

        if (stageModeActive == false)
        {
            stageModeActive = true;
        }
        else
        {
            stageModeActive = false;
        }

        UpgradeProgress();
    }

    private void ResetAllUpgrades()
    {
        for (int i = 0; i < availableUpgradesArraySM.Length; i++)
        {
            if (i % 2 == 0)
            {
                availableUpgradesArraySM[i] = -1;
            }
            else
            {
                availableUpgradesArraySM[i] = 0;
            }
        }
    }

    public void SaveStoryManager()
    {
        SaveSystem.SaveStoryManager(this);

    }

    public void LoadPlayerDataIntoStoryMode(PlayerData data)
    {

        dataLoadedFromSave = true;

        stageProgress = data.stageProgressPD;
        stageProgressArray = data.stageProgressArrayPD;
        mapProgress = data.mapProgressPD;

        availableUpgradesArraySM = data.availableUpgradesArrayPD;
        availableResourcesArraySM = data.availableResourcesArrayPD;

        testingModeActive = data.testingModeActivePD;
        testingUpgradeTypesSM = data.testingUpgradeTypesPD;
        testingUpgradeCostsSM = data.testingUpgradeCostsPD;


    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void CheckForSaveData()
    {
        saveDataExists = SaveSystem.SaveDataExists();
    }

    private void CheckForCompletedChecklist()
    {
        int numberOfCompletedItems = 0;

        foreach(bool item in checklistComplete)
        {
            if (item)
            {
                numberOfCompletedItems++;
            }
        }

        if(numberOfCompletedItems == checklistComplete.Length)
        {
            allChecklistItemsComplete = true;
        }
        else
        {
            allChecklistItemsComplete = false;
        }
    }
   
}
