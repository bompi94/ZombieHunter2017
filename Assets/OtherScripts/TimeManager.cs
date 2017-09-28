﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{

    [SerializeField]
    float slowTimeScale;

    [SerializeField]
    float fastTimeScale;

    public UnityEvent tick = new UnityEvent();

    float scale;
    float timer;

    float normalFrameTime = 0.01666667f;

    public static TimeManager Instance;

    public static float deltaTime;

    Coroutine clockCoroutine;

    private void Awake()
    {
        scale = slowTimeScale;
        Instance = this;
    }

    public void SlowTime()
    {
        Time.fixedDeltaTime = deltaTime; 
        scale = slowTimeScale;
    }

    public void FastTime()
    {
        Time.fixedDeltaTime = deltaTime; 
        scale = fastTimeScale;
    }

    public void Impulse()
    { 
        FastTime();
        Restart();
    }

    public void Stop()
    {
        if (clockCoroutine!=null)
            StopCoroutine(clockCoroutine);
    }

    public void Work()
    {
        clockCoroutine = StartCoroutine(Clock());
    }

    void Restart()
    {
        Stop();
        Work(); 
    }

    IEnumerator Clock()
    {
        while (true)
        {
            //probably this can be done like 
            //waitfor(extremelysmalltime)
            //counter+=extremelysmalltime
            //if(counter>deltatime)
            //counter = 0
            //tick.invoke
            //this should adapt nicely to continuous time scale change
            deltaTime = normalFrameTime * scale;
            yield return new WaitForSeconds(deltaTime);
            tick.Invoke();
        }
    }

    public float GetScale()
    {
        return scale;
    }
}
