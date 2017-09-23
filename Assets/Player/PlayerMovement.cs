using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    float speed;

    Vector3 movement = Vector3.zero;
    Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update ()
    {
        float horizMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical"); 
        movement.x = horizMovement;
        movement.y = vertMovement; 
	}

    private void FixedUpdate()
    {
        transform.position += movement * speed * Time.fixedDeltaTime;
    }

}
