using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number : MonoBehaviour
{
    private EarthTile earthTile;
    private ErosionPoint erosionPoint;
    private Board board;
    public int displayNumberIndex;
    private EarthTile parentEarthTile;
    [SerializeField] private Sprite[] numberSprites;
    public float lerpSpeed;
    int parentResistance;

    private void Start()
    {
        displayNumberIndex = 0;
        erosionPoint = FindObjectOfType<ErosionPoint>();
        board = FindObjectOfType<Board>();
        parentEarthTile = GetComponentInParent<EarthTile>();
        lerpSpeed = Time.deltaTime * 100f;
    }

    private void Update()
    {
        UpdateNumberIfChanged();        
    }

    public void UpdateDisplayNumber(int resistance)
    {


        int numberToUse = resistance;
        if (resistance >= 0)
        {
            GetComponent<SpriteRenderer>().sprite = numberSprites[numberToUse];
        }
        else
        {
            Debug.LogError("resistance is a negitive number");
        }


    }

    public void UpdateNumberIfChanged()
    {
        int parentResistance = parentEarthTile.resistance;

        if (parentResistance != displayNumberIndex)
        {
            if (parentResistance >= 0)
            {
                GetComponent<SpriteRenderer>().sprite = numberSprites[parentResistance];
                displayNumberIndex = parentResistance;                
            }
            else
            {
                Debug.LogError("resistance is a negitive number");
            }
        }
    }
}
