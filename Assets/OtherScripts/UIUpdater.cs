using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdater : MonoBehaviour {

    [SerializeField]
    GameObject pausePanel;

    bool paused = false; 

    void Update ()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Pause(); 
        }
	}

    public void Pause()
    {
        paused = !paused;

        if (paused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }

        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
