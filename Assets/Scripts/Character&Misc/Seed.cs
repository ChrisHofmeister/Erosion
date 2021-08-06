using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private ResourceManager resourceManager;

    // Start is called before the first frame update
    void Start()
    {
        resourceManager = FindObjectOfType<ResourceManager>();
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
            if (resourceManager)
            {
                resourceManager.AddResource("OR", "C", 1);
            }
        }
        if (waterNeighbors == 3)
        {
            anim.Play("Grow2");
            if (resourceManager)
            {
                resourceManager.AddResource("OR", "U", 1);
            }
        }
        if (waterNeighbors >= 4)
        {
            anim.Play("Grow3");
            if (resourceManager)
            {
                resourceManager.AddResource("OR", "R", 1);
            }
        }
    }
}
