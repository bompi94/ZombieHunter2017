using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
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
            transform.position += throwdir * throwSpeed * TimeManager.deltaTime;
        }
    }

    public override void Use()
    {
        Shoot(); 
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

    public bool IsFull()
    {
        return actualBullets == bullets;
    }

    public void Reload()
    {
        actualBullets = bullets;
    }

    public int GetNumberOfBullets()
    {
        return actualBullets;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyShooter es = collision.gameObject.GetComponent<EnemyShooter>();
        if (throwed && (es || collision.gameObject.name.StartsWith("wall")))
        {
            if (es)
            {
                es.HitByAPunch(throwdir);
            }
            GunBreak();
        }
    }

    public void SetNumberOfBullets(int numberOfBullets)
    {
        actualBullets = numberOfBullets;
    }
}
