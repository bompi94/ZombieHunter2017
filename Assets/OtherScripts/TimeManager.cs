using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{

    [SerializeField]
    float slowTimeScale;

    [SerializeField]
    float fastTimeScale;

    [HideInInspector]
    public UnityEvent tick = new UnityEvent();

    float scale;
    float timer;

    const float normalFrameTime = 0.01666667f; //60fps
    const float normalFixedDeltaTime = 0.02f;

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
        scale = slowTimeScale;
        Time.fixedDeltaTime = normalFixedDeltaTime * scale;

    }

    public void FastTime()
    {
        scale = fastTimeScale;
        Time.fixedDeltaTime = normalFixedDeltaTime * scale;

    }

    public void Impulse()
    { 
        FastTime();
        Restart();
    }

    void Restart()
    {
        Stop();
        Work();
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
