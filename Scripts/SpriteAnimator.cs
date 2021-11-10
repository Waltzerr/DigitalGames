using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimator : MonoBehaviour
{
    public int framesPerChange;
    private int counter = 0;
    private int currentFrame = 0;
    public Sprite[] frames;
    private SpriteRenderer sr;
    private Image ir;

    private void Awake()
    {
        
        if(TryGetComponent<SpriteRenderer>(out SpriteRenderer Sr))
        {
            sr = Sr;
            ir = null;
        }
        else
        {
            sr = null;
            ir = GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(counter == framesPerChange - 1)
        {
            if(sr == null)
            {
                ir.sprite = frames[currentFrame];
            }
            else
            {
                sr.sprite = frames[currentFrame];
            }
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
