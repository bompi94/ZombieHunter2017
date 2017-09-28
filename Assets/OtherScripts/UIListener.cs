using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIListener : MonoBehaviour {

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }
    }

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
