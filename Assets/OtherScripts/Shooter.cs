using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    float cooldownTime;
    float cooldownTimer;

    protected bool canUseWeapon = true;
    protected Weapon weapon;
    protected Rigidbody2D body;
    protected Weapon nearWeapon;

    [HideInInspector]
    public UnityEvent bulletsChangedEvent = new UnityEvent();

    protected virtual void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        TimeManager.Instance.tick.AddListener(TimedUpdate);
    }

    public void SetNearWeapon(Weapon g)
    {
        nearWeapon = g;
    }

    public bool Armed()
    {
        return weapon != null;
    }

    protected virtual void TimedUpdate()
    {
        if (!canUseWeapon)
        {
            cooldownTimer += TimeManager.deltaTime;
            if (cooldownTimer >= cooldownTime)
            {
                canUseWeapon = true;
                cooldownTimer = 0;
            }
        }
    }

    protected virtual void Shoot()
    {
        if (canUseWeapon)
        {
            canUseWeapon = false;
            weapon.Use();
        }
    }

    public virtual void NoBullets()
    {

    }

    public virtual void PickWeapon(Weapon weapon)
    {
        if (weapon!=null && weapon.CanBePicked())
        {
            GameObject gunGameObject = weapon.gameObject;
            gunGameObject.transform.SetParent(transform);
            gunGameObject.transform.position = transform.position;
            this.weapon = weapon;
            weapon.PickedUp(this);
            bulletsChangedEvent.Invoke();
        }
    }

    public virtual void LeaveWeapon()
    {
        if (weapon)
        {
            GameObject gunGameObject = weapon.gameObject;
            gunGameObject.transform.SetParent(null);
            weapon.Leave();
            weapon = null;
            bulletsChangedEvent.Invoke();
        }
    }
        
    public int GetNumberOfBullets()
    {
        if (weapon.GetWType() == WeaponType.Gun)
            return ((Gun)weapon).GetNumberOfBullets();
        else
            return 0;
    }
    public bool HasGun()
    {
        return weapon != null;
    }
}
