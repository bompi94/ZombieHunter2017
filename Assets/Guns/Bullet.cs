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

    Faction faction;

    Vector3 dir;

    bool init = false;

    public void Fire(Vector2 direction, ObjectPooler pooler, int damages, Faction faction)
    {
        dir = direction * bulletSpeed;
        myPooler = pooler;
        this.damages = damages;
        this.faction = faction;
        init = true; 
    }

    private void Update()
    {
        if (init)
            transform.position += dir * Time.deltaTime;
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
