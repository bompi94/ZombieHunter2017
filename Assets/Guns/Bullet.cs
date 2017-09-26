using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField]
    Rigidbody2D body;

    [SerializeField]
    float bulletSpeed;

    ObjectPooler myPooler;
    int damages;

    Vector3 dir;

    bool init = false;

    LineRenderer lr; 

    private void Awake()
    {
        lr = GetComponent<LineRenderer>(); 
    }

    public void Fire(Vector2 direction, ObjectPooler pooler, int damages)
    {
        dir = direction * bulletSpeed;
        myPooler = pooler;
        this.damages = damages;
        init = true; 
    }

    private void Update()
    {
        if (init)
        {
            transform.position += dir * Time.deltaTime;
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, transform.position - dir.normalized); 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.GetComponent<Health>())
        {
            other.GetComponent<Health>().TakeDamage(damages);
        }
        Dismiss();
    }

    public void EnemyHit()
    {
        Dismiss();
    }

    void Dismiss()
    {
        dir = Vector3.zero;
        init = false; 
        myPooler.GameObjectReturnsAvailable(gameObject);
    }
}
