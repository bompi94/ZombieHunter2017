using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappearer : MonoBehaviour {

    float disappearTime = 5;
    float timer;
    SpriteRenderer spriteRenderer; 

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        TimeManager.Instance.tick.AddListener(TimedUpdate);
    }

    void TimedUpdate()
    {
        timer += TimeManager.deltaTime;
        float percentage = 1 - timer / disappearTime; 
        SetAlpha(percentage);
        if (percentage <= 0)
            Destroy(gameObject);  
    }

    void SetAlpha(float percentage)
    {
        Color c = spriteRenderer.color;
        c.a = percentage;
        spriteRenderer.color = c; 
    }
}
