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
    int parentResistance;
    public float spinSpeed;
    private Animator anim;

    private void Start()
    {
        displayNumberIndex = 0;
        erosionPoint = FindObjectOfType<ErosionPoint>();
        board = FindObjectOfType<Board>();
        parentEarthTile = GetComponentInParent<EarthTile>();        
        spinSpeed = .5f;
        anim = GetComponent<Animator>();
        anim.speed = spinSpeed;
        ChangeIndexAndSprite();
    }

    private void Update()
    {
        
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

    public void PlayNumberSpin()
    {

            anim.Play("Number Spin");
  
    }

    public void ChangeIndexAndSprite()
    {
        if (parentEarthTile.resistance >= 0)
        {
            displayNumberIndex = parentEarthTile.resistance;
            GetComponent<SpriteRenderer>().sprite = numberSprites[displayNumberIndex];            
        }
        else
        {
            Debug.LogError("resistance is a negitive number");
        }
    }
}
