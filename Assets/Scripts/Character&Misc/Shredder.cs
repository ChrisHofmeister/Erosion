using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.GetComponent<EarthTile>() != null)
        {
            collision.GetComponent<EarthTile>().DestroySelf();
        }
    }

}
