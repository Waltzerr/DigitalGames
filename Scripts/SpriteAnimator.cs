using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public int framesPerChange;
    private int counter = 0;
    private int currentFrame = 0;
    public Sprite[] frames;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(counter == framesPerChange - 1)
        {
            
            sr.sprite = frames[currentFrame];
            if (currentFrame == frames.Length - 1)
            {
                currentFrame = 0;
            } else
            {
                currentFrame++;
            }
            counter = 0;
        }
        else
        {
            counter++;
        }
    }
}
