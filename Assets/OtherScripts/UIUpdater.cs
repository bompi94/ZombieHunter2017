using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdater : MonoBehaviour
{

    [SerializeField]
    GameObject pausePanel;

    [SerializeField]
    GameObject pickItPanel;
    float showTime = 0.7f;
    float timer;
    bool showing;

    bool paused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }

        if(showing)
        {
            timer += Time.deltaTime; 
            if(timer>=showTime)
            {
                pickItPanel.SetActive(false);
                timer = 0;
                showing = false; 
            }
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

    public void ShowPickPanel()
    {
        pickItPanel.SetActive(true);
        showing = true; 
    }
}
