using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public List<GameObject> slides;
    private int currentSlide;
    public Button nextSlideButton;
    public Button backSlideButton;
    // Start is called before the first frame update
    void Start()
    {
        currentSlide = 0;
        changeSlide();
        backSlideButton.interactable = false;
    }

    public void nextSlide()
    {
        currentSlide++;
        backSlideButton.interactable = true;
        if (currentSlide >= slides.Count - 1)
        {
            nextSlideButton.GetComponentInChildren<Text>().text = "Finish";
            if(currentSlide == slides.Count)
            {
                gameObject.SetActive(false);
            }
        }
        changeSlide();
    }

    public void backSlide()
    {
        if (currentSlide > 1)
        {
            nextSlideButton.GetComponentInChildren<Text>().text = "Next >";
        }
        else
        {
            backSlideButton.interactable = false;
        }
        currentSlide--;
        changeSlide();
    }

    public void changeSlide()
    {
        Debug.Log(currentSlide);
        foreach(GameObject slide in slides)
        {
            if(slides.IndexOf(slide) != currentSlide)
            {
                slide.SetActive(false);
            } else
            {
                slide.SetActive(true);
            }
        }
    }
}
