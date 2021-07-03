using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthTile : MonoBehaviour
{
    /*  
     this script holds info on the earth tiles
     movement based on swipes
     order of actions for earth tiles and erosion point before and after movement

    */

    //resistance of tile (how long to fully erode)

    public int resistance;
    public int startingResistance;


    //variables for swipe movement

    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    private Vector2 tempPosition;
    private float swipeAngle = 0f;
    private float clickDeadZone = .25f;
    [SerializeField] public int column;
    [SerializeField] public int row;
    [SerializeField] public int targetX;
    [SerializeField] public int targetY;

    //lerp speed adjusted for various comp speeds.  5 seems right
    private float lerpSpeed = 5f;



    //board class so earthtiles can know the board size

    private Board board;

    //other tiles  to be moved

    private GameObject tileToMove;
    private GameObject tileCopy;

    //erosion point class so can tell ep to act after movement happens
    private ErosionPoint erosionPoint;



    //Game Manager game object, will dictate when actions take place
    private GameManager gameManager;

    //int for how many points are scored when tile is eroded
    [SerializeField] int points;

    // Start is called before the first frame update
    void Start()
    {
        //setting instance of board, erosion point, and gameManager
        board = FindObjectOfType<Board>();
        erosionPoint = FindObjectOfType<ErosionPoint>();
        gameManager = FindObjectOfType<GameManager>();
        startingResistance = resistance;

        //setting column and row variables  = to current position. targ x&y will be used for movement
        //unless a move has been made, the tile copies created will have this info set farther in script

        if (!board.gameStarted)
        {
            targetX = (int)transform.position.x;
            targetY = (int)transform.position.y;

            column = targetX;
            row = targetY;
        }        
    }

    // Update is called once per frame
    void Update()
    {
        //moveing every tile to new position on the update
        HandleMove();
        //turinging tiles shredders on and off based on swipe direction
        destroyExtraTile(board.rightSwipe, board.leftSwipe, board.upSwipe, board.downSwipe);
        CheckForFullErosion();
        




    }

    //moves the actual gamesobjects to their new positions

    private void HandleMove()
    {
        targetX = column;
        targetY = row;


        if (Mathf.Abs(targetX - transform.position.x) > .05)
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, lerpSpeed * Time.deltaTime);
            
        }
        else
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
            if (column > 0 && column <= board.boardSize && row > 0 && row <= board.boardSize)
            {
                board.allEarthTiles[column, row] = this.gameObject;
            }
        }
        


        if (Mathf.Abs(targetY - transform.position.y) > .05)
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, lerpSpeed * Time.deltaTime);
        }
        else
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
            if (column > 0 && column <= board.boardSize && row > 0 && row <= board.boardSize)
            {
                board.allEarthTiles[column, row] = this.gameObject;
            }
        }          
              
    }
    //turns on tile shredder in the swipe direction and turns the others off

    private void destroyExtraTile(bool rightSwipe, bool leftSwipe, bool upSwipe, bool downSwipe)
    {
        rightSwipe = board.rightSwipe;
        leftSwipe = board.leftSwipe;
        upSwipe = board.upSwipe;
        downSwipe = board.downSwipe;

        //right
        if (rightSwipe && !leftSwipe && !upSwipe && !downSwipe)
        {

            board.rightTileShredder.SetActive(true);
            board.leftTileShredder.SetActive(false);
            board.upTileShredder.SetActive(false);
            board.downTileShredder.SetActive(false);
                
        }
        //left
        if (!rightSwipe && leftSwipe && !upSwipe && !downSwipe)
        {

            board.rightTileShredder.SetActive(false);
            board.leftTileShredder.SetActive(true);
            board.upTileShredder.SetActive(false);
            board.downTileShredder.SetActive(false);

        }
        //up
        if (!rightSwipe && !leftSwipe && upSwipe && !downSwipe)
        {

            board.rightTileShredder.SetActive(false);
            board.leftTileShredder.SetActive(false);
            board.upTileShredder.SetActive(true);
            board.downTileShredder.SetActive(false);

        }
        //down
        if (!rightSwipe && !leftSwipe && !upSwipe && downSwipe)
        {

            board.rightTileShredder.SetActive(false);
            board.leftTileShredder.SetActive(false);
            board.upTileShredder.SetActive(false);
            board.downTileShredder.SetActive(true);

        }

    }

    //mouse down is the click, mouse up is when mouse button is released after swiping in another direction

    private void OnMouseDown()
    {
        //once you make your first move, sets game started bool to true
        board.gameStarted = true;
        firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //to allow a click without and release without moving, say the player makes an error... 
        //the mouse must have traveled farther than the clickDeadZone to trigger

        if (Mathf.Abs(firstTouchPosition.x - finalTouchPosition.x) > clickDeadZone || Mathf.Abs(firstTouchPosition.y - finalTouchPosition.y) > clickDeadZone)
        {
            CalculateAngle();


            MoveTiles();
            // ****************erosionPoint.multiPath = false;            
            gameManager.Erosion();
        }

    }

    void CalculateAngle()
    {
        swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y,
            finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
    }

    //changes tile's position in the allEarthTiles array
    void MoveTiles()
    {
        if (swipeAngle > -45 && swipeAngle <= 45)
        {
            board.rightSwipe = true;
            board.leftSwipe = false;
            board.upSwipe = false;
            board.downSwipe = false;
            //Right Swipe- loop through and move each tile in the row over by one column            
            


            for (int t = 1; t <= board.boardSize; t++)
            {

                tileToMove = board.allEarthTiles[t, row];
                tileToMove.GetComponent<EarthTile>().column = t + 1;
                //copy tile at the end of row and place at the beginning bfore being moved
                //end tile then gets shredded after move complete
                if (tileToMove.GetComponent<EarthTile>().column > board.boardSize)
                {
                    tileCopy = Instantiate(tileToMove, new Vector3(0, row, 0), Quaternion.identity) as GameObject;
                    tileCopy.transform.parent = board.transform;
                    board.allEarthTiles[1, row] = tileCopy;
                    
                    tileCopy.GetComponent<EarthTile>().column = 1;
                    
                    tileCopy.tag = "1," + row;

                }
            }

        }



        else if (swipeAngle > 45 && swipeAngle <= 135)
        {
            //Up Swipe
            board.rightSwipe = false;
            board.leftSwipe = false;
            board.upSwipe = true;
            board.downSwipe = false;

            for (int t = 1; t <= board.boardSize; t++)
            {

                tileToMove = board.allEarthTiles[column, t];
                tileToMove.GetComponent<EarthTile>().row = t + 1;
                if (tileToMove.GetComponent<EarthTile>().row > board.boardSize)
                {
                    tileCopy = Instantiate(tileToMove, new Vector3(column, 0, 0), Quaternion.identity) as GameObject;
                    tileCopy.transform.parent = board.transform;
                    board.allEarthTiles[column, 1] = tileCopy;

                    tileCopy.GetComponent<EarthTile>().row = 1;
                    
                    tileCopy.tag = column + ",1";
                }
            }

        }
        else if (swipeAngle > 135 || swipeAngle <= -135)
        {
            //Left Swipe
            board.rightSwipe = false;
            board.leftSwipe = true;
            board.upSwipe = false;
            board.downSwipe = false;
            for (int t = board.boardSize; t >= 1; t--)
            {

                tileToMove = board.allEarthTiles[t, row];
                tileToMove.GetComponent<EarthTile>().column = t - 1;
                if (tileToMove.GetComponent<EarthTile>().column < 1)
                {
                    tileCopy = Instantiate(tileToMove, new Vector3(board.boardSize + 1, row, 0), Quaternion.identity) as GameObject;
                    tileCopy.transform.parent = board.transform;
                    board.allEarthTiles[board.boardSize, row] = tileCopy;

                    tileCopy.GetComponent<EarthTile>().column = board.boardSize;

                    tileCopy.tag = board.boardSize + "," + row;
                }

                tileToMove.tag = column
                    + "," + tileToMove.GetComponent<EarthTile>().row;

            }


        }
        else if (swipeAngle < -45 && swipeAngle >= -135)
        {
            //Down Swipe
            board.rightSwipe = false;
            board.leftSwipe = false;
            board.upSwipe = false;
            board.downSwipe = true;

            for (int t = board.boardSize; t >= 1; t--)
            {

                tileToMove = board.allEarthTiles[column, t];
                tileToMove.GetComponent<EarthTile>().row = t - 1;
                if (tileToMove.GetComponent<EarthTile>().row < 1)
                {
                    tileCopy = Instantiate(tileToMove, new Vector3(column, board.boardSize + 1, 0), Quaternion.identity) as GameObject;
                    tileCopy.transform.parent = board.transform;
                    board.allEarthTiles[column, board.boardSize] = tileCopy;

                    tileCopy.GetComponent<EarthTile>().row = board.boardSize;

                    tileCopy.tag = column + "," + board.boardSize;
                }

                tileToMove.tag = tileToMove.GetComponent<EarthTile>().column
                    + "," + row;

            }
        }
    }
    //called by shredders to destroy object if  triggered
    public void DestroySelf()
    {
 
            Debug.Log(gameObject.name + " is being destroyed");
            Destroy(gameObject);
        
    }
    //caled anytime the resistance of a tile is needed
    public int GetResistance()
    {
        return resistance;
    }

    //when called, will erode the resistance by one
    public void ErodeResistance()
    {

        if (resistance > 0)
        {
            resistance--;
        }
    }
    
    //check for full erosion
    private void CheckForFullErosion()
    {
        if (resistance <= 0)
        {
            if (!GetComponent<Bedrock>())
            {      
                //create a bedrock tile in its current location and destroy itself
                GameObject newBedrock = Instantiate(board.earthTiles[0], tempPosition, Quaternion.identity);
                newBedrock.GetComponent<EarthTile>().targetX = (int) tempPosition.x;
                newBedrock.GetComponent<EarthTile>().targetY = (int)tempPosition.y;
                newBedrock.transform.parent = board.transform;                
                DestroySelf();
                gameManager.UpdateScore(points);
            }
        }
    }

    public void UpdatePositionTag()
    {
        this.gameObject.tag = column + "," + row;
    }
}
