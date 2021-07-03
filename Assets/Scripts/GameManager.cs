using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //erosion point so Gm can handle timing and order of opperations
    private ErosionPoint erosionPoint;

    //board so GM knows when game has sarted
    private Board board;

    //bool so you only set erosion point once
    private bool erosionPointSet;

    //delay, in seconds, so various action can complete before mving on
    private float lerpDelay = .6f;

    //array for all earth tiles used by the update tag and position method below
    private GameObject[] allTilesWithEarthTag;

    //array for all waterkeep tiles and all water tiles
    private GameObject[] allWaterTiles;
    private GameObject[] allWaterKeepTiles;
    private Water waterTileScript;

    //gameobject canvas for main, momentumlost, and ending
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject alertPanel;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private GameObject retryPanel;

    //alert messaging game objects
    [SerializeField] private GameObject momentumLostText;
    [SerializeField] private GameObject powerLowText;

    //collider cover, due to strange bug, no longer serialized.  aquires ref at start using tag.  fixed bug, but left it as is
    private GameObject colliderCover;

    //character emote stuff
    [SerializeField] private CharacterEmote characterEmote;

    //text to be updated
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI turnTimerText;

    //turn timer and score
    public bool reachedEdgeBeforeTimerZero;
    public int turnTimer;
    public int score = 0;

    //endgame panel stuff
    [SerializeField] private GameObject niceWorkHeader;
    [SerializeField] private GameObject batteryDeadHeader;
    [SerializeField] private GameObject prettyCloseHeader;
    [SerializeField] private GameObject exitMissedHeader;
    [SerializeField] TextMeshProUGUI endgameCurrentScoreValue;
    [SerializeField] TextMeshProUGUI endgameSoilBonusValue;
    [SerializeField] TextMeshProUGUI endgameBatteryBonusValue;
    [SerializeField] TextMeshProUGUI endgameEndPointValue;
    [SerializeField] TextMeshProUGUI endgameFinalScoreValue;

    private int egCurrentScore = 0;
    private int egSoilBonus = 0;
    private int egBatteryBonus = 0;
    public int egEndPointBonus = 0;
    private int egFinalScore = 0;

    private Vector3 gameEndingTilePos;

    //retry panel stuff
    [SerializeField] TextMeshProUGUI retryFinalScoreValue;


    //skip turn button
    [SerializeField] private GameObject skipMoveButton;

    //water tile scripts for edge check
    private Water[] waterTileScripts;



    // Start is called before the first frame update
    void Start()
    {

        colliderCover = GameObject.FindGameObjectWithTag("collidercover");
        colliderCover.SetActive(false);
        erosionPointSet = false;
        board = FindObjectOfType<Board>();
        endGamePanel.SetActive(false);
        alertPanel.SetActive(false);
        momentumLostText.SetActive(false);
        powerLowText.SetActive(false);
        niceWorkHeader.SetActive(false);
        batteryDeadHeader.SetActive(false);
        SetTurnTimer(board.boardSize);
        scoreText.text = score.ToString();
        reachedEdgeBeforeTimerZero = false;
        retryPanel.SetActive(false);
        exitMissedHeader.SetActive(false);
        prettyCloseHeader.SetActive(false);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //called in Earth Tile on mouse up, once movement has started.
    public void Erosion()
    {
        //keep multiple events from triggering by turning on the collider cover and off the skip move button
        //switch back at the end of lerpDelay()
        colliderCover.SetActive(true);
        skipMoveButton.SetActive(false);
       
        //setting erosion point only once
        if(!erosionPointSet)
        {
            erosionPoint = FindObjectOfType<ErosionPoint>();
            erosionPointSet = true;
        }        
        UpdateAllTileArrayPosition();
        DisableWaterTiles();
        RetagAllWaterKeepTiles();
        erosionPoint.SourceCheck();        
        while (erosionPoint.willMove)
        {            
            erosionPoint.FindErosionDirection();
            erosionPoint.MoveChecker();
            erosionPoint.TagWaterAsWaterKeep();
        }
        DisableWaterTiles();

        StartCoroutine(LerpDelay());

    }

    //after delay, erosion takes palce and rest of actions can occur
    IEnumerator LerpDelay()
    {

        yield return new WaitForSeconds(lerpDelay);

        DisableWaterTiles();
        RetagAllWaterKeepTiles();
        UpdateAllEarthTilePosition();
        erosionPoint.SourceCheck();        
        while (erosionPoint.willMove)
        {
            erosionPoint.FindErosionDirection();
            erosionPoint.MoveChecker();
            erosionPoint.TagWaterAsWaterKeep();
        }        
        erosionPoint.FindErosionTargets();
        erosionPoint.ErodeTarget();
        UpdateAllEarthTilePosition();
        erosionPoint.SourceCheck();
        
        while (erosionPoint.willMove)
        {
            erosionPoint.FindErosionDirection();
            erosionPoint.MoveChecker();
            erosionPoint.TagWaterAsWaterKeep();
        }
        DisableWaterTiles();

        StartCoroutine(EndCheck());
        
    }

    public void SkipMoveErosion()
    {        
        //keep multiple events from triggering by turning on the collider cover and off the skip move button
        //switch back at the end of lerpDelay()
        colliderCover.SetActive(true);
        skipMoveButton.SetActive(false);

        //setting erosion point only once
        if (!erosionPointSet)
        {
            erosionPoint = FindObjectOfType<ErosionPoint>();
            erosionPointSet = true;
        }
        if(board.gameStarted == false)
        {
            board.gameStarted = true;
        }
        UpdateAllTileArrayPosition();
        DisableWaterTiles();
        RetagAllWaterKeepTiles();
        erosionPoint.SourceCheck();
        while (erosionPoint.willMove)
        {
            erosionPoint.FindErosionDirection();
            erosionPoint.MoveChecker();
            erosionPoint.TagWaterAsWaterKeep();
        }
        DisableWaterTiles();

        StartCoroutine(LerpDelay());

    }

    private void UpdateAllEarthTilePosition()
    {
        allTilesWithEarthTag = GameObject.FindGameObjectsWithTag("earthtile");

        foreach(GameObject tile in allTilesWithEarthTag)
        {
            EarthTile parentEarthTile = tile.GetComponentInParent<EarthTile>();
            tile.transform.parent.tag = parentEarthTile.column + "," + parentEarthTile.row;            
            board.allTilesArray[parentEarthTile.column, parentEarthTile.row] = tile.transform.parent.gameObject;
            board.allEarthTiles[parentEarthTile.column, parentEarthTile.row] = tile.transform.parent.gameObject;         

        }
    }

    private void UpdateAllTileArrayPosition()
    {
        allTilesWithEarthTag = GameObject.FindGameObjectsWithTag("earthtile");

        foreach (GameObject tile in allTilesWithEarthTag)
        {
            EarthTile parentEarthTile = tile.GetComponentInParent<EarthTile>();            
            board.allTilesArray[parentEarthTile.column, parentEarthTile.row] = tile.transform.parent.gameObject;
        }
    }

    private void DisableWaterTiles()
    {
        allWaterTiles = GameObject.FindGameObjectsWithTag("water");

        foreach(GameObject waterTile in allWaterTiles)
        {
            if (waterTile.GetComponent<Water>().waterOn == true)
            {
                waterTile.GetComponent<Water>().WaterOff();
            }

        }
    }

    private void RetagAllWaterKeepTiles()
    {
        allWaterTiles = GameObject.FindGameObjectsWithTag("waterkeep");

        foreach (GameObject waterKeepTile in allWaterTiles)
        {
            waterKeepTile.tag = "water";
        }
    }
    //needed to wait for very small amount of time for the bedrock script to complete setup before checking for edge
    IEnumerator EndCheck()
    {

        UpdateTimer();

        yield return new WaitForSeconds(.1f);



        waterTileScripts = FindObjectsOfType<Water>();

        foreach(Water waterTile in waterTileScripts)
        {
            if (waterTile.EdgeCheck())
            {
                reachedEdgeBeforeTimerZero = true;
                gameEndingTilePos = waterTile.transform.position;
                StartCoroutine(EndGame());
            }            
        }

        if (turnTimer == 5 && reachedEdgeBeforeTimerZero != true)
        {
            StartCoroutine(PowerLowAlert());
        }

        if(turnTimer == 0 && reachedEdgeBeforeTimerZero != true)
        {
            StartCoroutine(EndGame());
        }

        if(turnTimer != 5 && reachedEdgeBeforeTimerZero != true)
        {
            colliderCover.SetActive(false);
            skipMoveButton.SetActive(true);
        }

    }
    //change turn timer depending on boardsize then update timer text
    private void SetTurnTimer(int boardSize)
    {
        if(boardSize == 3)
        {
            turnTimer = 10;
        }

        if (boardSize == 4)
        {
            turnTimer = 15;
        }

        if (boardSize == 5)
        {
            turnTimer = 20;
        }

        if (boardSize == 6)
        {
            turnTimer = 25;
        }

        turnTimerText.text = turnTimer.ToString();
    }

    private void UpdateTimer()
    {
        turnTimer--;
        turnTimerText.text = turnTimer.ToString();
    }

    IEnumerator PowerLowAlert()
    {
        powerLowText.SetActive(true);
        alertPanel.SetActive(true);
        colliderCover.SetActive(true);
        skipMoveButton.SetActive(false);
        characterEmote.EmoteSurprise();

        yield return new WaitForSeconds(1f);

        characterEmote.EmoteSad();

        yield return new WaitForSeconds(1.5f);

        colliderCover.SetActive(false);
        alertPanel.SetActive(false);
        powerLowText.SetActive(false);
        skipMoveButton.SetActive(true);
        characterEmote.EmoteIdle();
    }

    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }

    public void NoErosionAlert()
    {
        StartCoroutine(NoErosionAlertCR());
    }

    IEnumerator NoErosionAlertCR()
    {
        momentumLostText.SetActive(true);
        alertPanel.SetActive(true);
        colliderCover.SetActive(true);
        skipMoveButton.SetActive(false);
        characterEmote.EmoteSquint();

        yield return new WaitForSeconds(1.25f);

        characterEmote.EmoteConfused();

        yield return new WaitForSeconds(1.25f);

        colliderCover.SetActive(false);
        alertPanel.SetActive(false);
        momentumLostText.SetActive(false);
        skipMoveButton.SetActive(true);
        characterEmote.EmoteIdle();
    }

    IEnumerator EndGame()
    {
        skipMoveButton.SetActive(false);
        colliderCover.SetActive(true);
        CalculateSoilBonus();
        CalcFinalScores();
        if (!reachedEdgeBeforeTimerZero)
        {
            batteryDeadHeader.SetActive(true);
            characterEmote.EmoteAngry();
        }
        else if (egEndPointBonus == board.boardSize * 200)
        {
            niceWorkHeader.SetActive(true);
            characterEmote.EmoteHappy();
        }
        else if (egEndPointBonus == board.boardSize * 100)
        {
            prettyCloseHeader.SetActive(true);
            characterEmote.EmoteHappy();
        }
        else
        {
            exitMissedHeader.SetActive(true);
            characterEmote.EmoteConfused();
        }
        
        yield return new WaitForSeconds(3f);

        endGamePanel.SetActive(true);

        yield return new WaitForSeconds(4f);

        characterEmote.EmoteIdle();
        endGamePanel.SetActive(false);

        yield return new WaitForSeconds(.5f);
        InitiateRetryPanel();

    }

    private void CalculateSoilBonus()
    {
        allTilesWithEarthTag = GameObject.FindGameObjectsWithTag("earthtile");

        foreach (GameObject tile in allTilesWithEarthTag)
        {
            if(tile.GetComponentInParent<Soil>() != null)
            {
                egSoilBonus += tile.GetComponentInParent<Soil>().CalcSoilBonus();
            }
        }
    }

    private void CalcFinalScores()
    {
        //handle end point bonus. if in the end zone, boardsize * 200 if not but on the correct edge, boardsize * 100
        if (gameEndingTilePos == board.endZone)
        {
            egEndPointBonus = board.boardSize * 200;
        }
        else if (board.endZone.x == 1 || board.endZone.x == board.boardSize)
        {
            if (gameEndingTilePos.x == board.endZone.x)
            {
                egEndPointBonus = board.boardSize * 100;
            }
        }
        else if (board.endZone.y == 1 || board.endZone.y == board.boardSize)
        {
            if (gameEndingTilePos.y == board.endZone.y)
            {
                egEndPointBonus = board.boardSize * 100;
            }
        }
        else
        {
            egEndPointBonus = 0;
        }

        if (turnTimer >= 1)
        {
            egBatteryBonus = turnTimer * 50;
        }

        //tally final score
        egFinalScore += score;
        egFinalScore += egBatteryBonus;
        egFinalScore += egEndPointBonus;
        egFinalScore += egSoilBonus;

        //set endgame text equal to ints
        endgameEndPointValue.text = egEndPointBonus.ToString();
        endgameSoilBonusValue.text = egSoilBonus.ToString();
        endgameBatteryBonusValue.text = egBatteryBonus.ToString();
        endgameCurrentScoreValue.text = score.ToString();
        endgameFinalScoreValue.text = egFinalScore.ToString();
    }

    private void InitiateRetryPanel()
    {
        characterEmote.EmoteQuestion();
        retryFinalScoreValue.text = egFinalScore.ToString();
        retryPanel.SetActive(true);
    }
}
