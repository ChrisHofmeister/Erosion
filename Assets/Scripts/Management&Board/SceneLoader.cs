using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

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
        SceneManager.LoadScene("Three");
    }

    public void LoadGameFour()
    {
        SceneManager.LoadScene("Four");
    }

    public void LoadGameFive()
    {
        SceneManager.LoadScene("Five");
    }

    public void LoadGameSix()
    {
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
}
