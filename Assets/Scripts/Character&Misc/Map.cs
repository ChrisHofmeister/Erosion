using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    //managers
    private StoryManager storyManager;
    private SceneLoader sceneLoader;

    public int mapProgress;
    public int stageProgress;    

    private int currentStageProgress;

    [SerializeField] GameObject[] stageButtonsArray;   
    [SerializeField] GameObject[] maps;

    //variables for startend river
    private string riverStartSide;
    private int riverStartPos;
    private string riverEndSide;
    private int riverEndPos;

    private int stageNumber;

    public Vector2 stageRiverStartPos;
    public Vector2 stageRiverEndPos;   
    

    private void Awake()
    {
        storyManager = FindObjectOfType<StoryManager>();      
        
        sceneLoader = FindObjectOfType<SceneLoader>();
        stageProgress = -1;
        mapProgress = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForStagesToDisplay();
    }

    private void CheckForStagesToDisplay()
    {
        if(stageProgress < storyManager.stageProgress)
        {
            stageProgress = storyManager.stageProgress;

            for(int i = 0; i <=  stageProgress; i++)
            {
                stageButtonsArray[i].SetActive(true);
            }
        }
        if(mapProgress < storyManager.mapProgress)
        {
            maps[mapProgress].SetActive(false);
            mapProgress = storyManager.mapProgress;
            maps[mapProgress].SetActive(true);            
        }
    }

    private void MapLoader()
    {
        stageRiverStartPos = SetUpRiverPos(riverStartSide, riverStartPos);
        stageRiverEndPos = SetUpRiverPos(riverEndSide, riverEndPos);

        storyManager.SetUpStageLoad(stageNumber, stageRiverStartPos, stageRiverEndPos);


        sceneLoader.LoadStoryStageScreen();
    }

    public void PlayStage1()
    {
        stageNumber = 1;

        riverStartSide = "d";        
        riverEndSide = "u";
        if(storyManager.boardSizeSM == 9)
        {
            riverStartPos = 1;
            riverEndPos = 1;
        }
        if (storyManager.boardSizeSM == 16)
        {
            riverStartPos = 1;
            riverEndPos = 2;
        }
        if (storyManager.boardSizeSM == 25)
        {
            riverStartPos = 2;
            riverEndPos = 2;
        }
        if (storyManager.boardSizeSM == 36)
        {
            riverStartPos = 1;
            riverEndPos = 4;
        }
        MapLoader();
    }

    public void PlayStage2()
    {
        stageNumber = 2;

        riverStartSide = "d";
        riverEndSide = "r";
        if (storyManager.boardSizeSM == 9)
        {
            riverStartPos = 1;
            riverEndPos = 1;
        }
        if (storyManager.boardSizeSM == 16)
        {
            riverStartPos = 1;
            riverEndPos = 2;
        }
        if (storyManager.boardSizeSM == 25)
        {
            riverStartPos = 1;
            riverEndPos = 3;
        }
        if (storyManager.boardSizeSM == 36)
        {
            riverStartPos = 1;
            riverEndPos = 3;
        }
        MapLoader();
    }

    public void PlayStage3()
    {
        stageNumber = 3;

        riverStartSide = "l";
        riverEndSide = "r";
        if (storyManager.boardSizeSM == 9)
        {
            riverStartPos = 1;
            riverEndPos = 1;
        }
        if (storyManager.boardSizeSM == 16)
        {
            riverStartPos = 2;
            riverEndPos = 2;
        }
        if (storyManager.boardSizeSM == 25)
        {
            riverStartPos = 3;
            riverEndPos = 1;
        }
        if (storyManager.boardSizeSM == 36)
        {
            riverStartPos = 1;
            riverEndPos = 4;
        }
        MapLoader();
    }

    public void PlayStage4()
    {
        stageNumber = 4;

        riverStartSide = "l";
        riverEndSide = "u";
        if (storyManager.boardSizeSM == 9)
        {
            riverStartPos = 1;
            riverEndPos = 1;
        }
        if (storyManager.boardSizeSM == 16)
        {
            riverStartPos = 1;
            riverEndPos = 2;
        }
        if (storyManager.boardSizeSM == 25)
        {
            riverStartPos = 2;
            riverEndPos = 3;
        }
        if (storyManager.boardSizeSM == 36)
        {
            riverStartPos = 2;
            riverEndPos = 4;
        }
        MapLoader();
    }

    public void PlayStage5()
    {
        stageNumber = 5;

        riverStartSide = "d";
        riverEndSide = "u";
        if (storyManager.boardSizeSM == 9)
        {
            riverStartPos = 1;
            riverEndPos = 1;
        }
        if (storyManager.boardSizeSM == 16)
        {
            riverStartPos = 1;
            riverEndPos = 2;
        }
        if (storyManager.boardSizeSM == 25)
        {
            riverStartPos = 2;
            riverEndPos = 2;
        }
        if (storyManager.boardSizeSM == 36)
        {
            riverStartPos = 1;
            riverEndPos = 4;
        }
        MapLoader();
    }
    public void PlayStage6()
    {
        stageNumber = 6;

        riverStartSide = "d";
        riverEndSide = "u";
        if (storyManager.boardSizeSM == 9)
        {
            riverStartPos = 1;
            riverEndPos = 1;
        }
        if (storyManager.boardSizeSM == 16)
        {
            riverStartPos = 1;
            riverEndPos = 2;
        }
        if (storyManager.boardSizeSM == 25)
        {
            riverStartPos = 2;
            riverEndPos = 2;
        }
        if (storyManager.boardSizeSM == 36)
        {
            riverStartPos = 1;
            riverEndPos = 4;
        }
        MapLoader();
    }

    public void PlayStage7()
    {
        stageNumber = 7;

        riverStartSide = "d";
        riverEndSide = "r";
        if (storyManager.boardSizeSM == 9)
        {
            riverStartPos = 1;
            riverEndPos = 1;
        }
        if (storyManager.boardSizeSM == 16)
        {
            riverStartPos = 1;
            riverEndPos = 2;
        }
        if (storyManager.boardSizeSM == 25)
        {
            riverStartPos = 1;
            riverEndPos = 3;
        }
        if (storyManager.boardSizeSM == 36)
        {
            riverStartPos = 1;
            riverEndPos = 3;
        }
        MapLoader();
    }

    public void PlayStage8()
    {
        stageNumber = 8;

        riverStartSide = "l";
        riverEndSide = "r";
        if (storyManager.boardSizeSM == 9)
        {
            riverStartPos = 1;
            riverEndPos = 1;
        }
        if (storyManager.boardSizeSM == 16)
        {
            riverStartPos = 2;
            riverEndPos = 2;
        }
        if (storyManager.boardSizeSM == 25)
        {
            riverStartPos = 3;
            riverEndPos = 1;
        }
        if (storyManager.boardSizeSM == 36)
        {
            riverStartPos = 1;
            riverEndPos = 4;
        }
        MapLoader();
    }

    public void PlayStage9()
    {
        stageNumber = 9;

        riverStartSide = "l";
        riverEndSide = "u";
        if (storyManager.boardSizeSM == 9)
        {
            riverStartPos = 1;
            riverEndPos = 1;
        }
        if (storyManager.boardSizeSM == 16)
        {
            riverStartPos = 1;
            riverEndPos = 2;
        }
        if (storyManager.boardSizeSM == 25)
        {
            riverStartPos = 2;
            riverEndPos = 3;
        }
        if (storyManager.boardSizeSM == 36)
        {
            riverStartPos = 2;
            riverEndPos = 4;
        }
        MapLoader();
    }

    public void PlayStage10()
    {
        stageNumber = 10;

        riverStartSide = "d";
        riverEndSide = "u";
        if (storyManager.boardSizeSM == 9)
        {
            riverStartPos = 1;
            riverEndPos = 1;
        }
        if (storyManager.boardSizeSM == 16)
        {
            riverStartPos = 1;
            riverEndPos = 2;
        }
        if (storyManager.boardSizeSM == 25)
        {
            riverStartPos = 2;
            riverEndPos = 2;
        }
        if (storyManager.boardSizeSM == 36)
        {
            riverStartPos = 1;
            riverEndPos = 4;
        }
        MapLoader();
    }
    public void PlayStage11()
    {
        stageNumber = 11;

        riverStartSide = "d";
        riverEndSide = "u";
        if (storyManager.boardSizeSM == 9)
        {
            riverStartPos = 1;
            riverEndPos = 1;
        }
        if (storyManager.boardSizeSM == 16)
        {
            riverStartPos = 1;
            riverEndPos = 2;
        }
        if (storyManager.boardSizeSM == 25)
        {
            riverStartPos = 2;
            riverEndPos = 2;
        }
        if (storyManager.boardSizeSM == 36)
        {
            riverStartPos = 1;
            riverEndPos = 4;
        }
        MapLoader();
    }

    public void PlayStage12()
    {
        stageNumber = 12;

        riverStartSide = "d";
        riverEndSide = "r";
        if (storyManager.boardSizeSM == 9)
        {
            riverStartPos = 1;
            riverEndPos = 1;
        }
        if (storyManager.boardSizeSM == 16)
        {
            riverStartPos = 1;
            riverEndPos = 2;
        }
        if (storyManager.boardSizeSM == 25)
        {
            riverStartPos = 1;
            riverEndPos = 3;
        }
        if (storyManager.boardSizeSM == 36)
        {
            riverStartPos = 1;
            riverEndPos = 3;
        }
        MapLoader();
    }

    public void PlayStage13()
    {
        stageNumber = 13;

        riverStartSide = "l";
        riverEndSide = "r";
        if (storyManager.boardSizeSM == 9)
        {
            riverStartPos = 1;
            riverEndPos = 1;
        }
        if (storyManager.boardSizeSM == 16)
        {
            riverStartPos = 2;
            riverEndPos = 2;
        }
        if (storyManager.boardSizeSM == 25)
        {
            riverStartPos = 3;
            riverEndPos = 1;
        }
        if (storyManager.boardSizeSM == 36)
        {
            riverStartPos = 1;
            riverEndPos = 4;
        }
        MapLoader();
    }

    public void PlayStage14()
    {
        stageNumber = 14;

        riverStartSide = "l";
        riverEndSide = "u";
        if (storyManager.boardSizeSM == 9)
        {
            riverStartPos = 1;
            riverEndPos = 1;
        }
        if (storyManager.boardSizeSM == 16)
        {
            riverStartPos = 1;
            riverEndPos = 2;
        }
        if (storyManager.boardSizeSM == 25)
        {
            riverStartPos = 2;
            riverEndPos = 3;
        }
        if (storyManager.boardSizeSM == 36)
        {
            riverStartPos = 2;
            riverEndPos = 4;
        }
        MapLoader();
    }

    public void PlayStage15()
    {
        stageNumber = 15;

        riverStartSide = "d";
        riverEndSide = "u";
        if (storyManager.boardSizeSM == 9)
        {
            riverStartPos = 1;
            riverEndPos = 1;
        }
        if (storyManager.boardSizeSM == 16)
        {
            riverStartPos = 1;
            riverEndPos = 2;
        }
        if (storyManager.boardSizeSM == 25)
        {
            riverStartPos = 2;
            riverEndPos = 2;
        }
        if (storyManager.boardSizeSM == 36)
        {
            riverStartPos = 1;
            riverEndPos = 4;
        }
        MapLoader();
    }
    public void PlayStage16()
    {
        stageNumber = 16;

        riverStartSide = "d";
        riverEndSide = "u";
        if (storyManager.boardSizeSM == 9)
        {
            riverStartPos = 1;
            riverEndPos = 1;
        }
        if (storyManager.boardSizeSM == 16)
        {
            riverStartPos = 1;
            riverEndPos = 2;
        }
        if (storyManager.boardSizeSM == 25)
        {
            riverStartPos = 2;
            riverEndPos = 2;
        }
        if (storyManager.boardSizeSM == 36)
        {
            riverStartPos = 1;
            riverEndPos = 4;
        }
        MapLoader();
    }

    public void PlayStage17()
    {
        stageNumber = 17;

        riverStartSide = "d";
        riverEndSide = "r";
        if (storyManager.boardSizeSM == 9)
        {
            riverStartPos = 1;
            riverEndPos = 1;
        }
        if (storyManager.boardSizeSM == 16)
        {
            riverStartPos = 1;
            riverEndPos = 2;
        }
        if (storyManager.boardSizeSM == 25)
        {
            riverStartPos = 1;
            riverEndPos = 3;
        }
        if (storyManager.boardSizeSM == 36)
        {
            riverStartPos = 1;
            riverEndPos = 3;
        }
        MapLoader();
    }

    public void PlayStage18()
    {
        stageNumber = 18;

        riverStartSide = "l";
        riverEndSide = "r";
        if (storyManager.boardSizeSM == 9)
        {
            riverStartPos = 1;
            riverEndPos = 1;
        }
        if (storyManager.boardSizeSM == 16)
        {
            riverStartPos = 2;
            riverEndPos = 2;
        }
        if (storyManager.boardSizeSM == 25)
        {
            riverStartPos = 3;
            riverEndPos = 1;
        }
        if (storyManager.boardSizeSM == 36)
        {
            riverStartPos = 1;
            riverEndPos = 4;
        }
        MapLoader();
    }

    public void PlayStage19()
    {
        stageNumber = 19;

        riverStartSide = "l";
        riverEndSide = "u";
        if (storyManager.boardSizeSM == 9)
        {
            riverStartPos = 1;
            riverEndPos = 1;
        }
        if (storyManager.boardSizeSM == 16)
        {
            riverStartPos = 1;
            riverEndPos = 2;
        }
        if (storyManager.boardSizeSM == 25)
        {
            riverStartPos = 2;
            riverEndPos = 3;
        }
        if (storyManager.boardSizeSM == 36)
        {
            riverStartPos = 2;
            riverEndPos = 4;
        }
        MapLoader();
    }

    public void PlayStage20()
    {
        stageNumber = 20;

        riverStartSide = "d";
        riverEndSide = "u";
        if (storyManager.boardSizeSM == 9)
        {
            riverStartPos = 1;
            riverEndPos = 1;
        }
        if (storyManager.boardSizeSM == 16)
        {
            riverStartPos = 1;
            riverEndPos = 2;
        }
        if (storyManager.boardSizeSM == 25)
        {
            riverStartPos = 2;
            riverEndPos = 2;
        }
        if (storyManager.boardSizeSM == 36)
        {
            riverStartPos = 1;
            riverEndPos = 4;
        }
        MapLoader();
    }

    private Vector2 SetUpRiverPos(string riverSide, int riverPos)
    {
        if (riverSide == "u")
        {
            return new Vector2(riverPos + 1, Mathf.Sqrt(storyManager.boardSizeSM) + 1);
        }
        else if (riverSide == "d")
        {
            return new Vector2(riverPos + 1, 0);
        }
        else if (riverSide == "r")
        {
            return new Vector2(Mathf.Sqrt(storyManager.boardSizeSM) + 1, riverPos + 1);
        }
        else
        {
            return new Vector2(0f, riverPos + 1);
        }
    }

}
