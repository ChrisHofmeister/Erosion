using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private StoryManager storyManager;

    public bool storyManagerExists;

    private void Start()
    {
        storyManager = FindObjectOfType<StoryManager>();

        if(storyManager != null)
        {
            storyManagerExists = true;
        }
    }

    public void LoadGameSelectScreen()
    {
        SceneManager.LoadScene("Game Select Screen");
    }

    public void LoadStartScreen()
    {
        SceneManager.LoadScene("Start Screen");
    }

    public void LoadGameThree()
    {
        if (storyManager)
        {
            storyManager.DestroySelf();
        }
        SceneManager.LoadScene("Three");
    }

    public void LoadGameFour()
    {
        if (storyManager)
        {
            storyManager.DestroySelf();
        }
        SceneManager.LoadScene("Four");
    }

    public void LoadGameFive()
    {
        if (storyManager)
        {
            storyManager.DestroySelf();
        }
        SceneManager.LoadScene("Five");
    }

    public void LoadGameSix()
    {
        if (storyManager)
        {
            storyManager.DestroySelf();
        }
        SceneManager.LoadScene("Six");
    }

    public void LoadInstructionsScece()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadStoryStageScreen()
    {
        SceneManager.LoadScene("Story");
    }

    public void LoadStoryMapScreen()
    {
        SceneManager.LoadScene("Map");
    }

    public void LoadTesterScreen()
    {
        SceneManager.LoadScene("Tester Screen");
    }

    public void LoadMapFromStage()
    {
        storyManager.SwitchMapStageMode();
        SceneManager.LoadScene("Map");        
    }

    public void SaveAndQuit()
    {
        storyManager.SaveStoryManager();
        Application.Quit();
    }
}
