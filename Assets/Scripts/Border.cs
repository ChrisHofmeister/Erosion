using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{

    //different sprites. will change depending on where the game object is
    [SerializeField] Sprite[] borderSprites;

    //board to get board size
    private Board board;

    //parent transform for position
    private Transform parentTransform;

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        parentTransform = GetComponentInParent<Transform>();
        ChangeSprite();
    }

    private void ChangeSprite()
    {
        //if the top left corner
        if ((int)parentTransform.position.x == 0 && (int)parentTransform.position.y == board.boardSize +1)
        {
            GetComponent<SpriteRenderer>().sprite = borderSprites[6];            
        }
        //if top right
        if ((int)parentTransform.position.x == board.boardSize + 1 && (int)parentTransform.position.y == board.boardSize + 1)
        {
            GetComponent<SpriteRenderer>().sprite = borderSprites[7];
        }
        //if top edge, but not corner
        if ((int)parentTransform.position.x >= 1 && (int)parentTransform.position.x <= board.boardSize && (int)parentTransform.position.y == board.boardSize + 1)
        {
            GetComponent<SpriteRenderer>().sprite = borderSprites[5];
        }
        //if right edge, but not corner. note the flipped x and y
        if ((int)parentTransform.position.y >= 1 && (int)parentTransform.position.y <= board.boardSize && (int)parentTransform.position.x == board.boardSize + 1)
        {
            GetComponent<SpriteRenderer>().sprite = borderSprites[4];
        }
        //if left edge, but not corner. note the flipped x and y
        if ((int)parentTransform.position.y >= 1 && (int)parentTransform.position.y <= board.boardSize && (int)parentTransform.position.x == 0)
        {
            GetComponent<SpriteRenderer>().sprite = borderSprites[3];
        }
        //if the bottom left corner
        if ((int)parentTransform.position.x == 0 && (int)parentTransform.position.y == 0)
        {
            GetComponent<SpriteRenderer>().sprite = borderSprites[1];
        }
        //if bottom right
        if ((int)parentTransform.position.x == board.boardSize + 1 && (int)parentTransform.position.y == 0)
        {
            GetComponent<SpriteRenderer>().sprite = borderSprites[2];
        }
        //if bottom edge, but not corner
        if ((int)parentTransform.position.x >= 1 && (int)parentTransform.position.x <= board.boardSize && (int)parentTransform.position.y == 0)
        {
            GetComponent<SpriteRenderer>().sprite = borderSprites[0];
        }

    }
}