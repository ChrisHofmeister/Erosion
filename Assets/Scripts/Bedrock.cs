using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bedrock : MonoBehaviour
{

    private Board board;
    private RiverStart riverStart;
    private Water water;
    [SerializeField] GameObject waterPrefab;

    //game object that is being reviewed for reviewsurroundings()
    private GameObject targetObject;

    //bool will be set to true by check surroundings if surrounding tile has water
    private bool up = false;
    private bool down = false;
    private bool left = false;
    private bool right = false;

    public bool hasWater = false;

    private GameObject targetTile;

    //water script and GO for child water
    private Water childWater;

    //refrence to earthtile script attached to this game object to get position
    private EarthTile earthTile;

    // Start is called before the first frame update
    void Start()
    {
        //when the tile is created, runs set up
        
        board = FindObjectOfType<Board>();
        earthTile = GetComponent<EarthTile>();
        TileSetUp();
        childWater = GetComponentInChildren<Water>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (earthTile.column >= 1 && earthTile.column <= board.boardSize
            && earthTile.row >= 1 && earthTile.row <= board.boardSize)
        {
            CheckSurroundingsAndAct();
        }
    }

    private void TileSetUp()
    {
        if (board.gameStarted)
        {
            GameObject displayNumber =
            Instantiate(board.displayNumberPrefab, transform.position, Quaternion.identity);
            displayNumber.transform.parent = transform;

            GameObject displayGrid =
        Instantiate(board.gridPrefab, transform.position, Quaternion.identity);
            displayGrid.transform.parent = transform;
        }

        //add number, grid, tag, and column/row
        /*GameObject displayNumber =
            Instantiate(board.displayNumberPrefab, transform.position, Quaternion.identity);
        displayNumber.transform.parent = transform;*/
        if (earthTile.column >= 1 && earthTile.column <= board.boardSize
         && earthTile.row >= 1 && earthTile.row <= board.boardSize)
        {
            tag = (int)transform.position.x + "," + (int)transform.position.y;
        }
        
        gameObject.GetComponent<EarthTile>().column = gameObject.GetComponent<EarthTile>().targetX;
        gameObject.GetComponent<EarthTile>().row = gameObject.GetComponent<EarthTile>().targetY;

        /*GameObject displayGrid =
        Instantiate(board.gridPrefab, transform.position, Quaternion.identity);
        displayGrid.transform.parent = transform;*/
        board.allTilesArray[(int)transform.position.x, (int)transform.position.y] = this.gameObject;
        CreateWater();
        childWater = GetComponentInChildren<Water>();
        childWater.WaterOff();

    }

    public void CreateWater()
    {
        if (!hasWater)
        {
            hasWater = true;
            GameObject water = Instantiate(waterPrefab, transform.position, Quaternion.identity);
            water.transform.parent = transform;
            water.tag = "water";
        }
    }

    //checking surroundings for River
    public void CheckSurroundingsAndAct()
    {
        //if bedrock is next to river start, only edge space where water can be and not end the game, create river on self
        if(transform.position == board.safeZone)
        {
            if (childWater.waterOn == false)
            {
                childWater.WaterOn();
            }
        }

        up = CheckUp();
        down = CheckDown();        
        right = CheckRight();
        left = CheckLeft();
        
        //if any directions return true, have water, and has water bool is false, create river
        if (up || down || right || left)
        {
            if (childWater.waterOn == false)
            {
                childWater.WaterOn();
            }
        }                 
    }

    //check down and return true if river is child up
    private bool CheckUp()
    {
        int objectX = (int)transform.position.x;
        int objectY = (int)transform.position.y + 1;

        /*var objectTagNameX = objectX.ToString();
        var objectTagNameY = objectY.ToString();*/

        if (objectX >= 0 && objectX <= board.boardSize + 1 && objectY >= 0 && objectY <= board.boardSize + 1)
        {
            targetObject = board.allTilesArray[objectX, objectY];
        }


        if (targetObject != null && targetObject.GetComponentInChildren<Water>() != null
            && targetObject.GetComponentInChildren<Water>().waterOn ==true)
        {
            return true;
        }
        else { return false; }
    }
    //check down and return true if river is child down
    private bool CheckDown()
    {
        int objectX = (int)transform.position.x;
        int objectY = (int)transform.position.y - 1;

        /*var objectTagNameX = objectX.ToString();
        var objectTagNameY = objectY.ToString();*/

        if (objectX >= 0 && objectX <= board.boardSize + 1 && objectY >= 0 && objectY <= board.boardSize + 1)
        {
            targetObject = board.allTilesArray[objectX, objectY];
        }


        if (targetObject != null && targetObject.GetComponentInChildren<Water>() != null
            && targetObject.GetComponentInChildren<Water>().waterOn == true)
        {
            return true;
        }
        else { return false; }
    }
    //check down and return true if river is child right
    private bool CheckRight()
    {
        int objectX = (int)transform.position.x + 1;
        int objectY = (int)transform.position.y;

        /*var objectTagNameX = objectX.ToString();
        var objectTagNameY = objectY.ToString();*/

        if (objectX >= 0 && objectX <= board.boardSize + 1 && objectY >= 0 && objectY <= board.boardSize + 1)
        {
            targetObject = board.allTilesArray[objectX, objectY];
        }


        if (targetObject != null && targetObject.GetComponentInChildren<Water>() != null
            && targetObject.GetComponentInChildren<Water>().waterOn == true)
        {
            return true;
        }
        else { return false; }
    }
    //check down and return true if river is child left
    private bool CheckLeft()
    {
        int objectX = (int)transform.position.x - 1;
        int objectY = (int)transform.position.y;

        /*var objectTagNameX = objectX.ToString();
        var objectTagNameY = objectY.ToString();*/

        if (objectX >= 0 && objectX <= board.boardSize + 1 && objectY >= 0 && objectY <= board.boardSize + 1)
        {
            targetObject = board.allTilesArray[objectX, objectY];
        }


        if (targetObject != null && targetObject.GetComponentInChildren<Water>() != null
            && targetObject.GetComponentInChildren<Water>().waterOn == true)
        {
            return true;
        }
        else { return false; }
    }



}
