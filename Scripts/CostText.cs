using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CostText : MonoBehaviour
{
    public TextMeshProUGUI costDisplay;
    // Start is called before the first frame update
    void Start()
    {
        changeText("");
    }
    
    public void changeText(string text)
    {
        costDisplay.text = text;
    }

}
