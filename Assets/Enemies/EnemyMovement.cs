using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField]
    float speed;

    GameObject player;

    Vector3 destination; 

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject; 
    }

	void FixedUpdate () {
		transform.position += (destination - transform.position).normalized * speed * Time.fixedDeltaTime; 
	}

    void Die()
    {
        Destroy(gameObject);
    }

    public void SetDestination(Vector3 v)
    {
        destination = v; 
    }
}
