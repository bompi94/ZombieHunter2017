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
    bool jumped = false; 

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

        if (Input.GetButtonDown("Jump") && !jumped)
        {
            Jump();
        }
	}

    private void FixedUpdate()
    {
        body.velocity = new Vector3(0, body.velocity.y);
        transform.position += movement * speed * Time.fixedDeltaTime;

    }

    void Jump()
    {
        jumped = true; 
        body.AddForce(jumpVector, ForceMode2D.Impulse); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "platform" && collision.contacts[0].normal.y>0)
        {
            jumped = false; 
        }
    }
}
