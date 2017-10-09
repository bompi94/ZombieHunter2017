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

    protected override void Awake()
    {
        base.Awake(); 
        bulletPooler = FindObjectOfType<ObjectPooler>();
        actualBullets = bullets;
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

    public void SetNumberOfBullets(int numberOfBullets)
    {
        actualBullets = numberOfBullets;
    }
}
