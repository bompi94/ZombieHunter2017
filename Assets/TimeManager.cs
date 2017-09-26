using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    [SerializeField]
    float slowTimeScale;

    [SerializeField]
    float fastTimeScale;

    private void Awake()
    {
        Time.timeScale = slowTimeScale; 
    }

    public void SlowTime()
    {
        Time.timeScale = slowTimeScale; 
    }

    public void FastTime()
    {
        Time.timeScale = fastTimeScale; 
    }
}
