using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class PlayerData 
{
    public int stageProgressPD;
    public bool[] stageProgressArrayPD;
    public int mapProgressPD;

    public bool testingModeActivePD;
    public string[] testingUpgradeTypesPD;
    public int[] testingUpgradeCostsPD;

    public int[] availableUpgradesArrayPD;
    public int[] availableResourcesArrayPD;

    public PlayerData(StoryManager storyManager)
    {
        stageProgressPD = storyManager.stageProgress;
        stageProgressArrayPD = storyManager.stageProgressArray;
        mapProgressPD = storyManager.mapProgress;

        availableUpgradesArrayPD = storyManager.availableUpgradesArraySM;
        availableResourcesArrayPD = storyManager.availableResourcesArraySM;

        testingModeActivePD = storyManager.testingModeActive;
        testingUpgradeTypesPD = storyManager.testingUpgradeTypesSM;
        testingUpgradeCostsPD = storyManager.testingUpgradeCostsSM;        
    }

}
