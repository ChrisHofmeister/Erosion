using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{

    public int stageProgress;
    public bool[] stageProgressArray;
    public int mapProgress;
    public int stageIndex;
    

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
    public bool mapModeActive = false;
    public bool stageModeActive = true;

    //vars for testing mode
    public bool testingModeActive;
    public string[] testingUpgradeTypesSM;
    public int[] testingUpgradeCostsSM;

    //arrays of data from managers to be updated when they are recreated

    // even are  the upgrades level and odd is the research or build mode indicators
    //0-emitter, 2-battery, 4-rain, 6-deluge, 8-storm, 10-freeze 
    public int[] availableUpgradesArraySM;

    //0-CO, 1-UO, 2-RO, 3-CI, 4-UI, 5-RI
    public int[] availableResourcesArraySM;

    private void Awake()
    {
        SetUpSingleton();
        availableUpgradesArraySM = new int[12];
        availableResourcesArraySM = new int[6];
        stageProgressArray = new bool[20];
        testingUpgradeTypesSM = new string[48];
        testingUpgradeCostsSM = new int[48];
        ResetAllUpgrades();
        stageProgress = 0;
        mapProgress = 0;
    }
    // Start is called before the first frame update
    void Start()
    {

        sceneLoader = FindObjectOfType<SceneLoader>();

        availableUpgradesArraySM[0] = 0;
        availableUpgradesArraySM[2] = 0;

        mapModeActive = true;
        stageModeActive = false;

    }

    // Update is called once per frame
    void Update()
    {
        boardSizeSM = (availableUpgradesArraySM[0] + 3) * (availableUpgradesArraySM[0] + 3);
        batterySizeSM = 10 + (availableUpgradesArraySM[2] * 5);
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
        if(stageProgress == 1)
        {
            availableUpgradesArraySM[4] = 0;
        }
        if(stageProgress == 5)
        {
            availableUpgradesArraySM[6] = 0;
        }
        if (stageProgress == 10)
        {
            availableUpgradesArraySM[8] = 0;
        }
        if (stageProgress == 15)
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


}
