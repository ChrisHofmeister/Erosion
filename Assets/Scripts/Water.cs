using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    //various water sprites, will call correct ones based on surroundings
    // 0-straight 1-bend 2-split 3-end
    [SerializeField] Sprite[] waterSprites;

    //bools for various water tile types
    private bool isRiver;
    private bool isLake;

    //game object that is being reviewed for reviewsurroundings()
    private GameObject targetObject;

    //board object to refer to the safezone location
    private Board board;

    //bools will be set to true if surrounding tile has water
    public bool up = false;
    public bool down = false;
    public bool left = false;
    public bool right = false;
    public bool diagUpRight = false;
    public bool diagUpLeft = false;
    public bool diagDownRight = false;
    public bool diagDownLeft = false;

    //bool for is water is on or off
    public bool waterOn;

    //reference to parents earthtile script to get positioning
    private EarthTile parentEarthTile;

    //int for number of water matches for testing. should always be one.
    [SerializeField] private int testingWaterMatches;
    [SerializeField] private int testingWaterMatchNumber;
    // Start is called before the first frame update
    void Start()
    {
        waterOn = false;
        board = FindObjectOfType<Board>();
        parentEarthTile = GetComponentInParent<EarthTile>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (parentEarthTile.column >= 1 && parentEarthTile.column <= board.boardSize
            && parentEarthTile.row >= 1 && parentEarthTile.row <= board.boardSize)
        {
            CheckSurroundingsAndAct();
        }
    }

    //
    public void CheckSurroundingsAndAct()
    {
        //bool return true if has water around tile
        up = CheckUp();
        down = CheckDown();
        right = CheckRight();
        left = CheckLeft();
        diagUpRight = CheckDiagUpRight();
        diagUpLeft = CheckDiagUpLeft();
        diagDownRight = CheckDiagDownRight();
        diagDownLeft = CheckDiagDownLeft();

        //if water is on safe zone, set it automatically after normal process runs
        if (GetComponentInParent<Transform>().position == board.safeZone)
        {
            if (GetComponentInParent<Transform>().position.y == board.boardSize)
            {
                up = true;
            }

            else if(GetComponentInParent<Transform>().position.x == board.boardSize)
            {
                right = true;
            }

            if (GetComponentInParent<Transform>().position.y == 1)
            {
                down = true;
            }

            if (GetComponentInParent<Transform>().position.x == 1)
            {
                left = true;
            }

        }
        //setting testing watermatches to 0, if statement at the end of method checks to ensure no dupe options
        testingWaterMatches = 0;
        testingWaterMatchNumber = 99999999;
        //depending on what water surrounds tile, will change the sprite and orientation
        
        //1-only up
        if (up && !down && !right && !left && !diagUpRight 
            && !diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 1;
        }
        //2-only down
        else if (!up && down && !right && !left 
            && !diagUpRight && !diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 2;
        }
        //3-only right
        else if (!up && !down && right && !left 
            && !diagUpRight && !diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 3;
        }
        //4-only left
        else if (!up && !down && !right && left
            && !diagUpRight && !diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 4;
        }
        //5-up right
        else if(up && !down && right && !left
            && !diagUpRight && !diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 5;
        }
        //6-up left
        else if (up && !down && !right && left
            && !diagUpRight && !diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 6;
        }
        //7-down right
        else if (!up && down && right && !left
            && !diagUpRight && !diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 7;
        }
        //8-down left
        else if (!up && down && !right && left
            && !diagUpRight && !diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 8;
        }
        //00-up down
        else if (up && down && !right && !left
            && !diagUpRight && !diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[0];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 00;
        }
        //0-right left
        else if (!up && !down && right && left
            && !diagUpRight && !diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[0];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 0;
        }
        //9-up right left
        else if (up && !down && right && left
            && !diagUpRight && !diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[2];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 9;
        }
        //10-down right left
        else if (!up && down && right && left
            && !diagUpRight && !diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[2];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 10;
        }
        //11-up down right
        else if (up && down && right && !left
            && !diagUpRight && !diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[2];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 11;
        }
        //12-up down left
        else if (up && down && !right && left
            && !diagUpRight && !diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[2];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 12;
        }

        //13-up right diagUpRight
        else if (up && !down && right && !left
            && diagUpRight && !diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[4];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 13;
        }

        //14-up left diagUpLeft
        else if (up && !down && !right && left
            && !diagUpRight && diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[4];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 14;
        }
         //15-down right dialDownRight
        else if(!up && down && right && !left
            && !diagUpRight && !diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[4];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 15;
        }
        //16-down left dialDownLeft
        else if (!up && down && !right && left
            && !diagUpRight && !diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[4];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 16;
        }
        //17-up right diagUpRight diagDownRight
        else if (up && !down && right && !left
             && diagUpRight && !diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[4];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 17;
        }
        //18-up right diagUpRight diagUpLeft
        else if (up && !down && right && !left
             && diagUpRight && diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[4];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 18;
        }
        //19-up left diagUpLeft diagDownLeft
        else if (up && !down && !right && left
             && !diagUpRight && diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[4];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 19;
        }
        //20-up left diagUpLeft diagUpRight
        else if (up && !down && !right && left
             && diagUpRight && diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[4];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 20;
        }
        //21-down right dialDownRight diagUpRight
        else if (!up && down && right && !left
            && diagUpRight && !diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[4];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 21;
        }
        //22-down right dialDownRight diagDownLeft
        else if (!up && down && right && !left
            && !diagUpRight && !diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[4];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 22;
        }
        //23-down left dialDownLeft diagUpLeft
        else if (!up && down && !right && left
            && !diagUpRight && diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[4];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 23;
        }
        //24-down left dialDownLeft diagDownRight
        else if (!up && down && !right && left
            && !diagUpRight && !diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[4];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 24;
        }
        //25-up down right diagDownRight
        else if (up && down && right && !left
            && !diagUpRight && !diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[7];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 25;
        }
        //26-up down right diagUpRight
        else if (up && down && right && !left
            && diagUpRight && !diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[6];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 26;
        }
        //27-up down left diagDownLeft
        else if (up && down && !right && left
            && !diagUpRight && !diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[6];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 27;
        }
        //28-up down left diagUpLeft
        else if (up && down && !right && left
            && !diagUpRight && diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[7];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 28;
        }
        //29-up right left diagUpRight
        else if (up && !down && right && left
            && diagUpRight && !diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[7];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 29;
        }
        //30-up right left diagUpLeft
        else if (up && !down && right && left
            && !diagUpRight && diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[6];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 30;
        }
        //31-down right left diagDownRight
        else if (!up && down && right && left
            && !diagUpRight && !diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[6];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 31;
        }
        //32-down right left diagDownLeft
        else if (!up && down && right && left
            && !diagUpRight && !diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[7];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 32;
        }
        //33-up down diagDownRight
        else if (up && down && !right && !left
            && !diagUpRight && !diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[0];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 33;
        }
        //34-up down diagDownLeft
        else if (up && down && !right && !left
            && !diagUpRight && !diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[0];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 34;
        }
        //35-up down diagUpRight
        else if (up && down && !right && !left
            && diagUpRight && !diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[0];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 35;
        }
        //36-up down diagUpLeft
        else if (up && down && !right && !left
            && !diagUpRight && diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[0];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 36;
        }
        //37-right left diagUpRight
        else if (!up && !down && right && left
            && diagUpRight && !diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[0];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 37;
        }
        //38-right left diagUpLeft
        else if (!up && !down && right && left
            && !diagUpRight && diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[0];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 38;
        }
        //39-right left diagDownRight
        else if (!up && !down && right && left
            && !diagUpRight && !diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[0];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 39;
        }
        //40-right left diagDownLeft
        else if (!up && !down && right && left
            && !diagUpRight && !diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[0];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 40;
        }
        //41-up down right diagUpRight diagDownRight
        else if (up && down && right && !left
            && diagUpRight && !diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[5];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 41;
        }
        //42-up down left diagUpLeft diagDownLeft
        else if (up && down && !right && left
            && !diagUpRight && diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[5];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 42;
        }
        //43-up right left diagUpRight diagUpLeft
        else if (up && !down && right && left
            && diagUpRight && diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[5];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 43;
        }
        //44-down right left diagDownRight diagDownLeft
        else if (!up && down && right && left
            && !diagUpRight && !diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[5];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 44;
        }
        //45-up down diagDownRight diagDownLeft
        else if (up && down && !right && !left
            && !diagUpRight && !diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[0];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 45;
        }
        //46-up down diagUpRight diagUpLeft
        else if (up && down && !right && !left
            && diagUpRight && diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[0];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 46;
        }
        //47-right left diagUpRight diagDownRight
        else if (!up && !down && right && left
            && diagUpRight && !diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[0];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 47;
        }
        //48-right left diagUpLeft diagDownLeft
        else if (!up && !down && right && left
            && !diagUpRight && diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[0];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 48;
        }
        //49-up down right left diagDownRight diagDownLeft
        else if (up && down && right && left
            && !diagUpRight && !diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[8];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 49;
        }
        //50-up down right left diagUpRight diagUpLeft
        else if (up && down && right && left
            && diagUpRight && diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[8];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 50;
        }
        //51-up down right left diagUpRight diagDownRight
        else if (up && down && right && left
            && diagUpRight && !diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[8];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 51;
        }
        //52-up down right left diagUpLeft diagDownLeft
        else if (up && down && right && left
            && !diagUpRight && diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[8];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 52;
        }
        //53-down right left diagUpRight diagDownRight diagDownLeft
        else if (!up && down && right && left
            && diagUpRight && !diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[5];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 53;
        }
        //54-down right left diagUpLeft diagDownRight diagDownLeft
        else if (!up && down && right && left
            && !diagUpRight && diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[5];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 54;
        }
        //55-up right left diagUpRight diagUpLeft diagDownRight
        else if (up && !down && right && left
            && diagUpRight && diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[5];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 55;
        }
        //56-up right left diagUpRight diagUpLeft diagDownLeft
        else if (up && !down && right && left
            && diagUpRight && diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[5];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 56;
        }
        //57-up down right diagUpRight diagUpLeft diagDownRight
        else if (up && down && right && !left
            && diagUpRight && diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[5];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 57;
        }
        //58-up down right diagUpRight diagDownRight diagDownLeft
        else if (up && down && right && !left
            && diagUpRight && !diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[5];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 58;
        }
        //59-up down left diagUpRight diagUpLeft diagDownLeft
        else if (up && down && !right && left
            && diagUpRight && diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[5];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 59;
        }
        //60-up down left diagUpLeft diagDownRight diagDownLeft
        else if (up && down && !right && left
            && !diagUpRight && diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[5];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 60;
        }
        //61-up down right left diagUpRight diagUpLeft diagDownRight diagDownLeft
        else if (up && down && right && left
            && diagUpRight && diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[11];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 61;
        }
        //62-right left diagUpRight diagUpLeft
        else if (!up && !down && right && left
            && diagUpRight && diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[0];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 62;
        }
        //63-right left diagDownRight diagDownLeft
        else if (!up && !down && right && left
            && !diagUpRight && !diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[0];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 63;
        }
        //64-right left diagUpRight diagDownLeft
        else if (!up && !down && right && left
            && diagUpRight && !diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[0];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 64;
        }
        //65-right left diagUpLeft diagDownRight
        else if (!up && !down && right && left
            && !diagUpRight && diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[0];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 65;
        }
        //66-up down diagUpRight diagDownRight
        else if (up && down && !right && !left
            && diagUpRight && !diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[0];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 66;
        }
        //67-up down diagUpLeft diagDownLeft
        else if (up && down && !right && !left
            && !diagUpRight && diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[0];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 67;
        }
        //68-up down diagUpRight diagDownLeft
        else if (up && down && !right && !left
            && diagUpRight && !diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[0];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 68;
        }
        //69-up down diagUpLeft diagDownRight
        else if (up && down && !right && !left
            && !diagUpRight && diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[0];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 69;
        }
        //70-right diagUpRight
        else if (!up && !down && right && !left
            && diagUpRight && !diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 70;
        }
        //71-right diagDownRight
        else if (!up && !down && right && !left
            && !diagUpRight && !diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 71;
        }
        //72-right diagUpRight diagDownRight
        else if (!up && !down && right && !left
            && diagUpRight && !diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 72;
        }
        //73-left diagDownleft
        else if (!up && !down && !right && left
            && !diagUpRight && !diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 73;
        }
        //74-left diagUpleft
        else if (!up && !down && !right && left
            && !diagUpRight && diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 74;
        }
        //75-left diagUpleft diagDownLeft
        else if (!up && !down && !right && left
            && !diagUpRight && diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 75;
        }
        //76-down diagDownRight
        else if (!up && down && !right && !left
            && !diagUpRight && !diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 76;
        }
        //77-down diagDownLeft
        else if (!up && down && !right && !left
            && !diagUpRight && !diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 77;
        }
        //78-down diagDownRight diagDownLeft
        else if (!up && down && !right && !left
            && !diagUpRight && !diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 78;
        }
        //79-up diagUpRight
        if (up && !down && !right && !left 
            && diagUpRight && !diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 79;
        }
        //80-up diagUpLeft
        if (up && !down && !right && !left
            && !diagUpRight && diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 80;
        }
        //81-up diagUpRight diagUpLeft
        if (up && !down && !right && !left
            && diagUpRight && diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 81;
        }
        //82-down right diagUpRight diagDownRight diagDownLeft
        else if (!up && down && right && !left
            && diagUpRight && !diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[4];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 82;
        }
        //83-down left diagUpLeft diagDownRight diagDownLeft
        else if (!up && down && !right && left
            && !diagUpRight && diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[4];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 83;
        }
        //84-up right diagUpRight diagUpleft diagDownRight
        else if (up && !down && right && !left
            && diagUpRight && diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[4];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 84;
        }
        //85-up left diagUpRight diagUpleft diagDownLeft
        else if (up && !down && !right && left
            && diagUpRight && diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[4];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 85;
        }
        //86-up down right left diagDownRight
        else if (up && down && right && left
            && !diagUpRight && !diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[9];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 86;
        }
        //87-up down right left diagDownLeft
        else if (up && down && right && left
            && !diagUpRight && !diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[9];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 87;
        }
        //88-up down right left diagUpRight
        else if (up && down && right && left
            && diagUpRight && !diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[9];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 88;
        }
        //89-up down right left diagUpLeft
        else if (up && down && right && left
            && !diagUpRight && diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[9];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 89;
        }
        //90-up down right diagDownRight diagDownLeft
        else if (up && down && right && !left
            && !diagUpRight && !diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[7];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 90;
        }
        //91-up down right diagUpLeft diagDownRight
        else if (up && down && right && !left
            && !diagUpRight && diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[7];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 91;
        }
        //92-up down left diagDownLeft diagDownRight
        else if (up && down && !right && left
            && !diagUpRight && !diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[6];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 92;
        }
        //93-up down left diagupRight diagDownLeft
        else if (up && down && !right && left
            && diagUpRight && !diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[6];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 93;
        }
        //94-down right left diagUpLeft diagDownLeft
        else if (!up && down && right && left
            && !diagUpRight && diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[7];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 94;
        }
        //95-down right left diagUpRight diagDownLeft
        else if (!up && down && right && left
            && diagUpRight && !diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[7];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 95;
        }
        //96-up right left diagUpLeft diagDownLeft
        else if (up && !down && right && left
            && !diagUpRight && diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[6];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 96;
        }
        //97-up right left diagUpLeft diagDownRight
        else if (up && !down && right && left
            && !diagUpRight && diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[6];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 97;
        }
        //98-down right diagDownLeft
        else if (!up && down && right && !left
            && !diagUpRight && !diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 98;
        }
        //99-down left diagDownRight
        else if (!up && down && !right && left
            && !diagUpRight && !diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 99;
        }
        //100-up left diagUpRight diagDownLeft
        else if (up && !down && !right && left
            && diagUpRight && !diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 100;
        }
        //101-up right diagUpLeft diagDownRight
        else if (up && !down && right && !left
            && !diagUpRight && diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 101;
        }
        //102-down left diagUpLeft
        else if (!up && down && !right && left
            && !diagUpRight && diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 102;
        }
        //103-down right diagUpRight
        else if (!up && down && right && !left
            && diagUpRight && !diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 103;
        }
        //104-up right diagDownRight
        else if (up && !down && right && !left
            && !diagUpRight && !diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 104;
        }
        //105-up left diagDownLeft
        else if (up && !down && !right && left
            && !diagUpRight && !diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 105;
        }
        //106-down left diagUpLeft diagDownRight
        else if (!up && down && !right && left
            && !diagUpRight && diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 106;
        }
        //107-down right diagUpRight diagDownLeft
        else if (!up && down && right && !left
            && diagUpRight && !diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 107;
        }
        //108-up right diagdiagUpLeft
        else if (up && !down && right && !left
            && !diagUpRight && diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 108;
        }
        //109-up left diagUpRight
        else if (up && !down && !right && left
            && diagUpRight && !diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 109;
        }
        //110-down right left diagUpRight diagUpLeft diagDownRight diagDownLeft
        else if (!up && down && right && left
            && diagUpRight && diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[5];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 110;
        }
        //111-up down left diagUpRight diagUpLeft diagDownRight diagDownLeft
        else if (up && down && !right && left
            && diagUpRight && diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[5];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 111;
        }
        //112-up down right diagUpRight diagUpLeft diagDownRight diagDownLeft
        else if (up && down && right && !left
            && diagUpRight && diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[5];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 112;
        }
        //113-up right left diagUpRight diagUpLeft diagDownRight diagDownLeft
        else if (up && !down && right && left
            && diagUpRight && diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[5];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 113;
        }
        //114-up down right left diagUpRight diagDownRight diagDownLeft
        else if (up && down && right && left
            && diagUpRight && !diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[12];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 114;
        }
        //115-up down right left diagUpLeft diagDownRight diagDownLeft
        else if (up && down && right && left
            && !diagUpRight && diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[12];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 115;
        }
        //116-up down right left diagUpRight diagUpLeft diagDownLeft
        else if (up && down && right && left
            && diagUpRight && diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[12];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 116;
        }
        //117-up down right left diagUpRight diagUpLeft diagDownRight
        else if (up && down && right && left
            && diagUpRight && diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[12];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 117;
        }
        //118-up down right left diagUpRight diagDownLeft
        else if (up && down && right && left
            && diagUpRight && diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[13];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 118;
        }
        //119-up down right left diagUpLeft diagDownRight
        else if (up && down && right && left
            && !diagUpRight && diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[13];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 119;
        }
        //120-up down right left
        else if (up && down && right && left
            && !diagUpRight && !diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[10];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 120;
        }
        //121-down right left diagUpRight diagDownRight
        else if (!up && down && right && left
            && diagUpRight && !diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[6];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 121;
        }
        //122-up down left diagDownRight diagDownLeft
        else if (up && down && right && !left
            && !diagUpRight && !diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[6];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 122;
        }
        //123-up right left diagUpLeft diagDownLeft
        else if (up && !down && right && left
            && !diagUpRight && diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[6];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 123;
        }
        //124-up down right diagUpRight diagUpLeft
        else if (up && down && right && !left
            && diagUpRight && diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[6];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 124;
        }
        //125-up down right diagUpRight diagUpLeft diagDownLeft
        else if (up && down && right && !left
            && diagUpRight && diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[14];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 125;
        }
        //126-up down left diagUpRight diagUpLeft diagDownRight diagDownLeft
        else if (up && down && !right && left
            && diagUpRight && diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[15];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 126;
        }
        //127-up down right diagUpLeft diagDownRight diagDownLeft
        else if (up && down && right && !left
            && !diagUpRight && diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[15];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 127;
        }
        //128-up down left diagUpRight diagDownRight diagDownLeft
        else if (up && down && !right && left
            && diagUpRight && !diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[14];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 128;
        }
        //129-down right left diagUpRight diagDownRight diagDownLeft
        else if (!up && down && right && left
            && diagUpRight && diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[14];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 129;
        }
        //130-down right left diagUpRight diagUpLeft diagDownLeft
        else if (!up && down && right && left
            && diagUpRight && diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[15];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 130;
        }
        //131-up right left diagUpRight diagDownRight diagDownLeft
        else if (up && !down && right && left
            && diagUpRight && !diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[15];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 131;
        }
        //132-up right left diagUpLeft diagDownRight diagDownLeft
        else if (up && !down && right && left
            && !diagUpRight && diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[14];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 132;
        }
        //133-up right left diagUpRight diagDownLeft
        else if (up && !down && right && left
            && diagUpRight && !diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[7];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 133;
        }
        //134-up down left diagUpRight diagUpLeft
        else if (up && down && !right && left
            && diagUpRight && diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[7];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 134;
        }
        //135-up down right diagDownRight diagDownLeft
        else if (up && down && right && !left
            && !diagUpRight && !diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[7];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 135;
        }
        //136-down right left diagUpLeft diagDownLeft
        else if (!up && down && right && left
            && !diagUpRight && diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[7];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 32;
        }
        //137-up right diagDownRight diagDownLeft
        else if (up && !down && right && !left
            && !diagUpRight && !diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 137;
        }
        //138-up left diagDownRight diagDownLeft
        else if (up && !down && !right && left
            && !diagUpRight && !diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 138;
        }
        //139-down right diagUpRight diagUpLeft
        else if (!up && down && right && !left
            && diagUpRight && diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 139;
        }
        //140-down left diagUpRight diagUpLeft
        else if (!up && down && !right && left
            && diagUpRight && diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 140;
        }
        //141-down right diagUpLeft diagDownLeft
        else if (!up && down && right && !left
            && !diagUpRight && diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 141;
        }
        //142-down left diagUpright diagDownRight
        else if (!up && down && !right && left
            && diagUpRight && !diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 142;
        }
        //143-up left diagUpRight diagDownRight
        else if (up && !down && !right && left
            && diagUpRight && !diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 143;
        }
        //144-up right diagUpLeft diagDownLeft
        else if (up && !down && right && !left
            && !diagUpRight && diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 144;
        }
        //145-left diagUpRight diagUpleft
        else if (!up && !down && !right && left
            && diagUpRight && diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 145;
        }
        //146-right diagUpRight diagUpLeft
        else if (!up && !down && right && !left
            && diagUpRight && diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 146;
        }
        //147-left diagDownRight diagDownleft
        else if (!up && !down && !right && left
            && !diagUpRight && !diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 147;
        }
        //148-right diagDownRight diagDownLeft
        else if (!up && !down && right && !left
            && !diagUpRight && !diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 148;
        }
        //149-up diagUpRight diagDownRight
        if (up && !down && !right && !left
            && diagUpRight && !diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 149;
        }
        //150-up diagUpLeft diagDownLeft
        if (up && !down && !right && !left
            && !diagUpRight && diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 150;
        }
        //151-down diagUpRight diagDownRight
        else if (!up && down && !right && !left
            && diagUpRight && !diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 151;
        }
        //152-down diagUpLeft diagDownLeft
        else if (!up && down && !right && !left
            && !diagUpRight && diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 152;
        }
        //153-up right diagUpLeft diagDownRight diagDownLeft
        else if (up && !down && right && !left
            && !diagUpRight && diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 153;
        }
        //154-up left diagUpRight diagDownRight diagDownLeft
        else if (up && !down && !right && left
            && diagUpRight && !diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 154;
        }
        //155-down right diagUpRight diagUpLeft diagDownLeft
        else if (!up && down && right && !left
            && diagUpRight && diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 155;
        }
        //156-down left diagUpRight diagUpLeft diagDownRight
        else if (!up && down && !right && left
            && diagUpRight && diagUpLeft && !diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 156;
        }
        //157-down right diagUpright diagUpLeft diagDownLeft
        else if (!up && down && right && !left
            && diagUpRight && diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 157;
        }
        //158-down left diagUpright diagUpLeft diagDownRight
        else if (!up && down && !right && left
            && diagUpRight && diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 158;
        }
        //159-up left diagUpRight diagDownRight diagDownLeft
        else if (up && !down && !right && left
            && diagUpRight && !diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 159;
        }
        //160-up right diagUpLeft diagDownRight diagDownLeft
        else if (up && !down && right && !left
            && !diagUpRight && diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 160;
        }
        //161-left diagUpleft diagDownRight
        else if (!up && !down && !right && left
            && !diagUpRight && diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 161;
        }
        //162-left diagUpRight diagUpleft diagDownRight
        else if (!up && !down && !right && left
            && diagUpRight && diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 162;
        }
        //163-right diagUpRight diagDownLeft
        else if (!up && !down && right && !left
            && diagUpRight && !diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 163;
        }
        //164-right diagUpRight diagUpLeft diagDownLeft
        else if (!up && !down && right && !left
            && diagUpRight && diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 164;
        }
        //165-left diagUpRight diagDownleft
        else if (!up && !down && !right && left
            && diagUpRight && !diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 165;
        }
        //166-left diagUpRight diagDownRight diagDownleft
        else if (!up && !down && !right && left
            && diagUpRight && !diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 166;
        }
        //167-right diagUpLeft diagDownRight
        else if (!up && !down && right && !left
            && !diagUpRight && diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 167;
        }
        //168-right diagUpLeft diagDownRight diagDownLeft
        else if (!up && !down && right && !left
            && !diagUpRight && diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 168;
        }
        //169-up diagUpRight diagDownLeft
        if (up && !down && !right && !left
            && diagUpRight && !diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 169;
        }
        //170-up diagUpRight diagDownRight diagDownLeft
        if (up && !down && !right && !left
            && diagUpRight && !diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 170;
        }
        //171-up diagUpLeft diagDownRight
        if (up && !down && !right && !left
            && !diagUpRight && diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 171;
        }
        //172-up diagUpLeft diagDownRight diagDownLeft
        if (up && !down && !right && !left
            && !diagUpRight && diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 172;
        }
        //173-down diagUpLeft diagDownRight
        else if (!up && down && !right && !left
            && !diagUpRight && diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 173;
        }
        //174-down diagUpRight diagUpLeft diagDownRight
        else if (!up && down && !right && !left
            && diagUpRight && diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 174;
        }
        //175-down diagUpRight diagDownLeft
        else if (!up && down && !right && !left
            && diagUpRight && !diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 175;
        }
        //176-down diagUpRight diagUpLeft diagDownLeft
        else if (!up && down && !right && !left
            && diagUpRight && diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 176;
        }
        //177-left diagUpRight diagUpleft diagDownRight diagDownLeft
        else if (!up && !down && !right && left
            && diagUpRight && diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 177;
        }
        //178-left diagUpleft diagDownRight diagDownLeft
        else if (!up && !down && !right && left
            && !diagUpRight && diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 178;
        }
        //179-left diagUpRight diagUpLeft diagDownLeft
        else if (!up && !down && !right && left
            && diagUpRight && diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 90);
            testingWaterMatches++;
            testingWaterMatchNumber = 179;
        }
        //180-right diagUpRight diagUpLeft diagDownRight diagDownLeft
        else if (!up && !down && right && !left
            && diagUpRight && diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 180;
        }
        //181-right diagUpRight diagDownRight diagDownLeft
        else if (!up && !down && right && !left
            && diagUpRight && !diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 181;
        }
        //182-right diagUpRight diagUpLeft diagDownRight
        else if (!up && !down && right && !left
            && diagUpRight && diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 270);
            testingWaterMatches++;
            testingWaterMatchNumber = 182;
        }
        //183-up diagUpRight diagUpLeft diagDownRight diagDownLeft
        if (up && !down && !right && !left
            && diagUpRight && diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 183;
        }
        //184-up diagUpRight diagUpLeft diagDownRight
        if (up && !down && !right && !left
            && diagUpRight && diagUpLeft && diagDownRight && !diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 184;
        }
        //185-up diagUpRight diagUpLeft diagDownLeft
        if (up && !down && !right && !left
            && diagUpRight && diagUpLeft && !diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 0);
            testingWaterMatches++;
            testingWaterMatchNumber = 185;
        }
        //186-down diagUpRight diagUpLeft diagDownRight diagDownLeft
        else if (!up && down && !right && !left
            && diagUpRight && diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 186;
        }
        //187-down diagUpRight diagDownRight diagDownLeft
        else if (!up && down && !right && !left
            && diagUpRight && !diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 187;
        }
        //188-down diagUpLeft diagDownRight diagDownLeft
        else if (!up && down && !right && !left
            && !diagUpRight && diagUpLeft && diagDownRight && diagDownLeft)
        {
            GetComponent<SpriteRenderer>().sprite = waterSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 180);
            testingWaterMatches++;
            testingWaterMatchNumber = 188;
        }





        /*else if(testingWaterMatches != 1)
        {
            if (testingWaterMatches == 0)
            {
                Debug.LogError("no matches");
            }
            if(testingWaterMatches >= 2)
            {
                Debug.LogError("multiple matches");
            }
        }*/

        /*else
        {
            GetComponent<SpriteRenderer>().sprite = null;
        }*/
    }

    private bool CheckUp()
    {
        
        int objectX = (int)GetComponentInParent<Transform>().position.x;        
        int objectY = (int)GetComponentInParent<Transform>().position.y + 1;

        /*var objectTagNameX = objectX.ToString();
        var objectTagNameY = objectY.ToString();*/
        if(objectX >= 0 && objectX <= board.boardSize + 1 && objectY >= 0 && objectY <= board.boardSize + 1)
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
    //check down and return true if river is child down
    private bool CheckDown()
    {
        int objectX = (int)GetComponentInParent<Transform>().position.x;
        int objectY = (int)GetComponentInParent<Transform>().position.y - 1;

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
        int objectX = (int)GetComponentInParent<Transform>().position.x + 1;
        int objectY = (int)GetComponentInParent<Transform>().position.y;

        /*var objectTagNameX = objectX.ToString();
        var objectTagNameY = objectY.ToString();*/
        if (objectX >= 0 && objectX <= board.boardSize + 1 && objectY >= 0 && objectY <= board.boardSize + 1)
        {
            targetObject = board.allTilesArray[objectX, objectY];
        }

        if (targetObject != null && targetObject.GetComponentInChildren<Water>() != null)
        {
            return true;
        }
        else { return false; }
    }
    //check down and return true if river is child left
    private bool CheckLeft()
    {
        int objectX = (int)GetComponentInParent<Transform>().position.x - 1;
        int objectY = (int)GetComponentInParent<Transform>().position.y;

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

    private bool CheckDiagUpRight()
    {
        int objectX = (int)GetComponentInParent<Transform>().position.x + 1;
        int objectY = (int)GetComponentInParent<Transform>().position.y + 1;

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

    private bool CheckDiagUpLeft()
    {
        int objectX = (int)GetComponentInParent<Transform>().position.x - 1;
        int objectY = (int)GetComponentInParent<Transform>().position.y + 1;

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

    private bool CheckDiagDownRight()
    {
        int objectX = (int)GetComponentInParent<Transform>().position.x + 1;
        int objectY = (int)GetComponentInParent<Transform>().position.y - 1;

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

    private bool CheckDiagDownLeft()
    {
        int objectX = (int)GetComponentInParent<Transform>().position.x - 1;
        int objectY = (int)GetComponentInParent<Transform>().position.y - 1;

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

    public void WaterOn()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        waterOn = true;
        
    }

    public void WaterOff()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        waterOn = false;
        
    }

    public bool EdgeCheck()
    {
        if(waterOn == true)
        {
            if(parentEarthTile.transform.position != board.safeZone)
            {
                if (parentEarthTile.transform.position.x == 1 || parentEarthTile.transform.position.x == board.boardSize
                    || parentEarthTile.transform.position.y == 1 || parentEarthTile.transform.position.y == board.boardSize)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
