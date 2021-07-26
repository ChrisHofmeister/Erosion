using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    //different sprites. will change depending on where the gaem object is
    [SerializeField] Sprite[] gridSprites;

    //board to get boardSize
    private Board board;

    //parents transform
    private Transform parentTransform;

    private Vector3 newRotation;
    private Vector3 rotateGrid;

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        parentTransform = GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSprite();
        HandleRotation();
    }

    private void ChangeSprite()
    {
        //if in top left corner. all grid edges
        if((int)parentTransform.position.x == 1 && (int)parentTransform.position.y == board.boardSize)
        {
            GetComponent<SpriteRenderer>().sprite = gridSprites[0];
            newRotation = new Vector3(0, 0, 0);
        }
        //if along the left side, but not at top., sides and bottom of grid, but not top
        else if((int)parentTransform.position.x == 1 && (int)parentTransform.position.y != board.boardSize)
        {
            GetComponent<SpriteRenderer>().sprite = gridSprites[1];
            newRotation = new Vector3(0, 0, 0);
        }
        //if along the top, but not on left side. top, bottom and right side, but not left by rotate gameobject 270
        else if((int)parentTransform.position.x != 1 && (int)parentTransform.position.y == board.boardSize)
        {
            GetComponent<SpriteRenderer>().sprite = gridSprites[1];
            newRotation = new Vector3(0, 0, 90f);
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = gridSprites[2];
            newRotation = new Vector3(0, 0, 0);
        }

    }
    //rotates grid, if necessary, based on position
    private void HandleRotation()
    {
        rotateGrid = newRotation;
        transform.eulerAngles = rotateGrid;
    }
}
