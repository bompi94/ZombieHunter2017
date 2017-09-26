using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPiece : MonoBehaviour
{


    float intensity = .3f;
    Vector3 dir;
    bool init;

    private void Awake()
    {
        TimeManager.Instance.tick.AddListener(TimedUpdate); 
    }

    public void AddForce(Vector3 dir)
    {
        this.dir = dir;
        init = true;
    }

    private void TimedUpdate()
    {
        if (init)
        {
            intensity -= Time.deltaTime;
            if (intensity >= 0)
                transform.position += dir * intensity * Time.deltaTime;
        }
    }
}
