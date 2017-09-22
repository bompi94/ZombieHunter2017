using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Bullet>())
        {
            Die(); 
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
