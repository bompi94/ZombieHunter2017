using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    float speed;

    float actualSpeed;

    Vector3 movement = Vector3.zero;

    float horizMovement;
    float vertMovement;

    TimeManager timeManager; 

    private void Awake()
    {
        actualSpeed = speed;
        timeManager = FindObjectOfType<TimeManager>();
        timeManager.tick.AddListener(TimedUpdate); 
    }

    private void Update()
    {
        horizMovement = Input.GetAxis("Horizontal");
        vertMovement = Input.GetAxis("Vertical");
        movement.x = horizMovement;
        movement.y = vertMovement;
    }

    void TimedUpdate()
    {
        if (horizMovement != 0 || vertMovement != 0)
        {
            timeManager.FastTime();
        }

        else
        {
            timeManager.SlowTime();
        }
        transform.position += movement * actualSpeed * TimeManager.deltaTime;
    }

}
