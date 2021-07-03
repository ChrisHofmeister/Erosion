using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverEnd : MonoBehaviour
{
    //array for different sprites available depending on position of river end

    [SerializeField] private Sprite[] riverEndSprites;

    //instance of board to get size
    private Board board;

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        OrientSide(board.boardSize);
    }


    private void OrientSide(int boardSize)
    {
        if (transform.position.y == boardSize + 1)
        {
            GetComponent<SpriteRenderer>().sprite = riverEndSprites[0];
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (transform.position.y == 0)
        {
            GetComponent<SpriteRenderer>().sprite = riverEndSprites[1];
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (transform.position.x == boardSize + 1)
        {
            GetComponent<SpriteRenderer>().sprite = riverEndSprites[3];
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (transform.position.x == 0)
        {
            GetComponent<SpriteRenderer>().sprite = riverEndSprites[2];
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
