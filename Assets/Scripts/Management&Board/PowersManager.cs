using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowersManager : MonoBehaviour
{
    //bool for if storyMode active
    [SerializeField] bool storyModeActive;

    //if power mode on, swipe controls dont work.  after using a power, automatically reverts to erosion mode(aka powermode false)
    public bool powerModeOn = false;

    //power panel
    [SerializeField] GameObject powerPanel;
    [SerializeField] GameObject promptPanel;

    //collider cover, skip move button, power button to turn off to avoid issue when power panel open
    [SerializeField] GameObject colliderCover;
    [SerializeField] GameObject skipMoveButton;
    [SerializeField] GameObject powerButton;

    //power buttons
    [SerializeField] GameObject rainButton;
    [SerializeField] GameObject delugeButton;
    [SerializeField] GameObject stormButton;
    [SerializeField] GameObject freezeButton;

    //power usage cost
    [SerializeField] TextMeshProUGUI rainCostDisplayText;
    [SerializeField] GameObject rainCostGO;
    private int rainCost = 1;
    [SerializeField] TextMeshProUGUI freezeCostDisplayText;
    [SerializeField] GameObject freezeCostGO;
    private int freezeCost = 1;
    [SerializeField] TextMeshProUGUI delugeCostDisplayText;
    [SerializeField] GameObject delugeCostGO;
    private int delugeCost = 2;
    [SerializeField] TextMeshProUGUI stormCostDisplayText;
    [SerializeField] GameObject stormCostGO;
    private int stormCost = 3;

    //uses for each power
    [SerializeField]  TextMeshProUGUI rainUsesDisplayText;
    [SerializeField] GameObject rainUsesGO;
    private int rainUses = 1;
    [SerializeField] TextMeshProUGUI freezeUsesDisplayText;
    [SerializeField] GameObject freezeUsesGO;
    private int freezeUses = 2;
    [SerializeField] TextMeshProUGUI delugeUsesDisplayText;
    [SerializeField] GameObject delugeUsesGO;
    private int delugeUses = 1;
    [SerializeField] TextMeshProUGUI stormUsesDisplayText;
    [SerializeField] GameObject stormUsesGO;
    private int stormUses = 1;

    //number of times storm hits
    int stormIndex = 3;

    //active power
    public string activePower;

    //board
    private Board board;

    //managers
    private GameManager gameManager;
    private StoryManager storyManager;    

    //erosion point
    private ErosionPoint erosionPoint;

    //alert indicator 1=no more uses, 2=battery low
    private int alertIndicator;

    //bool to show which powers are active
    public bool rainActive;
    public bool delugeActive;
    public bool stormActive;
    public bool freezeActive;





    // Start is called before the first frame update
    void Start()
    {


        board = FindObjectOfType<Board>();
        gameManager = FindObjectOfType<GameManager>();
        erosionPoint = FindObjectOfType<ErosionPoint>();
        storyManager = FindObjectOfType<StoryManager>();

        rainCostDisplayText.text = rainCost.ToString();
        freezeCostDisplayText.text = freezeCost.ToString();
        delugeCostDisplayText.text = delugeCost.ToString();
        stormCostDisplayText.text = stormCost.ToString();
        if (storyModeActive)
        {
            TurnPowerOff("rain");
            TurnPowerOff("deluge");
            TurnPowerOff("storm");
            TurnPowerOff("freeze");
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (storyModeActive)
        {
            CheckWhichPowersToDisplay();
        }
        else
        {
            rainUsesDisplayText.text = rainUses.ToString();
            freezeUsesDisplayText.text = freezeUses.ToString();
            delugeUsesDisplayText.text = delugeUses.ToString();
            stormUsesDisplayText.text = stormUses.ToString();
        }

        if (rainActive)
        {
            rainUsesDisplayText.text = rainUses.ToString();
            rainCostDisplayText.text = rainCost.ToString();
        }
        if (delugeActive)
        {
            delugeUsesDisplayText.text = delugeUses.ToString();
            delugeCostDisplayText.text = delugeCost.ToString();
        }
        if (stormActive)
        {
            stormUsesDisplayText.text = stormUses.ToString();
            stormCostDisplayText.text = stormCost.ToString();
        }
        if (freezeActive)
        {
            freezeUsesDisplayText.text = freezeUses.ToString();
            freezeCostDisplayText.text = freezeCost.ToString();
        }

    }

    private void CheckWhichPowersToDisplay()
    {
        if(storyManager.availableUpgradesArraySM[4] >= 1)
        {
            TurnPowerOn("rain");
        }
        if (storyManager.availableUpgradesArraySM[6] >= 1)
        {
            TurnPowerOn("deluge");
        }
        if (storyManager.availableUpgradesArraySM[8] >= 1)
        {
            TurnPowerOn("storm");
        }
        if (storyManager.availableUpgradesArraySM[10] >= 1)
        {
            TurnPowerOn("freeze");
        }

    }

    private void TurnPowerOn(string power)
    {
        if(power == "rain")
        {
            rainButton.SetActive(true);
            rainActive = true;
            rainCostGO.SetActive(true);
            rainUsesGO.SetActive(true);

        }
        if (power == "deluge")
        {
            delugeButton.SetActive(true);
            delugeActive = true;
            delugeCostGO.SetActive(true);
            delugeUsesGO.SetActive(true);
        }
        if (power == "storm")
        {
            stormButton.SetActive(true);
            stormActive = true;
            stormCostGO.SetActive(true);
            stormUsesGO.SetActive(true);
        }
        if (power == "freeze")
        {
            freezeButton.SetActive(true);
            freezeActive = true;
            freezeCostGO.SetActive(true);
            freezeUsesGO.SetActive(true);
        }
    }

    private void TurnPowerOff(string power)
    {
        if (power == "rain")
        {
            rainButton.SetActive(false);
            rainActive = false;
            rainCostGO.SetActive(false);
            rainUsesGO.SetActive(false);
        }
        if (power == "deluge")
        {
            delugeButton.SetActive(false);
            delugeActive = true;
            delugeCostGO.SetActive(false);
            delugeUsesGO.SetActive(false);
        }
        if (power == "storm")
        {
            stormButton.SetActive(false);
            stormActive = true;
            stormCostGO.SetActive(false);
            stormUsesGO.SetActive(false);
        }
        if (power == "freeze")
        {
            freezeButton.SetActive(false);
            freezeActive = true;
            freezeCostGO.SetActive(false);
            freezeUsesGO.SetActive(false);
        }
    }

    public void OpenPowerPanel()
    {        
        colliderCover.SetActive(true);
        skipMoveButton.SetActive(false);
        powerButton.SetActive(false);
        powerPanel.SetActive(true);
    }

    public void ClosePowerPanel()
    {        
        colliderCover.SetActive(false);
        skipMoveButton.SetActive(true);
        powerButton.SetActive(true);
        powerPanel.SetActive(false);
    }

    public void ActivateRainPower()
    {
        alertIndicator = 0;

        colliderCover.SetActive(false);
        skipMoveButton.SetActive(true);
        powerButton.SetActive(true);
        powerPanel.SetActive(false);

        if (rainUses <= 0)
        {
            alertIndicator = 1;
        }
        else if(gameManager.turnTimer <= rainCost - 1)
        {
            alertIndicator = 2;
        }
        else
        {
            powerModeOn = true;
            activePower = "rain";
        }

        if(alertIndicator != 0)
        {
            gameManager.StartPowersAlert(alertIndicator);
            alertIndicator = 0;
        }


    }

    public void UseRainPower(Vector2 location)
    {
        GameObject targetObject = board.allEarthTiles[(int)location.x,(int)location.y];

        if(targetObject != null && targetObject.GetComponent<Soil>() != null)
        {
            targetObject.GetComponent<Soil>().rainBonus++;
            rainUses--;
            //reducing the turn timer and updating text
            gameManager.ReduceTurnTimer(rainCost);

        }

        powerModeOn = false;
        targetObject = null;
    }

    public void ActivateFreezePower()
    {
        alertIndicator = 0;

        colliderCover.SetActive(false);
        skipMoveButton.SetActive(true);
        powerButton.SetActive(true);
        powerPanel.SetActive(false);

        if (freezeUses <= 0)
        {
            alertIndicator = 1;
        }
        else if (gameManager.turnTimer <= freezeCost - 1)
        {
            alertIndicator = 2;
        }
        else
        {
            powerModeOn = true;
            activePower = "freeze";
        }

        if (alertIndicator != 0)
        {
            gameManager.StartPowersAlert(alertIndicator);            
        }

    }

    public void UseFreezePower(Vector2 location)
    {
        GameObject targetObject = board.allEarthTiles[(int)location.x, (int)location.y];

        if (targetObject != null && targetObject.GetComponent<EarthTile>() != null)
        {
            targetObject.GetComponent<EarthTile>().AddResistance(1);
            freezeUses--;
            //reducing the turn timer and updating text
            gameManager.ReduceTurnTimer(rainCost);

        }

        powerModeOn = false;
        targetObject = null;
    }

    public void ActivateDelugePower()
    {
        if (!board.gameStarted)
        {
            board.gameStarted = true;
        }

        alertIndicator = 0;

        if (delugeUses == 0)
        {
            alertIndicator = 1;
        }
        else if (gameManager.turnTimer <= delugeCost - 1)
        {
            alertIndicator = 2;
        }
        else
        {
            powerModeOn = true;
            activePower = "deluge";
        }

        if (alertIndicator != 0)
        {
            colliderCover.SetActive(false);
            skipMoveButton.SetActive(true);
            powerButton.SetActive(true);
            powerPanel.SetActive(false);
            gameManager.StartPowersAlert(alertIndicator);
        }
        else
        {
            powerPanel.SetActive(false);
            promptPanel.SetActive(true);
        }

    }

    public void ActivateStormPower()
    {
        if (!board.gameStarted)
        {
            board.gameStarted = true;
        }

        alertIndicator = 0;

        if (stormUses == 0)
        {
            alertIndicator = 1;
        }
        else if (gameManager.turnTimer <= stormCost - 1)
        {
            alertIndicator = 2;
        }
        else
        {
            powerModeOn = true;
            activePower = "storm";
        }

        if (alertIndicator != 0)
        {
            colliderCover.SetActive(false);
            skipMoveButton.SetActive(true);
            powerButton.SetActive(true);
            powerPanel.SetActive(false);
            gameManager.StartPowersAlert(alertIndicator);
        }
        else
        {
            powerPanel.SetActive(false);
            promptPanel.SetActive(true);
        }

    }

    public void UseDelugePower(Vector2 erosionTargetLocation)
    {
        delugeUses--;
        gameManager.ReduceTurnTimer(delugeCost);

        GameObject targetObject = board.allEarthTiles[(int)erosionTargetLocation.x, (int)erosionTargetLocation.y];

        targetObject.GetComponent<EarthTile>().StartDeluge();

        powerModeOn = false;
    }

    public void UseStormPower()
    {
        stormUses--;
        gameManager.ReduceTurnTimer(stormCost);

        for (int i = 1; i <= stormIndex; i++)
        {
            int column = Random.Range(1, board.boardSize + 1);
            int row = Random.Range(1, board.boardSize + 1);

            GameObject targetTile = board.allEarthTiles[column, row];

            if(targetTile != null && targetTile.GetComponent<Soil>() != null)
            {
                targetTile.GetComponent<Soil>().rainBonus += 2;

            }
            else if(targetTile != null && targetTile.GetComponent<Bedrock>() != null)
            {
                continue;
            }
            else
            {
                targetTile.GetComponent<EarthTile>().ErodeResistance(2);
            }
        }
    }

    public void UseSelectedPower()
    {
        promptPanel.SetActive(false);
        colliderCover.SetActive(false);
        skipMoveButton.SetActive(true);
        powerButton.SetActive(true);

        if (activePower == "deluge")
        {
            UseDelugePower(erosionPoint.ReturnPrimaryErosionTargetLocation());
        }
        if(activePower == "storm")
        {
            UseStormPower();
        }
    }

    public void DontUseSelectedPower()
    {
        activePower = null;

        promptPanel.SetActive(false);
        colliderCover.SetActive(false);
        skipMoveButton.SetActive(true);
        powerButton.SetActive(true);

    }

}
