using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosMovement : MonoBehaviour {

    [SerializeField]
    float speed;

    Vector3 movement = new Vector3(0, 1, 0); 

    private void Awake()
    {
        float r = Random.Range(-1, 1f);
        float r1 = Random.Range(-1, 1f);

        movement = new Vector3(r, r1, 0);
        movement = movement.normalized; 
    }

    private void FixedUpdate()
    {
        transform.position += movement * speed * Time.fixedDeltaTime; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.StartsWith("wall"))
        {
            movement = Quaternion.Euler(0, 0, Random.Range(0, 45f)) * -movement;
        }
    }
}
