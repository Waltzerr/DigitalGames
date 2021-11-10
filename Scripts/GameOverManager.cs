using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    
    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ReplayButton()
    {
        SceneManager.LoadScene("Game");
    }
}
