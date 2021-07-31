using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : MonoBehaviour
{
    private int soilBonus = 0;

    public int waterNeighbors = 0;

    private Board board;

    private GameObject targetGameObject;

    private Seed childSeed;

    public int rainBonus;

    //soil sprites + renderer, change to indicate if rainbonus as applied
    [SerializeField] Sprite[] soilSprites;
    private SpriteRenderer spriteRenderer;

    //managers
    private ResourceManager resourceManager;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        childSeed = GetComponentInChildren<Seed>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        resourceManager = FindObjectOfType<ResourceManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rainBonus <= 3)
        {
            spriteRenderer.sprite = soilSprites[rainBonus];
        }
        else
        {
            spriteRenderer.sprite = soilSprites[3];
        }
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
        CheckRainBonus();

        if(waterNeighbors != 0)
        {
            childSeed.PlayGrowAnimation(waterNeighbors);
            if (gameManager.storyModeActive)
            {
                CheckForAwardedResources();
            }
        }

        return soilBonus;
    }

    private void CheckRainBonus()
    {
        if(rainBonus >= 1)
        {
            for(int rainLoop = 0; rainLoop < rainBonus; rainLoop++)
            {
                waterNeighbors += 1;
                soilBonus += 150;
            }
        }
    }

    private void CheckForAwardedResources()
    {
        if(waterNeighbors >= 1 && waterNeighbors <= 2)
        {
            resourceManager.AddResource("OR", "C", 1);
        }
        if(waterNeighbors == 3)
        {
            resourceManager.AddResource("OR", "U", 1);
        }
        if (waterNeighbors == 4)
        {
            resourceManager.AddResource("OR", "R", 1);
        }
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
