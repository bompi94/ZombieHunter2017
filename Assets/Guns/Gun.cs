using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    protected GameObject bullet;

    [SerializeField]
    protected Transform shootPos;

    [SerializeField]
    protected int bullets;
    protected int actualBullets;

    [SerializeField]
    protected int damages;

    protected ObjectPooler bulletPooler;
    bool pickedUp = false;
    float throwSpeed;
    bool throwed;
    Vector3 dir = Vector3.zero;
    Shooter shooter;

    private void Awake()
    {
        bulletPooler = FindObjectOfType<ObjectPooler>();
        actualBullets = bullets;
        TimeManager.Instance.tick.AddListener(TimedUpdate);
    }

    void TimedUpdate()
    {
        if (throwed)
        {
            transform.position += dir * throwSpeed * TimeManager.deltaTime;
        }
    }

    public void SetRotation(float angle)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public virtual bool Shoot()
    {
        if (actualBullets == 0)
        {
            NoBullets();
            return false;
        }

        else
        {
            GameObject b = bulletPooler.GetObject();
            b.transform.position = shootPos.position;
            Vector3 dir = shootPos.position - transform.position;
            b.GetComponent<Bullet>().Fire(dir, bulletPooler, damages);
            actualBullets--;
            return true;
        }
    }

    void NoBullets()
    {
        shooter.NoBullets();
    }

    protected void GunBreak()
    {
        if (throwed)
        {
            GetComponent<Explosion>().Explode();
        }
        Destroy(gameObject);
    }

    public bool IsFull()
    {
        return actualBullets == bullets;
    }

    public void PickedUp(Shooter shooter)
    {
        pickedUp = true;
        this.shooter = shooter;
        GetComponent<Collider2D>().isTrigger = false;
    }

    public void Leave()
    {
        pickedUp = false;
        shooter = null;
        if (!throwed)
            GetComponent<Collider2D>().isTrigger = true;
    }

    public bool CanBePicked()
    {
        return !pickedUp;
    }

    public void Reload()
    {
        actualBullets = bullets;
    }

    public int GetNumberOfBullets()
    {
        return actualBullets;
    }

    public void Throw(Vector3 dir, float throwSpeed)
    {
        this.dir = dir;
        this.throwSpeed = throwSpeed;
        throwed = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("coll");
        EnemyShooter es = collision.gameObject.GetComponent<EnemyShooter>();
        if (throwed && (es || collision.gameObject.name.StartsWith("wall")))
        {
            if (es)
            {
                es.HitByAPunch(dir);
            }
            GunBreak();
        }
    }

    public void SetNumberOfBullets(int numberOfBullets)
    {
        actualBullets = numberOfBullets;
    }
}
