using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdater : MonoBehaviour {

    Health myHealth;
    Shooter myShooter; 

    [SerializeField]
    Text bulletsText;

    [SerializeField]
    Text healthText;

    [SerializeField]
    GameObject pausePanel;

    bool paused = false; 

    private void Awake()
    {
        myHealth = GetComponent<Health>();
        myShooter = GetComponent<Shooter>();
        myHealth.healthChanged.AddListener(UpdateHPText);
        myShooter.bulletsChangedEvent.AddListener(UpdateBulletsText); 
    }

    void UpdateHPText()
    {
        healthText.text = myHealth.GetHP().ToString();
    }

    void UpdateBulletsText()
    {
        bulletsText.text = myShooter.GetNumberOfBullets().ToString();
    }

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
