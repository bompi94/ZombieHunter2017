using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    float speed;
    [SerializeField]
    float jumpStrength; 

    Vector3 movement = Vector3.zero;
    Vector3 jumpVector; 
    Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        jumpVector = new Vector3(0, jumpStrength); 
    }

    // Update is called once per frame
    void Update ()
    {
        float horizMovement = Input.GetAxis("Horizontal");
        movement.x = horizMovement; 

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
	}

    private void FixedUpdate()
    {
        transform.position += movement * speed * Time.fixedDeltaTime;
    }

    void Jump()
    {
        body.AddForce(jumpVector, ForceMode2D.Impulse); 
    }
}
