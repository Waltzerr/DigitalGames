using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject panel;
    public Image musicToggle;
    public Image soundToggle;
    public GameObject tutorialScreen;
    private void Start()
    {
        PlayerPrefs.SetInt("music", 1);
        if(tutorialScreen != null)
        {
            tutorialScreen.SetActive(false);
        }
    }
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

    public void exitGame()
    {
        Application.Quit();
    }

    public void openTutorial()
    {
        tutorialScreen.SetActive(true);
    }
    public void toggleSounds()
    {
        if(AudioListener.volume != 0f)
        {
            AudioListener.volume = 0f;
            soundToggle.gameObject.SetActive(true);
        } else
        {
            AudioListener.volume = 1;
            soundToggle.gameObject.SetActive(false);
        }
    }

    public void toggleMusic()
    {
        if(PlayerPrefs.GetInt("music") == 1)
        {
            PlayerPrefs.SetInt("music", 0);
            musicToggle.gameObject.SetActive(true);
        } else
        {
            PlayerPrefs.SetInt("music", 1);
            musicToggle.gameObject.SetActive(false);
        }
    }
}
