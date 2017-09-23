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

    public void Fire(Vector2 direction, ObjectPooler pooler, int damages, Faction faction)
    {
        body.velocity = direction * bulletSpeed;
        myPooler = pooler;
        this.damages = damages;
        this.faction = faction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.GetComponent<Health>())
        {
            if (other.GetComponent<Shooter>().myFaction != faction)
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
        body.velocity = Vector3.zero;
        myPooler.GameObjectReturnsAvailable(gameObject);
    }
}
