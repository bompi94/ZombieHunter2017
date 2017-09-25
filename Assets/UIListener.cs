using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIListener : MonoBehaviour {

    public void LoadGame()
    {
        SceneManager.LoadScene("MainScene"); 
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void ResumeFromPause()
    {
        FindObjectOfType<UIUpdater>().Pause(); 
    }
}
