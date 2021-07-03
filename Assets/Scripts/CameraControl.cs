using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraControl : MonoBehaviour
{
   //adjust the alignment and zoom of camera depending on size of board
    private float cameraX;
    private float cameraY;
    private float cameraZ = -10f;
    private float cameraSize;
    Board board;
    private int boardSize;

    public Camera m_OrthographicCamera;


    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        m_OrthographicCamera = FindObjectOfType<Camera>();

        

        EstablishValues();

        Vector3 cameraPos = new Vector3(cameraX, cameraY, cameraZ);

        transform.position = cameraPos;

        

        m_OrthographicCamera.orthographicSize = cameraSize;






    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //depending on board size, the x, y, and ortho size of the camera needs to change
    //******will need some tweeking later once overlay mask applied
    private void EstablishValues()
    {
        if (board.boardSize == 3)
        {
            cameraX = 2f;
            cameraY = 4f;
            cameraSize = 4.5f;
        }

        if (board.boardSize == 4)
        {
            cameraX = 2.5f;
            cameraY = 4.9f;
            cameraSize = 5.4f;
        }

        if (board.boardSize == 5)
        {
            cameraX = 3f;
            cameraY = 5.7f;
            cameraSize = 6.2f;
        }
        //need to update camera for 6, which is 8x8 and 7, 9x9
        if (board.boardSize == 6)
        {
            cameraX = 3.5f;
            cameraY = 6.5f;
            cameraSize = 7f;
        }

        if (board.boardSize == 7)
        {
            cameraX = 4f;
            cameraY = 7.25f;
            cameraSize = 9f;
        }


    }




}
