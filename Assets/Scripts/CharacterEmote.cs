using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterEmote : MonoBehaviour
{
   
    [SerializeField] private Sprite[] emotionSprites;
        
    private Image characterImage;

    // Start is called before the first frame update
    void Start()
    {        
        characterImage = GetComponent<Image>();
        EmoteIdle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EmoteAngry()
    {
        characterImage.sprite = emotionSprites[1];
    }

    public void EmoteConfused()
    {
        characterImage.sprite = emotionSprites[2];
    }

    public void EmoteHappy()
    {
        characterImage.sprite = emotionSprites[3];
    }
    
    public void EmoteIdle()
    {
        characterImage.sprite = emotionSprites[0];
    }
    
    public void EmoteOK()
    {
        characterImage.sprite = emotionSprites[4];
    }

    public void EmoteQuestion()
    {
        characterImage.sprite = emotionSprites[5];
    }

    public void EmoteSad()
    {
        characterImage.sprite = emotionSprites[6];
    }

    public void EmoteSquint()
    {
        characterImage.sprite = emotionSprites[7];
    }

    public void EmoteSurprise()
    {
        characterImage.sprite = emotionSprites[8];
    }
    
}
