using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer =GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        spriteRenderer.enabled = false;
    }


    public void PlayGrowAnimation(int waterNeighbors)
    {
        spriteRenderer.enabled = true;

        if(waterNeighbors > 0 && waterNeighbors <= 2)
        {
            anim.Play("Grow1");
        }
        if (waterNeighbors == 3)
        {
            anim.Play("Grow2");
        }
        if (waterNeighbors >= 4)
        {
            anim.Play("Grow3");
        }
    }
}
