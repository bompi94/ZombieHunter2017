using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField]
    float speed;

    GameObject player;

    private void Awake()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>().gameObject; 
    }

	void FixedUpdate () {
		transform.position += (player.transform.position - transform.position).normalized * speed * Time.fixedDeltaTime; 
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Bullet>())
        {
            collision.GetComponent<Bullet>().EnemyHit(); 
            Die(); 
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
