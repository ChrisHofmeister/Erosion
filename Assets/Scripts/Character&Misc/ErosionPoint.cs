using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErosionPoint : MonoBehaviour
{
    //sprites
    [SerializeField] private Sprite[] erosionPointSprites;
    private SpriteRenderer spriteRenderer;

    //instance of board class for getting boardSize
    private Board board;

    //vector3 for the starting point of the EP
    private Vector3 erosionPointStart;

    //bools for direction EP is facing
    bool upEPFacing = false;
    bool downEPFacing = false;
    bool rightEPFacing = false;
    bool leftEPFacing = false;

    //variables for finding erosion point targets
    
    private GameObject primaryErosionTarget;
    private GameObject potentialErosionTargetA;
    private GameObject potentialErosionTargetB;


    //variables for the resistances of potential targets and erosionTarget that will be eroded
    private GameObject erosionTarget;
    private Vector2 erosionTargetPos;

    private int primaryRes;
    private int potentialResA;
    private int potentialResB;

    //Number class to update displaynumber
    private Number targetDisplayNumber;

    //bool to indicate if erosion point needs to move to another tile and 4 bool for if should move
    public bool willMove;
    public bool moveUp;
    public bool moveDown;
    public bool moveRight;
    public bool moveLeft;

    //game object to all for movement checks
    private GameObject potentialEPMovementTarget;

    //variables to make EP able to move and rotate
    int moveXPos;
    int moveYPos;
    Vector2 movePos;
    public int column;
    public int row;
    private Vector3 rotateEP;
    private Vector3 newRotation;


    //tile where EP will move
    private GameObject targetBedrock;

    //count on how many movement options are available for the erosion point
    private int movementOptionsCount;

    //water script for target bedrock
    
    private Water childWater;

    //gameManager script
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {

        board = FindObjectOfType<Board>();
        erosionPointStart = new Vector3(board.riverStartX, board.riverStartY,0);       
        transform.position = erosionPointStart;
        column = (int)transform.position.x;
        row = (int)transform.position.y;
        gameManager = FindObjectOfType<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
               
    }

    // Update is called once per frame
    void Update()
    {
        OritentSprite(board.boardSize);
        


    }

    //changing sprite depending on position 
    private void OritentSprite(int boardSize)
    {
        if (transform.position.y == boardSize + 1)
        {
            GetComponent<SpriteRenderer>().sprite = erosionPointSprites[1];
            
        }
        if (transform.position.y == 0)
        {
            GetComponent<SpriteRenderer>().sprite = erosionPointSprites[2];
            
        }
        if (transform.position.x == boardSize + 1 || transform.position.x == 0)
        {
            GetComponent<SpriteRenderer>().sprite = erosionPointSprites[3];
            
        }
        if (transform.position.y >= 1 && transform.position.y <= boardSize
            && transform.position.x >= 1 && transform.position.x <= boardSize)
        {
            GetComponent<SpriteRenderer>().sprite = erosionPointSprites[0];
            
        }
    }

    //based on euler angle of EP, what direction is it  facing
    public void FindErosionDirection()
    {

        //if facing down
        if (transform.eulerAngles.z > 359.5 || transform.eulerAngles.z < 0.5)
        {
           
            upEPFacing = false;
            downEPFacing = true;
            rightEPFacing = false;
            leftEPFacing = false;
           
        }

        //if facing up
        else if (transform.eulerAngles.z > 179.5 && transform.eulerAngles.z < 180.5)
        {
           
            upEPFacing = true;
            downEPFacing = false;
            rightEPFacing = false;
            leftEPFacing = false;

        }

        //if facing right
        else if (transform.eulerAngles.z > 89.5 && transform.eulerAngles.z < 90.5)
        {            
            upEPFacing = false;
            downEPFacing = false;
            rightEPFacing = true;
            leftEPFacing = false;

        }

        //if facing left
        else if (transform.eulerAngles.z > 269.5 && transform.eulerAngles.z < 270.5)
        {
            
            upEPFacing = false;
            downEPFacing = false;
            rightEPFacing = false;
            leftEPFacing = true;

        }        
        
    }

    //bool is facing direction of EP, gathering info to decide which direction to erode
    public void FindErosionTargets()
    {

        //down
        if (downEPFacing)
        {

           /* int primaryTargetX = (int)transform.position.x;
            int primaryTargetY = (int)transform.position.y - 1;

            var primaryTargetTagNameX = primaryTargetX.ToString();
            var primaryTargetTagNameY = primaryTargetY.ToString();*/



            //if inside board, get other potential options
            if (transform.position.x >= 1 && transform.position.x <= board.boardSize 
                && transform.position.y >= 1 && transform.position.y <= board.boardSize)
            {
                
                primaryErosionTarget = board.allTilesArray[(int)transform.position.x, (int)transform.position.y - 1];

                //potentialA
                /*int potentialTargetAX = (int)transform.position.x - 1;
                int potentialTargetAY = (int)transform.position.y;

                var potentialTargetATagNameX = potentialTargetAX.ToString();
                var potentialTargetATagNameY = potentialTargetAY.ToString();*/

                potentialErosionTargetA = board.allTilesArray[(int)transform.position.x - 1, (int)transform.position.y];

                //potentialB      
                /*int potentialTargetBX = (int)transform.position.x + 1;
                int potentialTargetBY = (int)transform.position.y;

                var potentialTargetBTagNameX = potentialTargetBX.ToString();
                var potentialTargetBTagNameY = potentialTargetBY.ToString();*/

                potentialErosionTargetB = board.allTilesArray[(int)transform.position.x + 1, (int)transform.position.y];

                CheckResistanceOptions();
                                                
            }
            //if outside board, aka at starting position, erosion point becomes primary
            else
            {

                /*erosionTarget = GameObject.FindGameObjectWithTag(primaryTargetTagNameX + "," + primaryTargetTagNameY);
                erosionTargetPos = new Vector2(primaryTargetX, primaryTargetY);*/
                erosionTarget = board.allTilesArray[(int)transform.position.x, (int)transform.position.y - 1];
                erosionTargetPos = new Vector2((int)erosionTarget.transform.position.x, (int)erosionTarget.transform.position.y);
                Debug.Log(erosionTargetPos);

            }

        }

        else if (upEPFacing)
        {
            /*int primaryTargetX = (int)transform.position.x;
            int primaryTargetY = (int)transform.position.y + 1;

            var primaryTargetTagNameX = primaryTargetX.ToString();
            var primaryTargetTagNameY = primaryTargetY.ToString();*/


            //if inside board, get other potential options
            if (transform.position.x >= 1 && transform.position.x <= board.boardSize
                && transform.position.y >= 1 && transform.position.y <= board.boardSize)
            {

                primaryErosionTarget = board.allTilesArray[(int)transform.position.x, (int)transform.position.y + 1];

                //potentialA
                /*int potentialTargetAX = (int)transform.position.x + 1;
                int potentialTargetAY = (int)transform.position.y;

                var potentialTargetATagNameX = potentialTargetAX.ToString();
                var potentialTargetATagNameY = potentialTargetAY.ToString();*/

                potentialErosionTargetA = board.allTilesArray[(int)transform.position.x + 1, (int)transform.position.y];

                //potentialB      
                /*int potentialTargetBX = (int)transform.position.x - 1;
                int potentialTargetBY = (int)transform.position.y;

                var potentialTargetBTagNameX = potentialTargetBX.ToString();
                var potentialTargetBTagNameY = potentialTargetBY.ToString();*/

                potentialErosionTargetB = board.allTilesArray[(int)transform.position.x - 1, (int)transform.position.y];

                CheckResistanceOptions();

            }
            //if outside board, aka at starting position, erosion point becomes primary
            else
            {

                erosionTarget = board.allTilesArray[(int)transform.position.x, (int)transform.position.y + 1];
                erosionTargetPos = new Vector2((int)erosionTarget.transform.position.x, (int)erosionTarget.transform.position.y);
            }
        }

        else if (rightEPFacing)
        {

            /*int primaryTargetX = (int)transform.position.x + 1;
            int primaryTargetY = (int)transform.position.y;

            var primaryTargetTagNameX = primaryTargetX.ToString();
            var primaryTargetTagNameY = primaryTargetY.ToString();*/


            //if inside board, get other potential options
            if (transform.position.x >= 1 && transform.position.x <= board.boardSize
                && transform.position.y >= 1 && transform.position.y <= board.boardSize)
            {

                primaryErosionTarget = board.allTilesArray[(int)transform.position.x + 1, (int)transform.position.y];

                //potentialA
                /*int potentialTargetAX = (int)transform.position.x;
                int potentialTargetAY = (int)transform.position.y - 1;

                var potentialTargetATagNameX = potentialTargetAX.ToString();
                var potentialTargetATagNameY = potentialTargetAY.ToString();*/

                potentialErosionTargetA = board.allTilesArray[(int)transform.position.x, (int)transform.position.y - 1];

                //potentialB      
                /*int potentialTargetBX = (int)transform.position.x;
                int potentialTargetBY = (int)transform.position.y + 1;

                var potentialTargetBTagNameX = potentialTargetBX.ToString();
                var potentialTargetBTagNameY = potentialTargetBY.ToString();*/

                potentialErosionTargetB = board.allTilesArray[(int)transform.position.x, (int)transform.position.y + 1];

                CheckResistanceOptions();

            }
            //if outside board, aka at starting position, erosion point becomes primary
            else
            {

                erosionTarget = board.allTilesArray[(int)transform.position.x + 1, (int)transform.position.y];
                erosionTargetPos = new Vector2((int)erosionTarget.transform.position.x, (int)erosionTarget.transform.position.y);
            }
        }

        else if (leftEPFacing)
        {

            /*int primaryTargetX = (int)transform.position.x - 1;
            int primaryTargetY = (int)transform.position.y;

            var primaryTargetTagNameX = primaryTargetX.ToString();
            var primaryTargetTagNameY = primaryTargetY.ToString();*/


            //if inside board, get other potential options
            if (transform.position.x >= 1 && transform.position.x <= board.boardSize
                && transform.position.y >= 1 && transform.position.y <= board.boardSize)
            {

                primaryErosionTarget = board.allTilesArray[(int)transform.position.x - 1, (int)transform.position.y];

                //potentialA
                /*int potentialTargetAX = (int)transform.position.x;
                int potentialTargetAY = (int)transform.position.y + 1;

                var potentialTargetATagNameX = potentialTargetAX.ToString();
                var potentialTargetATagNameY = potentialTargetAY.ToString();*/

                potentialErosionTargetA = board.allTilesArray[(int)transform.position.x, (int)transform.position.y + 1];

                //potentialB      
                /*int potentialTargetBX = (int)transform.position.x;
                int potentialTargetBY = (int)transform.position.y - 1;

                var potentialTargetBTagNameX = potentialTargetBX.ToString();
                var potentialTargetBTagNameY = potentialTargetBY.ToString();*/

                potentialErosionTargetB = board.allTilesArray[(int)transform.position.x, (int)transform.position.y - 1];

                CheckResistanceOptions();

            }
            //if outside board, aka at starting position, erosion point becomes primary
            else
            {

                erosionTarget = board.allTilesArray[(int)transform.position.x - 1, (int)transform.position.y];
                erosionTargetPos = new Vector2((int)erosionTarget.transform.position.x, (int)erosionTarget.transform.position.y);
            }
        }
    }

    //based on info from FindErosionTargets(), gets resistance of tiles and sets GO erosionTarget and vec2 erosionTargetPos
    private void CheckResistanceOptions()
    {
        
        primaryRes = primaryErosionTarget.GetComponent<EarthTile>().GetResistance();
        potentialResA = potentialErosionTargetA.GetComponent<EarthTile>().GetResistance();
        potentialResB = potentialErosionTargetB.GetComponent<EarthTile>().GetResistance();

        if (potentialResA <= potentialResB)
        {

            int differenceAbs = Mathf.Abs(potentialResA - primaryRes);


            if (primaryRes <= potentialResA)
            {
                erosionTarget = primaryErosionTarget;
                erosionTargetPos = new Vector2(erosionTarget.transform.position.x, erosionTarget.transform.position.y);
            }
            else
            {
                if (differenceAbs >= 2)
                {
                    erosionTarget = potentialErosionTargetA;
                    erosionTargetPos = new Vector2(erosionTarget.transform.position.x, erosionTarget.transform.position.y);
                }
                else
                {
                    erosionTarget = primaryErosionTarget;
                    erosionTargetPos = new Vector2(erosionTarget.transform.position.x, erosionTarget.transform.position.y);
                }

            }
        }
        else
        {

            int differenceAbs = Mathf.Abs(potentialResB - primaryRes);


            if (primaryRes <= potentialResB)
            {
                erosionTarget = primaryErosionTarget;
                erosionTargetPos = new Vector2(erosionTarget.transform.position.x, erosionTarget.transform.position.y);
            }
            else
            {
                if (differenceAbs >= 2)
                {
                    erosionTarget = potentialErosionTargetB;
                    erosionTargetPos = new Vector2(erosionTarget.transform.position.x, erosionTarget.transform.position.y);
                }
                else
                {
                    erosionTarget = primaryErosionTarget;
                    erosionTargetPos = new Vector2(erosionTarget.transform.position.x, erosionTarget.transform.position.y);
                }

            }
        }
                
    }

    //calls the erode resistance method on the erosiontargets earth tile script
    public void ErodeTarget()
    {        
        erosionTarget.GetComponent<EarthTile>().ErodeResistance();
        erosionTarget = null;
    }

    //Next methods will be to check in all directions to see if Ep should move, is next to bedrock tile
    //all directions are called by the MoveChecker()
    private bool CheckUp()
    {

        /*int potentialEPMovementTargetX = (int)transform.position.x;
        int potentialEPMovementTargetY = (int)transform.position.y + 1;

        var potentialEPMovementTargetTagNameX = potentialEPMovementTargetX.ToString();
        var potentialEPMovementTargetTagNameY = potentialEPMovementTargetY.ToString();*/

        potentialEPMovementTarget = board.allTilesArray[(int)transform.position.x, (int)transform.position.y + 1];

        if (potentialEPMovementTarget != null && potentialEPMovementTarget.GetComponent<EarthTile>() != null &&
            potentialEPMovementTarget.GetComponent<EarthTile>().GetResistance() == 0)
        {
            movementOptionsCount++;
            return true;
        }
        else { return false; }

    }

    private bool CheckDown()
    {

        /*int potentialEPMovementTargetX = (int)transform.position.x;
        int potentialEPMovementTargetY = (int)transform.position.y - 1;

        var potentialEPMovementTargetTagNameX = potentialEPMovementTargetX.ToString();
        var potentialEPMovementTargetTagNameY = potentialEPMovementTargetY.ToString();*/

        potentialEPMovementTarget = board.allTilesArray[(int)transform.position.x, (int)transform.position.y - 1];

        if (potentialEPMovementTarget != null && potentialEPMovementTarget.GetComponent<EarthTile>() != null &&
            potentialEPMovementTarget.GetComponent<EarthTile>().GetResistance() == 0)
        {
            movementOptionsCount++;
            return true;
        }
        else { return false; }

    }

    private bool CheckRight()
    {        
        /*int potentialEPMovementTargetX = (int)transform.position.x + 1;
        int potentialEPMovementTargetY = (int)transform.position.y;

        var potentialEPMovementTargetTagNameX = potentialEPMovementTargetX.ToString();
        var potentialEPMovementTargetTagNameY = potentialEPMovementTargetY.ToString();*/

        potentialEPMovementTarget = board.allTilesArray[(int)transform.position.x + 1, (int)transform.position.y];

        if (potentialEPMovementTarget != null && potentialEPMovementTarget.GetComponent<EarthTile>() != null &&
            potentialEPMovementTarget.GetComponent<EarthTile>().GetResistance() == 0)
        {
            movementOptionsCount++;
            return true;
        }
        else { return false; }

    }

    private bool CheckLeft()
    {

        /*int potentialEPMovementTargetX = (int)transform.position.x - 1;
        int potentialEPMovementTargetY = (int)transform.position.y;

        var potentialEPMovementTargetTagNameX = potentialEPMovementTargetX.ToString();
        var potentialEPMovementTargetTagNameY = potentialEPMovementTargetY.ToString();*/

        potentialEPMovementTarget = board.allTilesArray[(int)transform.position.x - 1, (int)transform.position.y];

        if (potentialEPMovementTarget != null && potentialEPMovementTarget.GetComponent<EarthTile>() != null &&
            potentialEPMovementTarget.GetComponent<EarthTile>().GetResistance() == 0)
        {
            movementOptionsCount++;
            return true;
        }
        else { return false; }

    }

    //checks in direction depending on the position and rotation of the EP
    public void MoveChecker()
    {

        //set movementOptions acount to 0

        movementOptionsCount = 0;
        //if ep at the starting point

        if (transform.position == erosionPointStart)
        {
            //if at top, check down only
            if (transform.position.y == board.boardSize + 1)
            {
                moveUp = false;
                moveDown = CheckDown();
                moveRight = false;
                moveLeft = false;

            }
            //if at bottom, check up only
            else if (transform.position.y == 0)
            {
                moveUp = CheckUp();
                moveDown = false;
                moveRight = false;
                moveLeft = false;
            }
            //if on right side, check left only
            else if (transform.position.x == board.boardSize + 1)
            {
                moveUp = false;
                moveDown = false;
                moveRight = false;
                moveLeft = CheckLeft();
            }
            //if at left side, check right only
            else if (transform.position.x == 0)
            {      
                moveUp = false;
                moveDown = false;
                moveRight = CheckRight();
                moveLeft = false;
            }
        }
        else
        {
            //checking in directions depeinding on facing direciton
            if (upEPFacing && !downEPFacing && !rightEPFacing && !leftEPFacing)
            {
                moveUp = CheckUp();
                moveDown = false;
                moveRight = CheckRight();
                moveLeft = CheckLeft();
                
            }

            if (!upEPFacing && downEPFacing && !rightEPFacing && !leftEPFacing)
            {
                moveUp = false;
                moveDown = CheckDown();
                moveRight = CheckRight();
                moveLeft = CheckLeft();
            }

            if (!upEPFacing && !downEPFacing && rightEPFacing && !leftEPFacing)
            {      
                moveUp = CheckUp();
                moveDown = CheckDown();
                moveRight = CheckRight();
                moveLeft = false;
            }

            if (!upEPFacing && !downEPFacing && !rightEPFacing && leftEPFacing)
            {
                moveUp = CheckUp();
                moveDown = CheckDown();
                moveRight = false;
                moveLeft = CheckLeft();
            }
        }

        //if movement options are less than 2, normal movement. if more stop moving and dont erode
        if (movementOptionsCount < 2)
        {

            //if any directions return true, will move set to true allowing HandleMove() to execute
            if (moveUp || moveDown || moveRight || moveLeft)
            {

                willMove = true;
                MoveErosionPoint(moveUp, moveDown, moveRight, moveLeft);




            }

            if (!moveUp && !moveDown && !moveRight && !moveLeft)
            {
                willMove = false;
            }
        }
        else
        {

            willMove = false;
            gameManager.NoErosionAlert();
        }
        
    }

    //give EP new cordinates and designate water as contiguous by changing tag to waterkeep
    private void MoveErosionPoint(bool moveUp, bool moveDown, bool moveRight, bool moveLeft)
    {
        if (moveUp)
        {
            column = (int)transform.position.x;
            row = (int)transform.position.y + 1;
            newRotation = new Vector3(0, 0, 180);

            /*var targetBedrockTagNameX = column.ToString();
            var targetBedrockTagNameY = row.ToString();*/

                      

            HandleMove();
        }

        if (moveDown)
        {

            column = (int)transform.position.x;
            row = (int)transform.position.y - 1;

            newRotation = new Vector3(0, 0, 0);

            /*var targetBedrockTagNameX = column.ToString();
            var targetBedrockTagNameY = row.ToString();*/

            

            HandleMove();
        }

        if (moveRight)
        {
            column = (int)transform.position.x + 1;
            row = (int)transform.position.y;
            newRotation = new Vector3(0, 0, 90);

            /*var targetBedrockTagNameX = column.ToString();
            var targetBedrockTagNameY = row.ToString();*/

           

            HandleMove();
        }

        if (moveLeft)
        {
            column = (int)transform.position.x - 1;
            row = (int)transform.position.y;
            newRotation = new Vector3(0, 0, 270);

            /*var targetBedrockTagNameX = column.ToString();
            var targetBedrockTagNameY = row.ToString();*/

            

            HandleMove();
        }
     


       /* if (targetBedrock.GetComponentInChildren<Water>() != null)
        {
            Debug.Log("in water tag checker");
            targetBedrock.GetComponentInChildren<Water>().tag = "waterkeep";
        }    */   
    }


    //actually move the ep to the next tile
    public void HandleMove()
    {


        moveXPos = column;
        moveYPos = row;
        rotateEP = newRotation;

        movePos = new Vector2(moveXPos, moveYPos);
        transform.position = movePos;
        transform.eulerAngles = rotateEP;
        


    }

    //sections of river that are not contigous to the source will not have water flowing
    //this moves the EP back to starting position. this will be followed by a move checker in GM
    public void SourceCheck()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;        
        willMove = true;
        column = board.riverStartX;
        row = board.riverStartY;
        newRotation = board.startingRotationZ;
        HandleMove();
        
    }



    public void TagWaterAsWaterKeep()
    {
        //if inside the board
        if(transform.position.x <= board.boardSize && transform.position.x >= 1
            && transform.position.y <= board.boardSize && transform.position.y >= 1)
        {            
            if (board.allTilesArray[(int)transform.position.x, (int)transform.position.y] != null 
                && board.allTilesArray[(int)transform.position.x, (int)transform.position.y].transform.childCount >= 4)
            {
                board.allTilesArray[(int)transform.position.x, (int)transform.position.y].transform.GetChild(3).tag = "waterkeep";
            }
        }
    }

    public void SpriteOn()
    {
        spriteRenderer.enabled = true;
    }

    public void SpriteOff()
    {
        spriteRenderer.enabled = false;
    }
}
