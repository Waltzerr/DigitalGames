using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject panel;

    public void PlayButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void SettingsButton()
    {
        panel.SetActive(true);
    }

    public void CloseSettings()
    {
        panel.SetActive(false);
    }
}
