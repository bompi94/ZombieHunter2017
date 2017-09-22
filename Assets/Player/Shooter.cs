using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

    [SerializeField]
    Transform shootPos;

    [SerializeField]
    ObjectPooler bulletPooler;

    Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetButtonDown("Fire1"))
        {
            Shoot(); 
        }
	}

    void Shoot()
    {
        GameObject b = bulletPooler.GetObject();
        b.transform.position = shootPos.position;
        Vector3 dir = shootPos.position - transform.position; 
        b.GetComponent<Bullet>().Fire(dir, bulletPooler);
    }
}
