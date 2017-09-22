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

    public void Fire(Vector2 direction, ObjectPooler pooler)
    {
        body.velocity = direction * bulletSpeed;
        myPooler = pooler;
    }

    private void OnBecameInvisible()
    {
        Dismiss();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.GetComponent<PlayerMovement>())
            Dismiss();
    }

    public void EnemyHit()
    {
        Dismiss(); 
    }

    void Dismiss()
    {
        myPooler.GameObjectReturnsAvailable(gameObject);
    }
}
