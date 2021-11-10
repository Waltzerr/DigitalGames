using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CostText : MonoBehaviour
{
    public Text costDisplay;
    public GameObject image;
    // Start is called before the first frame update
    void Start()
    {
        changeText("");
    }
    
    public void changeText(string text)
    {
        if(text == "")
        {
            image.SetActive(false);
        } else
        {
            image.SetActive(true);
        }
        text = text.Replace("/n", "\n");
        costDisplay.text = text;
    }

    public void uniqueUpgrade(string text)
    {
        string description;
        switch (GameManager.Instance.TowerUpgrade)
        {
            case 1:
                description = "Macrophages can consume more viruses before they get full.";
                break;
            case 2:
                description = "Antibodies are more effective at slowing viruses.";
                break;
            case 3:
                description = "Neutrophils stay exploded for longer.";
                break;
            default:
                description = "Cells give you extra DNA when they reach the end.";
                break;
        }
        changeText(text + $"/n{description}");
    }

}
