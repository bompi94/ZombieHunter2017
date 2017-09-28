using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    float speed;

    float actualSpeed;

    Vector3 movement = Vector3.zero;

    [SerializeField]
    float rollSpeed;

    [SerializeField]
    float rollTime;

    float rollTimer = 0;

    bool rolling = false;

    TimeManager timeManager; 

    private void Awake()
    {
        actualSpeed = speed;
        timeManager = FindObjectOfType<TimeManager>();
        timeManager.tick.AddListener(TimedUpdate); 
    }

    private void Update()
    {
        float horizMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        movement.x = horizMovement;
        movement.y = vertMovement;

        if (horizMovement != 0 || vertMovement != 0)
        {
            timeManager.FastTime();
        }

        else
        {
            timeManager.SlowTime();
        }
    }

    void TimedUpdate()
    {
        transform.position += movement * actualSpeed * TimeManager.deltaTime;
    }

    void DoRoll()
    {
        rolling = true;
        actualSpeed = rollSpeed;
        gameObject.layer = LayerMask.NameToLayer("Bullets");
    }

    void EndRoll()
    {
        rolling = false;
        actualSpeed = speed;
        rollTimer = 0;
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

}
