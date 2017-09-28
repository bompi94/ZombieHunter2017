﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    protected GameObject bullet;

    [SerializeField]
    protected float recoil;

    [SerializeField]
    protected Transform shootPos;

    [SerializeField]
    protected int bullets;
    protected int actualBullets;

    [SerializeField]
    protected int damages;

    protected bool canShoot = true;

    protected ObjectPooler bulletPooler;

    [SerializeField]
    bool pickedUp = false;

    float throwSpeed;
    bool throwed; 
    Vector3 dir = Vector3.zero; 

    private void Awake()
    {
        GameObject go = new GameObject();
        go.name = "gun pool";
        go.transform.SetParent(gameObject.transform);
        go.AddComponent<ObjectPooler>();
        go.GetComponent<ObjectPooler>().SetUp(bullet);
        bulletPooler = go.GetComponent<ObjectPooler>();
        actualBullets = bullets;
        TimeManager.Instance.tick.AddListener(TimedUpdate); 
    }

    void TimedUpdate()
    {
        if(throwed)
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
        GameObject b = bulletPooler.GetObject();
        b.transform.position = shootPos.position;
        Vector3 dir = shootPos.position - transform.position;
        b.GetComponent<Bullet>().Fire(dir, bulletPooler, damages);
        actualBullets--;
        if (actualBullets == 0)
        {
            print("no bullets"); 
            return false;
        }
        return true;
    }

    protected void GunBreak()
    {
        Destroy(gameObject);
    }

    public bool IsFull()
    {
        return actualBullets == bullets;
    }

    public Vector3 GetRecoilVector()
    {
        return (transform.position - shootPos.position) * recoil;
    }

    public void PickedUp()
    {
        pickedUp = true;
    }

    public void Leaved()
    {
        pickedUp = false;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyShooter es = collision.gameObject.GetComponent<EnemyShooter>();
        if (throwed && (es || collision.gameObject.name.StartsWith("wall")) )
        {
            throwed = false;
            if(es)
            {
                es.HitByAPunch(); 
            }
            print("throwed"); 
            GunBreak();
        } 
    }
}
