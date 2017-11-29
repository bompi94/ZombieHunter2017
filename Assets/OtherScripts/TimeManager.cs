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

    float timeScale;
    float timer;

    const float normalFrameTime = 0.01666667f; //60fps
    const float normalFixedDeltaTime = 0.02f;

    public static TimeManager Instance;

    public static float deltaTime;

    Coroutine clockCoroutine;

    private void Awake()
    {
        timeScale = slowTimeScale;
        Instance = this;
    }

    public void RegisterBehaviour(UnityEngine.Events.UnityAction method)
    {
        tick.AddListener(method);
    }

    public void SlowTime()
    {
        timeScale = slowTimeScale;
        Time.fixedDeltaTime = normalFixedDeltaTime * timeScale;
    }

    public void FastTime()
    {
        timeScale = fastTimeScale;
        Time.fixedDeltaTime = normalFixedDeltaTime * timeScale;
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
            deltaTime = normalFrameTime * timeScale;
            yield return new WaitForSeconds(deltaTime);
            tick.Invoke();
        }
    }

    public float GetScale()
    {
        return timeScale;
    }
}
