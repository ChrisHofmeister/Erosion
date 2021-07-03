using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : MonoBehaviour
{
    private int soilBonus = 0;

    public int waterNeighbors = 0;

    private Board board;

    private GameObject targetGameObject;

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }    
    
    public int CalcSoilBonus()
    {
        CheckUp();
        CheckDown();
        CheckRight();
        CheckLeft();
        CheckUpRight();
        CheckUpLeft();
        CheckDownRight();
        CheckDownLeft();

        return soilBonus;
    }


    private void CheckUp()
    {


        targetGameObject = board.allTilesArray[(int)transform.position.x, (int)transform.position.y + 1];

        if (targetGameObject != null && targetGameObject.GetComponentInChildren<Water>() != null &&
            targetGameObject.GetComponentInChildren<Water>().waterOn == true)
        {
            waterNeighbors += 1;
            soilBonus += 150;

        }
    }

    private void CheckDown()
    {

        targetGameObject = board.allTilesArray[(int)transform.position.x, (int)transform.position.y - 1];

        if (targetGameObject != null && targetGameObject.GetComponentInChildren<Water>() != null &&
            targetGameObject.GetComponentInChildren<Water>().waterOn == true)
        {
            waterNeighbors += 1;
            soilBonus += 150;

        }
    }

    private void CheckRight()
    {

        targetGameObject = board.allTilesArray[(int)transform.position.x + 1, (int)transform.position.y];

        if (targetGameObject != null && targetGameObject.GetComponentInChildren<Water>() != null &&
            targetGameObject.GetComponentInChildren<Water>().waterOn == true)
        {
            waterNeighbors += 1;
            soilBonus += 150;

        }
    }

    private void CheckLeft()
    {
        targetGameObject = board.allTilesArray[(int)transform.position.x - 1, (int)transform.position.y];

        if (targetGameObject != null && targetGameObject.GetComponentInChildren<Water>() != null &&
            targetGameObject.GetComponentInChildren<Water>().waterOn == true)
        {
            waterNeighbors += 1;
            soilBonus += 150; 
        }        

    }

    private void CheckUpRight()
    {
        targetGameObject = board.allTilesArray[(int)transform.position.x + 1, (int)transform.position.y + 1];

        if (targetGameObject != null && targetGameObject.GetComponentInChildren<Water>() != null &&
            targetGameObject.GetComponentInChildren<Water>().waterOn == true)
        {
            waterNeighbors += 1;
            soilBonus += 150;
        }

    }

    private void CheckUpLeft()
    {
        targetGameObject = board.allTilesArray[(int)transform.position.x - 1, (int)transform.position.y + 1];

        if (targetGameObject != null && targetGameObject.GetComponentInChildren<Water>() != null &&
            targetGameObject.GetComponentInChildren<Water>().waterOn == true)
        {
            waterNeighbors += 1;
            soilBonus += 150;
        }

    }

    private void CheckDownRight()
    {
        targetGameObject = board.allTilesArray[(int)transform.position.x + 1, (int)transform.position.y - 1];

        if (targetGameObject != null && targetGameObject.GetComponentInChildren<Water>() != null &&
            targetGameObject.GetComponentInChildren<Water>().waterOn == true)
        {
            waterNeighbors += 1;
            soilBonus += 150;
        }

    }

    private void CheckDownLeft()
    {
        targetGameObject = board.allTilesArray[(int)transform.position.x - 1, (int)transform.position.y - 1];

        if (targetGameObject != null && targetGameObject.GetComponentInChildren<Water>() != null &&
            targetGameObject.GetComponentInChildren<Water>().waterOn == true)
        {
            waterNeighbors += 1;
            soilBonus += 150;
        }

    }
}
