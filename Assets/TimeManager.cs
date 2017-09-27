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
        scale = slowTimeScale;
    }

    public void FastTime()
    {
        scale = fastTimeScale;
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
