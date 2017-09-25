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

    private void Awake()
    {
        actualSpeed = speed; 
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !rolling)
        {
            DoRoll();
        }

        else if(rolling)
        {
            rollTimer += Time.deltaTime; 
            if(rollTimer>rollTime)
            {
                EndRoll(); 
            }
        }

        else
        {
            float horizMovement = Input.GetAxis("Horizontal");
            float vertMovement = Input.GetAxis("Vertical");
            movement.x = horizMovement;
            movement.y = vertMovement;
        }
    }

    private void FixedUpdate()
    {
        transform.position += movement * actualSpeed * Time.fixedDeltaTime;
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
