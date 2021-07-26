using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{

    [SerializeField] public Sprite[] plantSprites;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePlantSprite(int waterNeighbors)
    {
        if(waterNeighbors <= 4)
        {
            spriteRenderer.sprite = plantSprites[waterNeighbors - 1];
        }
        else if(waterNeighbors >= 5)
        {
            spriteRenderer.sprite = plantSprites[3];
        }
    }
}
