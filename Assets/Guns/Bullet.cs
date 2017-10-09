using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float bulletSpeed;

    ObjectPooler myPooler;
    int damages;
    Vector3 dir;
    bool init = false;
    LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        TimeManager.Instance.tick.AddListener(TimedUpdate);
    }

    public void Fire(Vector2 direction, ObjectPooler pooler, int damages)
    {
        dir = direction * bulletSpeed;
        myPooler = pooler;
        this.damages = damages;
        init = true;
    }

    private void TimedUpdate()
    {
        if (init)
        {
            transform.position += dir * TimeManager.deltaTime;
            DrawTrail();
        }
    }

    public void Deflect()
    {
        dir *= -1;
    }

    void DrawTrail()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position - dir.normalized);
    }

    void HideTrail()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        DealWithCollision(other);
    }

    void DealWithCollision(GameObject other)
    {
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
        HideTrail();
        myPooler.GameObjectReturnsAvailable(gameObject);
    }
}
