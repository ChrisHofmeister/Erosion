using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    //game manager and Board to control active butons and collider cover
    private GameManager gameManager;
    private Board board;

    //other panel managers to control which text appears
    private PowersManager powersManager;
    private ResourceManager resourceManager;
    private UpgradesManager upgradesManager;
    private StoryManager storyManager;
    private SceneLoader sceneLoader;
    
    //menu panels
    [SerializeField] GameObject menuPanels;
    [SerializeField] GameObject powersPanel;
    [SerializeField] GameObject resourcesPanel;
    [SerializeField] GameObject upgradesPanel;
    [SerializeField] GameObject upgradePromptPanel;

    [SerializeField] GameObject StartScreenPanel;
    [SerializeField] GameObject AdventureButton;
    [SerializeField] GameObject NewAdventureButton;
    [SerializeField] GameObject LoadAdventureButton;
    [SerializeField] GameObject NewGameAlertPanel;

    GameObject activePanel = null;

    [SerializeField] GameObject menuButton;
    [SerializeField] GameObject skipMoveButton;
    [SerializeField] GameObject colliderCover;
    [SerializeField] GameObject mapsGO;
    [SerializeField] GameObject quitButton;

    [SerializeField] GameObject checklistPanel;
    [SerializeField] GameObject checklistButton;


    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        gameManager = FindObjectOfType<GameManager>();
        board = FindObjectOfType<Board>();
        powersManager = FindObjectOfType<PowersManager>();
        resourceManager = FindObjectOfType<ResourceManager>();
        upgradesManager = FindObjectOfType<UpgradesManager>();
        storyManager = FindObjectOfType<StoryManager>();
        SetUpStartPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMenu()
    {
        if(activePanel == null)
        {
            if (storyManager.mapModeActive)
            {
                activePanel = resourcesPanel;
            }
            else
            {
                activePanel = powersPanel;
            }
        }

        if (storyManager.mapModeActive)
        {
            mapsGO.SetActive(false);
            menuButton.SetActive(false);
            quitButton.SetActive(false);
            checklistButton.SetActive(false);
            activePanel.SetActive(true);
            menuPanels.SetActive(true);
            
        }
        else if (storyManager.stageModeActive)
        {
            skipMoveButton.SetActive(false);
            menuButton.SetActive(false);
            colliderCover.SetActive(true);

            activePanel.SetActive(true);
            menuPanels.SetActive(true);
        }
        else
        {
            skipMoveButton.SetActive(false);
            menuButton.SetActive(false);
            colliderCover.SetActive(true);

            activePanel.SetActive(true);
            menuPanels.SetActive(true);
        }
    }

    public void CloseMenu()
    {


        if (storyManager.mapModeActive)
        {
            mapsGO.SetActive(true);
            menuButton.SetActive(true);
            menuPanels.SetActive(false);
            quitButton.SetActive(true);
            checklistButton.SetActive(true);
        }
        else if (storyManager.stageModeActive)
        {
            skipMoveButton.SetActive(true);
            menuButton.SetActive(true);
            colliderCover.SetActive(false);

            menuPanels.SetActive(false);
        }
        else
        {
            skipMoveButton.SetActive(true);
            menuButton.SetActive(true);
            colliderCover.SetActive(false);

            menuPanels.SetActive(false);
        }
    }

    public void OpenPowersPanel()
    {
        activePanel.SetActive(false);
        powersPanel.SetActive(true);

        activePanel = powersPanel;        
    }

    public void OpenResroucesPanel()
    {
        activePanel.SetActive(false);
        resourcesPanel.SetActive(true);

        activePanel = resourcesPanel;
    }

    public void OpenUpgradesPanel()
    {
        activePanel.SetActive(false);
        upgradesPanel.SetActive(true);

        activePanel = upgradesPanel;
    }

    public void SaveAndQuitGame()
    {
        storyManager.SaveStoryManager();
        sceneLoader.QuitGame();
    }

    public void LoadPlayerData()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        storyManager.LoadPlayerDataIntoStoryMode(data);
        sceneLoader.LoadStoryMapScreen();

    }

    public void NewGameAlert()
    {
        StartScreenPanel.SetActive(false);
        NewGameAlertPanel.SetActive(true);
    }

    public void CloseNewGameAlertPanel()
    {
        StartScreenPanel.SetActive(true);
        NewGameAlertPanel.SetActive(false);
    }

    private void SetUpStartPanel()
    {
        if(StartScreenPanel != null)
        {
            if (storyManager.saveDataExists)
            {
                AdventureButton.SetActive(false);
                NewAdventureButton.SetActive(true);
                LoadAdventureButton.SetActive(true);
            }
            else
            {
                AdventureButton.SetActive(true);
                NewAdventureButton.SetActive(false);
                LoadAdventureButton.SetActive(false);
            }
        }        
    }

    public void OpenChecklistPanel()
    {
        mapsGO.SetActive(false);
        menuButton.SetActive(false);
        quitButton.SetActive(false);
        checklistButton.SetActive(false);
        checklistPanel.SetActive(true);        
    }

    public void CloseChecklistPanel()
    {
        mapsGO.SetActive(true);
        menuButton.SetActive(true);
        checklistPanel.SetActive(false);
        quitButton.SetActive(true);
        checklistButton.SetActive(true);
    }

    public void TurnOffMapButtonsAndMenu()
    {
        menuPanels.SetActive(false);
        quitButton.SetActive(false);
        menuButton.SetActive(false);
        checklistButton.SetActive(false);
        mapsGO.SetActive(true);
    }

    public void TurnOnMapButtons()
    {
        quitButton.SetActive(true);
        menuButton.SetActive(true);
        checklistButton.SetActive(true);
    }
}
