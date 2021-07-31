using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapPath : MonoBehaviour
{

    [SerializeField] Sprite[] mapPathSprites;


    private Image pathImage;

    //managers
    private StoryManager storyManager;
    private Map map;

    // Start is called before the first frame update
    void Start()
    {
        storyManager = FindObjectOfType<StoryManager>();
        map = FindObjectOfType<Map>();
        pathImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMapPath();
    }

    private void UpdateMapPath()
    {        
        if(map.stageProgress - (5 * map.mapProgress) == 0)
        {
            pathImage.sprite = null;
            pathImage.color = new Color(1f, 1f, 1f, 0f);
        }
        if (map.stageProgress - (5 * map.mapProgress) >= 1)
        {
            pathImage.sprite = mapPathSprites[map.stageProgress - (5 * map.mapProgress) - 1];
            pathImage.color = new Color(1f, 1f, 1f, 1f);
        }

    }
}
