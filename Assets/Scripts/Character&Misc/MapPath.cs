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
        pathImage.sprite = mapPathSprites[map.stageProgress - (5 * map.mapProgress)];      
    }
}
