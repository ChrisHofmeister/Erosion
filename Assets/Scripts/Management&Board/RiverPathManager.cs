using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverPathManager : MonoBehaviour
{

    public Bedrock[] uncheckedTilesArray = new Bedrock[0];
    public List<Bedrock> uncheckedTilesList = new List<Bedrock>();
    public List<Bedrock> pathTiles = new List<Bedrock>();
    public List<Bedrock> nonPathTiles = new List<Bedrock>();
    public List<Bedrock> tileNeighbors = new List<Bedrock>();
    public List<Bedrock> alreadyCheckedTiles = new List<Bedrock>();

    private Board board;

    private float shortestDistance;
    private Bedrock nextClosestBedrock;
    private Bedrock tileToCheck;
    private bool doubleCheckComplete;

    public int uncheckedTilesLength;
    public int pathTilesLength;
    public int nonPathTilesLength;

    private GameManager gameManager;


    void Start()
    {
        board = FindObjectOfType<Board>();
        gameManager = FindObjectOfType<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (uncheckedTilesList != null)
        {
            uncheckedTilesLength = uncheckedTilesList.Count;
        }
        if (pathTiles != null)
        {
            pathTilesLength = pathTiles.Count;
        }
        if (nonPathTiles != null)
        {
            nonPathTilesLength = nonPathTiles.Count;
        }
    }

    public void GatherAllBedrockTiles()
    {
        uncheckedTilesList.Clear();
        pathTiles.Clear();
        nonPathTiles.Clear();

        if (FindObjectOfType<Bedrock>() != null)
        {
            uncheckedTilesArray = FindObjectsOfType<Bedrock>();
        }
        if (uncheckedTilesArray.Length > 0)
        {
            foreach (Bedrock tile in uncheckedTilesArray)
            {
                uncheckedTilesList.Add(tile);
            }

            foreach (Bedrock tile in uncheckedTilesList)
            {
                if (tile.transform.position == board.safeZone)
                {
                    pathTiles = new List<Bedrock> { tile };
                    nonPathTiles = new List<Bedrock>();                    
                    FindPath();
                    break;
                }
            }
        }
    }

    private void FindPath()
    {
        while(uncheckedTilesList.Count > 0)
        {
            tileToCheck = GetShortestDistanceToSource();
            tileNeighbors.Clear();
            tileNeighbors = tileToCheck.CheckForNeighbors();
            uncheckedTilesList.Remove(tileToCheck);

            if (tileNeighbors.Count > 0)
            {
                foreach (Bedrock neighbor in tileNeighbors)
                {
                    if (pathTiles.Contains(neighbor))
                    {
                        if (pathTiles.Contains(tileToCheck))
                        {
                            continue;                           
                        }
                        else
                        {
                            pathTiles.Add(tileToCheck);
                        }
                    }
                }
            }
            if (pathTiles.Contains(tileToCheck))
            {
                foreach (Bedrock neighbor in tileNeighbors)
                {
                    if (pathTiles.Contains(neighbor))
                    {
                        uncheckedTilesList.Remove(neighbor);                        
                    }
                    else
                    {
                        pathTiles.Add(neighbor);
                        uncheckedTilesList.Remove(neighbor);
                    }
                }
            }
            else
            {
                if (nonPathTiles.Contains(tileToCheck))
                {
                    continue;
                }
                else
                {
                    nonPathTiles.Add(tileToCheck);
                }                
            }            
        }
        
        DoubleCheck();
    }

    private Bedrock GetShortestDistanceToSource()
    {
        shortestDistance = 999;

        foreach (Bedrock tile in uncheckedTilesList)
        {
            if(tile.distanceFromSource <= shortestDistance)
            {
                shortestDistance = tile.distanceFromSource;
                nextClosestBedrock = tile;
            }
        }
        return nextClosestBedrock;
        /*
        for (int i = 0; i >= uncheckedTilesList.Count; i++)
        {
            if (uncheckedTilesList[i].distanceFromSource <= shortestDistance)
            {
                shortestDistance = uncheckedTilesList[i].distanceFromSource;
                nextClosestBedrock = uncheckedTilesList[i];
            }
        }

        return nextClosestBedrock;
        */
    }

    private void DoubleCheck()
    {
        doubleCheckComplete = false;

        while (!doubleCheckComplete)
        {

            alreadyCheckedTiles.Clear();

            foreach (Bedrock tile in nonPathTiles)
            {
                tileNeighbors = tile.CheckForNeighbors();

                foreach (Bedrock neighbor in tileNeighbors)
                {
                    if (pathTiles.Contains(neighbor))
                    {                        
                        pathTiles.Add(tile);
                        alreadyCheckedTiles.Add(tile);
                    }
                }

            }
            if(alreadyCheckedTiles.Count > 0)
            {
                foreach(Bedrock tile in alreadyCheckedTiles)
                {
                    nonPathTiles.Remove(tile);
                }
            }
            else
            {
                doubleCheckComplete = true;
            }

        }

        ResolveWaterTiles();

    }

    private void ResolveWaterTiles()
    {
        foreach (Bedrock tile in nonPathTiles)
        {
            tile.childWater.WaterOff();
        }
        foreach (Bedrock tile in pathTiles)
        {
            tile.childWater.WaterOn();
        }

        pathTiles.Clear();
        nonPathTiles.Clear();

    }
}
