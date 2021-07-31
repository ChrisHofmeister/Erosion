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
    
    //menu panels
    [SerializeField] GameObject menuPanels;
    [SerializeField] GameObject powersPanel;
    [SerializeField] GameObject resourcesPanel;
    [SerializeField] GameObject upgradesPanel;
    [SerializeField] GameObject upgradePromptPanel;
    GameObject activePanel = null;

    [SerializeField] GameObject menuButton;
    [SerializeField] GameObject skipMoveButton;
    [SerializeField] GameObject colliderCover;
    [SerializeField] GameObject mapsGO;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        board = FindObjectOfType<Board>();
        powersManager = FindObjectOfType<PowersManager>();
        resourceManager = FindObjectOfType<ResourceManager>();
        upgradesManager = FindObjectOfType<UpgradesManager>();
        storyManager = FindObjectOfType<StoryManager>();

        menuPanels.SetActive(false);
        powersPanel.SetActive(false);
        resourcesPanel.SetActive(false);
        upgradesPanel.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMenu()
    {
        if(activePanel == null)
        {
            activePanel = powersPanel;
        }

        if (storyManager.mapModeActive)
        {
            mapsGO.SetActive(false);
            menuButton.SetActive(false);
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

}
