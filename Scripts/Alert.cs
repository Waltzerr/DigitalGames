using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour
{
    public List<GameObject> alerts;
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
    }

    public void show(int alertID)
    {
        foreach (GameObject alert in alerts)
        {
            alert.SetActive(false);
        }
        gameObject.SetActive(true);
        alerts[alertID].SetActive(true);
    }

    public void hide()
    {
        foreach(GameObject alert in alerts)
        {
            alert.SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
