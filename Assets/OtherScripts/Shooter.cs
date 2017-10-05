﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    float cooldownTime;
    float cooldownTimer;

    protected bool canShoot = true;
    protected Gun gun;
    protected Rigidbody2D body;
    protected Gun nearGun;

    [HideInInspector]
    public UnityEvent bulletsChangedEvent = new UnityEvent();

    protected virtual void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        TimeManager.Instance.tick.AddListener(TimedUpdate);
    }

    public void SetNearGun(Gun g)
    {
        nearGun = g;
    }

    protected virtual void TimedUpdate()
    {
        if (!canShoot)
        {
            cooldownTimer += TimeManager.deltaTime;
            if (cooldownTimer >= cooldownTime)
            {
                canShoot = true;
                cooldownTimer = 0;
            }
        }
    }

    protected virtual void Shoot()
    {
        if (canShoot)
        {
            canShoot = false;
            bool hasActuallyShot = gun.Shoot();
            if (hasActuallyShot)
                bulletsChangedEvent.Invoke();
        }
    }

    public virtual void NoBullets()
    {

    }

    protected virtual void PickGun(Gun gun)
    {
        if (gun && gun.CanBePicked())
        {
            GameObject gunGameObject = gun.gameObject;
            gunGameObject.transform.SetParent(transform);
            gunGameObject.transform.position = transform.position;
            this.gun = gun;
            gun.PickedUp(this);
            bulletsChangedEvent.Invoke();
        }
    }

    public virtual void LeaveGun()
    {
        if (gun)
        {
            GameObject gunGameObject = gun.gameObject;
            gunGameObject.transform.SetParent(null);
            gun.Leave();
            gun = null;
            bulletsChangedEvent.Invoke();
        }
    }
        
    public int GetNumberOfBullets()
    {
        if (gun)
            return gun.GetNumberOfBullets();
        else
            return 0;
    }
}
