using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Board : MonoBehaviour
{
    /* 
    this script holds all info needed to create the board and populate the earth tiles, erosion point, start/end of river.
    the playtest enabled board
         
         
    */

    //creating variables for board size
    private int width;
    private int height;

    // designating tile prefab
    public GameObject tilePrefab;

    // allowing for adjustment of size, will design for 4x4 5x5 and 6x6
    public int boardSize;

    //creating empty array for backgroundtile objects
    private BackgroundTile[,] allTiles;

    //variables for earth tiles
    public GameObject[] earthTiles;
    private GameObject[] tileArrayToUse;
    public GameObject[,] allEarthTiles;
    public GameObject[,] allTilesArray;
    int tileToUse = 0;

    //gameobject needed to remove tag from background tiles when eath tile is placed
    private GameObject targetObject;

    //need specific arrays depending on the size of the board

    // soil - 5, sand - 1, lime - 1, granite - 1, quartz - 1
    public GameObject[] threeBoardTiles;

    // soil - 6, sand - 4, lime - 3, granite - 2, quartz - 1
    public GameObject[] fourBoardTiles;

    // soil - 8, sand - 7, lime - 5, granite - 3, quartz - 2
    public GameObject[] fiveBoardTiles;

    // soil - 12, sand - 10, lime - 7, granite - 4, quartz - 3
    public GameObject[] sixBoardTiles;

    // soil - 17, sand - 13, lime - 10, granite - 5, quartz - 4
    public GameObject[] sevenBoardTiles;



    //enable play test mode to set specific tile arrangemnts


    public bool playtestMode = new bool();
    private List<int> playtestIndex;
    private GameObject[] playtestTiles;

    //tile shredder to remove extra tiles that get  created once they leave the bounds of the board
    [SerializeField] GameObject tileShredder;

    public GameObject rightTileShredder;
    public GameObject leftTileShredder;
    public GameObject upTileShredder;
    public GameObject downTileShredder;

    //bools to indicatre swipe direction

    public bool rightSwipe = false;
    public bool leftSwipe = false;
    public bool upSwipe = false;
    public bool downSwipe = false;

    //array for the display numbers

    public GameObject displayNumberPrefab;

    //river starting/end and erosion point variables

    public GameObject riverStartPrefab;
    public GameObject riverEndPrefab;

    public int riverStartX;
    public int riverStartY;
    private Vector2 riverStartPoint;

    public int riverEndX;
    public int riverEndY;
    private Vector2 riverEndPoint;

    public GameObject erosionPointPrefab;
    public Vector3 startingRotationZ;
    public Vector3 endZone;
    public Vector3 safeZone;

    //bool to indicate game started to ensure tiles do not instantiate in their setup state
    public bool gameStarted;

    //grid object
    [SerializeField] public GameObject gridPrefab;

    //border object
    [SerializeField] private GameObject borderPrefab;

    //ints used for randomizing starting and ending points
    int startingEdge;
    int startingPoint;
    int endingEdge;
    int endingPoint;

    

    // Start is called before the first frame update
    void Start()
    {
        //set started bool to false to allow game set up
        gameStarted = false;
        // setting board size equal to values from inspector, plus 2 is to account for the border
        width = boardSize + 1;
        height = boardSize + 1;

        //setting the size of the backgroundtile array based on 2 plus boardsize to account for border

        allTiles = new BackgroundTile[width, height];

        //setting size of allEarthTiles and allTilesArray array based on boardsize

        allEarthTiles = new GameObject[width, height];
        allTilesArray = new GameObject[width + 1, height + 1];




        //check for game mode sees if playtest mode and boardsize and adjusts the array to use

        //----------------------------------------------
        playtestMode = false;
        //----------------------------------------------

        CheckForGameMode();




        //runds set up method to create board
        SetUp();


    }

    //using 2 for loops, instantiating board. adding each column=j before itterating to next row = i
    private void SetUp()
    {
        //if we are not in playtest mode the array is shuffled.  this keeps the distro of the tiles
        //the same but changes their order

        if (!playtestMode)
        {


            for (int e = 0; e < tileArrayToUse.Length; e++)
            {
                GameObject tmp = tileArrayToUse[e];
                int r = Random.Range(e, tileArrayToUse.Length);
                tileArrayToUse[e] = tileArrayToUse[r];
                tileArrayToUse[r] = tmp;
            }
        }

        for (int i = 0; i <= width; i++)
        {
            for (int j = 0; j <= height; j++)
            {
                Vector2 tempPosition = new Vector2(i, j);
                GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) as GameObject;
                backgroundTile.transform.parent = this.transform;
                //setting tags for border tiles so will avoid null exceptions when tiles check surroundings
                //no tile can share a tag since its usesd for positioning.
                backgroundTile.tag = i + "," + j;
                allTilesArray[i, j] = backgroundTile;

                if (i == 0 || i == width)
                {

                    GameObject displayBorder =
                                Instantiate(borderPrefab, tempPosition, Quaternion.identity);
                    displayBorder.transform.parent = backgroundTile.transform;

                }

                if ((j == 0 && i > 0 && i < width) || (j == height && i > 0 && i < width))
                {
                    GameObject displayBorder =
                                Instantiate(borderPrefab, tempPosition, Quaternion.identity);
                    displayBorder.transform.parent = backgroundTile.transform;
                }

                //leaving a border around the playspace == one tile 
                if (i > 0 && i < width)
                {
                    if (j > 0 && j < height)
                    {

                        //Assigning earth tile
                        if (tileToUse <= tileArrayToUse.Length)
                        {
                            //need to remove tag from background tiles that will have eath tiles placed on them 
                            //so the erosion mechanics will work
                            var objectTagNameX = i.ToString();
                            var objectTagNameY = j.ToString();

                            targetObject = GameObject.FindGameObjectWithTag(objectTagNameX + "," + objectTagNameY);

                            targetObject.tag = "Untagged";

                            GameObject earthTile =
                                Instantiate(tileArrayToUse[tileToUse], tempPosition, Quaternion.identity) as GameObject;
                            earthTile.transform.parent = this.transform;
                            allEarthTiles[i, j] = earthTile;
                            earthTile.tag = i + "," + j;
                            allTilesArray[i, j] = earthTile;

                            //Assign starting displayNumber
                            int numberToUse = FindObjectOfType<EarthTile>().GetResistance();
                            GameObject displayNumber =
                                Instantiate(displayNumberPrefab, tempPosition, Quaternion.identity);
                            displayNumber.transform.parent = earthTile.transform;
                            displayNumber.GetComponent<Number>().displayNumberIndex
                                = earthTile.GetComponent<EarthTile>().resistance;

                            //instance grid game object and attach to tile
                            GameObject displayGrid =
                                    Instantiate(gridPrefab, tempPosition, Quaternion.identity);
                            displayGrid.transform.parent = earthTile.transform;


                            //increment to next tile in array
                            tileToUse++;



                        }
                    }
                }

            }
        }
        //still within set up, instance tile shredders
        rightTileShredder = Instantiate(tileShredder, new Vector3(boardSize + 1.4f, 0, 0), Quaternion.identity);
        leftTileShredder = Instantiate(tileShredder, new Vector3(-0.4f, 0, 0), Quaternion.identity);
        upTileShredder = Instantiate(tileShredder, new Vector3(0, boardSize + 1.4f, 0), Quaternion.Euler(new Vector3(0, 0, 90)));
        downTileShredder = Instantiate(tileShredder, new Vector3(0, -0.4f, 0), Quaternion.Euler(new Vector3(0, 0, 90)));

        rightTileShredder.SetActive(false);
        leftTileShredder.SetActive(false);
        upTileShredder.SetActive(false);
        downTileShredder.SetActive(false);

        if (playtestMode == false)
        {
            //still within set up, run methods to place start, end, and erosion points
            RandomizeRiverStartAndEndPoints();
            //creating the vector 2 used for river start and end based on variables from randomizer or set manually
        }
            riverStartPoint = new Vector2(riverStartX, riverStartY);
            riverEndPoint = new Vector2(riverEndX, riverEndY);
        
            SetRiverStartAndErosionPoint(boardSize);
            SetRiverEndPoint(boardSize);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CheckForGameMode()
    {

        if (playtestMode)
        {
            //must ensure boardsize is correct for playtest
            //ensure the playtest tiles array has the correct length for the playtest
            playtestIndex = new List<int>();
            playtestTiles = new GameObject[9];

            /*playtesting set up will be a manual process, setting the tiles and board size below. 
            adding to playtest index the tiles you want to see based on their positions in earthtTiles[]
            0=bedrock
            1=soil
            2=sandstone
            3=limestone
            4=granite
            5=quartz

            */

            // left to right is bottom to top starting and first row is first column
            //as ifyou took the array and tilted it 90 deg counter clockwise

            playtestIndex.Add(2); playtestIndex.Add(1); playtestIndex.Add(2); 
            playtestIndex.Add(1); playtestIndex.Add(1); playtestIndex.Add(1); 
            playtestIndex.Add(2); playtestIndex.Add(2); playtestIndex.Add(2); 

            int i = 0;
            foreach (int index in playtestIndex)
            {
                playtestTiles[i] = earthTiles[index];
                i++;
            }

            // sets array to use as the playtest tile array
            tileArrayToUse = playtestTiles;

            //when in playtest mode, river start and end points can be set here

            riverStartX = 2;
            riverStartY = 4;

            riverEndX = 2;
            riverEndY = 0;

            /*riverStartX = Mathf.RoundToInt((boardSize / 2) + 1);
            riverStartY = boardSize + 1;

            riverEndX = boardSize + 1;
            riverEndY = Mathf.RoundToInt((boardSize / 2) + 1);*/

        }

        else
        {
            //sets tileArraytouse as the specific array depending on the baord size
            if (boardSize == 3)
            {
                tileArrayToUse = threeBoardTiles;
            }

            else if (boardSize == 4)
            {
                tileArrayToUse = fourBoardTiles;
            }

            else if (boardSize == 5)
            {
                tileArrayToUse = fiveBoardTiles;
            }

            else if (boardSize == 6)
            {
                tileArrayToUse = sixBoardTiles;
            }

            else if (boardSize == 7)
            {
                tileArrayToUse = sevenBoardTiles;
            }

        }
    }

    //
    private void SetRiverStartAndErosionPoint(int boardSize)
    {


        //if starting at top
        if (riverStartPoint.y == boardSize + 1)
        {
            startingRotationZ = new Vector3(0, 0, 0);
            GameObject riverStart = Instantiate(riverStartPrefab, riverStartPoint, Quaternion.identity);
            GameObject erosionPoint = Instantiate(erosionPointPrefab, riverStartPoint, Quaternion.Euler(startingRotationZ)) as GameObject;
            safeZone = new Vector3(riverStartPoint.x, riverStartPoint.y - 1, 0);

        }
        //is starting at bottom
        if (riverStartPoint.y == 0)
        {
            startingRotationZ = new Vector3(0, 0, 180);
            GameObject riverStart = Instantiate(riverStartPrefab, riverStartPoint, Quaternion.identity);
            GameObject erosionPoint = Instantiate(erosionPointPrefab, riverStartPoint, Quaternion.Euler(startingRotationZ)) as GameObject;
            safeZone = new Vector3(riverStartPoint.x, riverStartPoint.y + 1, 0);
        }
        //if starting on right side
        if (riverStartPoint.x == boardSize + 1)
        {
            startingRotationZ = new Vector3(0, 0, 270);
            GameObject riverStart = Instantiate(riverStartPrefab, riverStartPoint, Quaternion.Euler(0, 0, 0));
            GameObject erosionPoint = Instantiate(erosionPointPrefab, riverStartPoint, Quaternion.Euler(startingRotationZ)) as GameObject;
            safeZone = new Vector3(riverStartPoint.x - 1, riverStartPoint.y, 0);
        }
        //if starting on left side
        if (riverStartPoint.x == 0)
        {
            startingRotationZ = new Vector3(0, 0, 90);
            GameObject riverStart = Instantiate(riverStartPrefab, riverStartPoint, Quaternion.Euler(0, 0, 0));
            GameObject erosionPoint = Instantiate(erosionPointPrefab, riverStartPoint, Quaternion.Euler(startingRotationZ)) as GameObject;
            safeZone = new Vector3(riverStartPoint.x + 1, riverStartPoint.y, 0);
        }
    }

    //creating the endpoint end point
    private void SetRiverEndPoint(int boardSize)
    {


        //if starting at top
        if (riverEndPoint.y == boardSize + 1)
        {

            GameObject riverEnd = Instantiate(riverEndPrefab, riverEndPoint, Quaternion.identity);

            endZone = new Vector3(riverEndPoint.x, riverEndPoint.y - 1, 0);

        }
        //is starting at bottom
        if (riverEndPoint.y == 0)
        {

            GameObject riverEnd = Instantiate(riverEndPrefab, riverEndPoint, Quaternion.identity);

            endZone = new Vector3(riverEndPoint.x, riverEndPoint.y + 1, 0);
        }
        //if starting on right side
        if (riverEndPoint.x == boardSize + 1)
        {

            GameObject riverEnd = Instantiate(riverEndPrefab, riverEndPoint, Quaternion.identity);

            endZone = new Vector3(riverEndPoint.x - 1, riverEndPoint.y, 0);
        }
        //if starting on left side
        if (riverEndPoint.x == 0)
        {

            GameObject riverEnd = Instantiate(riverEndPrefab, riverEndPoint, Quaternion.identity);

            endZone = new Vector3(riverEndPoint.x + 1, riverEndPoint.y, 0);
        }
    }

    private void RandomizeRiverStartAndEndPoints()
    {
        startingPoint = Random.Range(2, boardSize);
        endingPoint = Random.Range(2, boardSize);
        startingEdge = Random.Range(1, 5);
        //startingEdge = 4;

        if (boardSize == 3)
        {
            endingEdge = Random.Range(1, 4);

            if (startingEdge == 1)
            {
                riverStartX = startingPoint;
                riverStartY = boardSize + 1;

                if (endingEdge == 1)
                {
                    riverEndX = endingPoint;
                    riverEndY = 0;
                }
                if (endingEdge == 2)
                {
                    riverEndX = boardSize + 1;
                    riverEndY = endingPoint;
                }
                if (endingEdge == 3)
                {
                    riverEndX = 0;
                    riverEndY = endingPoint;
                }
            }
            if (startingEdge == 2)
            {
                riverStartX = startingPoint;
                riverStartY = 0;

                if (endingEdge == 1)
                {
                    riverEndX = endingPoint;
                    riverEndY = boardSize + 1;
                }
                if (endingEdge == 2)
                {
                    riverEndX = boardSize + 1;
                    riverEndY = endingPoint;
                }
                if (endingEdge == 3)
                {
                    riverEndX = 0;
                    riverEndY = endingPoint;
                }
            }
            if (startingEdge == 3)
            {
                riverStartX = boardSize + 1;
                riverStartY = startingPoint;

                if (endingEdge == 1)
                {
                    riverEndX = endingPoint;
                    riverEndY = boardSize + 1;
                }
                if (endingEdge == 2)
                {
                    riverEndX = endingPoint;
                    riverEndY = 0;
                }
                if (endingEdge == 3)
                {
                    riverEndX = 0;
                    riverEndY = endingPoint;
                }
            }
            if (startingEdge == 4)
            {
                riverStartX = 0;
                riverStartY = startingPoint;

                if (endingEdge == 1)
                {
                    riverEndX = endingPoint;
                    riverEndY = boardSize + 1;
                }
                if (endingEdge == 2)
                {
                    riverEndX = endingPoint;
                    riverEndY = 0;
                }
                if (endingEdge == 3)
                {
                    riverEndX = boardSize + 1;
                    riverEndY = endingPoint;
                }
            }
        }
        if (boardSize == 4)
        {
            endingEdge = Random.Range(1, 3);

            if (startingPoint == 2)
            {
                if (startingEdge == 1)
                {
                    riverStartX = startingPoint;
                    riverStartY = boardSize + 1;

                    if (endingEdge == 1)
                    {
                        riverEndX = endingPoint;
                        riverEndY = 0;
                    }
                    if (endingEdge == 2)
                    {
                        riverEndX = boardSize + 1;
                        riverEndY = endingPoint;
                    }
                }
                if (startingEdge == 2)
                {
                    riverStartX = startingPoint;
                    riverStartY = 0;

                    if (endingEdge == 1)
                    {
                        riverEndX = endingPoint;
                        riverEndY = boardSize + 1;
                    }
                    if (endingEdge == 2)
                    {
                        riverEndX = boardSize + 1;
                        riverEndY = endingPoint;
                    }
                }
                if (startingEdge == 3)
                {
                    riverStartX = boardSize + 1;
                    riverStartY = startingPoint;

                    if (endingEdge == 1)
                    {
                        riverEndX = endingPoint;
                        riverEndY = boardSize + 1;
                    }
                    if (endingEdge == 2)
                    {
                        riverEndX = endingPoint;
                        riverEndY = 0;
                    }
                }
                if (startingEdge == 4)
                {
                    riverStartX = 0;
                    riverStartY = startingPoint;

                    if (endingEdge == 1)
                    {
                        riverEndX = endingPoint;
                        riverEndY = boardSize + 1;
                    }
                    if (endingEdge == 2)
                    {
                        riverEndX = endingPoint;
                        riverEndY = boardSize + 1;
                    }
                }
            }
            if (startingPoint == 3)
            {
                if (startingEdge == 1)
                {
                    riverStartX = startingPoint;
                    riverStartY = boardSize + 1;

                    if (endingEdge == 1)
                    {
                        riverEndX = endingPoint;
                        riverEndY = 0;
                    }
                    if (endingEdge == 2)
                    {
                        riverEndX = 0;
                        riverEndY = endingPoint;
                    }
                }
                if (startingEdge == 2)
                {
                    riverStartX = startingPoint;
                    riverStartY = 0;

                    if (endingEdge == 1)
                    {
                        riverEndX = endingPoint;
                        riverEndY = boardSize + 1;
                    }
                    if (endingEdge == 2)
                    {
                        riverEndX = 0;
                        riverEndY = endingPoint;
                    }
                }
                if (startingEdge == 3)
                {
                    riverStartX = boardSize + 1;
                    riverStartY = startingPoint;

                    if (endingEdge == 1)
                    {
                        riverEndX = endingPoint;
                        riverEndY = 0;
                    }
                    if (endingEdge == 2)
                    {
                        riverEndX = endingPoint;
                        riverEndY = 0;
                    }
                }
                if (startingEdge == 4)
                {
                    riverStartX = 0;
                    riverStartY = startingPoint;

                    if (endingEdge == 1)
                    {
                        riverEndX = endingPoint;
                        riverEndY = 0;
                    }
                    if (endingEdge == 2)
                    {
                        riverEndX = endingPoint;
                        riverEndY = boardSize + 1;
                    }
                }
            }
        }
        if (boardSize == 5)
        {
            if (startingPoint == 2)
            {
                endingEdge = Random.Range(1, 3);

                if (startingEdge == 1)
                {
                    riverStartX = startingPoint;
                    riverStartY = boardSize + 1;

                    if (endingEdge == 1)
                    {
                        riverEndX = endingPoint;
                        riverEndY = 0;
                    }
                    if (endingEdge == 2)
                    {
                        riverEndX = boardSize + 1;
                        riverEndY = endingPoint;
                    }
                }
                if (startingEdge == 2)
                {
                    riverStartX = startingPoint;
                    riverStartY = 0;

                    if (endingEdge == 1)
                    {
                        riverEndX = endingPoint;
                        riverEndY = boardSize + 1;
                    }
                    if (endingEdge == 2)
                    {
                        riverEndX = boardSize + 1;
                        riverEndY = endingPoint;
                    }
                }
                if (startingEdge == 3)
                {
                    riverStartX = boardSize + 1;
                    riverStartY = startingPoint;

                    if (endingEdge == 1)
                    {
                        riverEndX = endingPoint;
                        riverEndY = boardSize + 1;
                    }
                    if (endingEdge == 2)
                    {
                        riverEndX = 0;
                        riverEndY = endingPoint;
                    }
                }
                if (startingEdge == 4)
                {
                    riverStartX = 0;
                    riverStartY = startingPoint;

                    if (endingEdge == 1)
                    {
                        riverEndX = endingPoint;
                        riverEndY = boardSize + 1;
                    }
                    if (endingEdge == 2)
                    {
                        riverEndX = boardSize + 1;
                        riverEndY = endingPoint;
                    }
                }
            }
            if (startingPoint == 3)
            {
                endingEdge = Random.Range(1, 4);
                if (startingEdge == 1)
                {
                    riverStartX = startingPoint;
                    riverStartY = boardSize + 1;

                    if (endingEdge == 1)
                    {
                        riverEndX = endingPoint;
                        riverEndY = 0;
                    }
                    if (endingEdge == 2)
                    {
                        riverEndX = boardSize + 1;
                        riverEndY = endingPoint;
                    }
                    if (endingEdge == 3)
                    {
                        riverEndX = 0;
                        riverEndY = endingPoint;
                    }
                }
                if (startingEdge == 2)
                {
                    riverStartX = startingPoint;
                    riverStartY = 0;

                    if (endingEdge == 1)
                    {
                        riverEndX = endingPoint;
                        riverEndY = boardSize + 1;
                    }
                    if (endingEdge == 2)
                    {
                        riverEndX = boardSize + 1;
                        riverEndY = endingPoint;
                    }
                    if (endingEdge == 3)
                    {
                        riverEndX = 0;
                        riverEndY = endingPoint;
                    }
                }
                if (startingEdge == 3)
                {
                    riverStartX = boardSize + 1;
                    riverStartY = startingPoint;

                    if (endingEdge == 1)
                    {
                        riverEndX = endingPoint;
                        riverEndY = boardSize + 1;
                    }
                    if (endingEdge == 2)
                    {
                        riverEndX = endingPoint;
                        riverEndY = 0;
                    }
                    if (endingEdge == 3)
                    {
                        riverEndX = 0;
                        riverEndY = endingPoint;
                    }
                }
                if (startingEdge == 4)
                {
                    riverStartX = 0;
                    riverStartY = startingPoint;

                    if (endingEdge == 1)
                    {
                        riverEndX = endingPoint;
                        riverEndY = boardSize + 1;
                    }
                    if (endingEdge == 2)
                    {
                        riverEndX = endingPoint;
                        riverEndY = 0;
                    }
                    if (endingEdge == 3)
                    {
                        riverEndX = boardSize + 1;
                        riverEndY = endingPoint;
                    }
                }
            }
            if (startingPoint == 4)
            {
                endingEdge = Random.Range(1, 3);

                if (startingEdge == 1)
                {
                    riverStartX = startingPoint;
                    riverStartY = boardSize + 1;

                    if (endingEdge == 1)
                    {
                        riverEndX = endingPoint;
                        riverEndY = 0;
                    }
                    if (endingEdge == 2)
                    {
                        riverEndX = 0;
                        riverEndY = endingPoint;
                    }
                }
                if (startingEdge == 2)
                {
                    riverStartX = startingPoint;
                    riverStartY = 0;

                    if (endingEdge == 1)
                    {
                        riverEndX = endingPoint;
                        riverEndY = boardSize + 1;
                    }
                    if (endingEdge == 2)
                    {
                        riverEndX = 0;
                        riverEndY = endingPoint;
                    }
                }
                if (startingEdge == 3)
                {
                    riverStartX = boardSize + 1;
                    riverStartY = startingPoint;

                    if (endingEdge == 1)
                    {
                        riverEndX = endingPoint;
                        riverEndY = 0;
                    }
                    if (endingEdge == 2)
                    {
                        riverEndX = 0;
                        riverEndY = endingPoint;
                    }
                }
                if (startingEdge == 4)
                {
                    riverStartX = 0;
                    riverStartY = startingPoint;

                    if (endingEdge == 1)
                    {
                        riverEndX = endingPoint;
                        riverEndY = 0;
                    }
                    if (endingEdge == 2)
                    {
                        riverEndX = boardSize + 1;
                        riverEndY = endingPoint;
                    }
                }
            }
        }   
        if (boardSize == 6)
        {
            if (startingPoint == 2 || startingPoint == 3)
            {
                endingEdge = Random.Range(1, 3);

                if (startingEdge == 1)
                {
                    riverStartX = startingPoint;
                    riverStartY = boardSize + 1;

                    if (endingEdge == 1)
                    {
                        riverEndX = endingPoint;
                        riverEndY = 0;
                    }
                    if (endingEdge == 2)
                    {
                        riverEndX = boardSize + 1;
                        riverEndY = endingPoint;
                    }
                }
                if (startingEdge == 2)
                {
                    riverStartX = startingPoint;
                    riverStartY = 0;

                    if (endingEdge == 1)
                    {
                        riverEndX = endingPoint;
                        riverEndY = boardSize + 1;
                    }
                    if (endingEdge == 2)
                    {
                        riverEndX = boardSize + 1;
                        riverEndY = endingPoint;
                    }
                }
                if (startingEdge == 3)
                {
                    riverStartX = boardSize + 1;
                    riverStartY = startingPoint;

                    if (endingEdge == 1)
                    {
                        riverEndX = endingPoint;
                        riverEndY = boardSize + 1;
                    }
                    if (endingEdge == 2)
                    {
                        riverEndX = 0;
                        riverEndY = endingPoint;
                    }
                }
                if (startingEdge == 4)
                {
                    riverStartX = 0;
                    riverStartY = startingPoint;

                    if (endingEdge == 1)
                    {
                        riverEndX = endingPoint;
                        riverEndY = boardSize + 1;
                    }
                    if (endingEdge == 2)
                    {
                        riverEndX = boardSize + 1;
                        riverEndY = endingPoint;
                    }
                }
            }
            if (startingPoint == 4 || startingPoint == 5)
            {
                endingEdge = Random.Range(1, 3);

                if (startingEdge == 1)
                {
                    riverStartX = startingPoint;
                    riverStartY = boardSize + 1;

                    if (endingEdge == 1)
                    {
                        riverEndX = endingPoint;
                        riverEndY = 0;
                    }
                    if (endingEdge == 2)
                    {
                        riverEndX = 0;
                        riverEndY = endingPoint;
                    }
                }
                if (startingEdge == 2)
                {
                    riverStartX = startingPoint;
                    riverStartY = 0;

                    if (endingEdge == 1)
                    {
                        riverEndX = endingPoint;
                        riverEndY = boardSize + 1;
                    }
                    if (endingEdge == 2)
                    {
                        riverEndX = 0;
                        riverEndY = endingPoint;
                    }
                }
                if (startingEdge == 3)
                {
                    riverStartX = boardSize + 1;
                    riverStartY = startingPoint;

                    if (endingEdge == 1)
                    {
                        riverEndX = endingPoint;
                        riverEndY = 0;
                    }
                    if (endingEdge == 2)
                    {
                        riverEndX = 0;
                        riverEndY = endingPoint;
                    }
                }
                if (startingEdge == 4)
                {
                    riverStartX = 0;
                    riverStartY = startingPoint;

                    if (endingEdge == 1)
                    {
                        riverEndX = endingPoint;
                        riverEndY = 0;
                    }
                    if (endingEdge == 2)
                    {
                        riverEndX = boardSize + 1;
                        riverEndY = endingPoint;
                    }
                }
            }

        }        
    }
}