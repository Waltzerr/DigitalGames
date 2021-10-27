using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public GameManager gameManager;
    public SpriteRenderer sprite;
    public List<AudioClip> sfx;
    public List<Sprite> frames1;
    public List<Sprite> frames2;
    public List<Sprite> frames3;

    public void Init()
    {
        gameManager = FindObjectOfType<GameManager>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void playSound(int soundID, float volume)
    {
        AudioSource.PlayClipAtPoint(sfx[soundID], Camera.main.transform.position, volume);
    }

    public void playSound(int soundID)
    {
        playSound(soundID, 1f);
    }
}
