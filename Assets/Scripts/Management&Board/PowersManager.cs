using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowersManager : MonoBehaviour
{
    //if power mode on, swipe controls dont work.  after using a power, automatically reverts to erosion mode(aka powermode false)
    public bool powerModeOn = false;

    //power panel
    [SerializeField] GameObject powerPanel;

    //collider cover, skip move button, power button to turn off to avoid issue when power panel open
    [SerializeField] GameObject colliderCover;
    [SerializeField] GameObject skipMoveButton;
    [SerializeField] GameObject powerButton;

    //uses for each power
    [SerializeField]  TextMeshProUGUI rainUsesDisplayText;
    private int rainUses = 1;

    //active power
    public string activePower;

    //board
    private Board board;


    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
    }

    // Update is called once per frame
    void Update()
    {
        rainUsesDisplayText.text = rainUses.ToString();
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
        powerModeOn = true;
        activePower = "rain";

        colliderCover.SetActive(false);
        skipMoveButton.SetActive(true);
        powerButton.SetActive(true);
        powerPanel.SetActive(false);
    }

    public void UseRainpower(Vector2 location)
    {
        GameObject targetObject = board.allEarthTiles[(int)location.x,(int)location.y];

        if(targetObject != null && targetObject.GetComponent<Soil>() != null && rainUses >= 1)
        {
            targetObject.GetComponent<Soil>().rainBonus++;
            rainUses--;
        }

        powerModeOn = false;
    }
}
