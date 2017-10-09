using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType
{
    Gun, Bat
}

public class Weapon : MonoBehaviour
{
    protected WeaponType type;
    protected Shooter shooter;
    protected bool pickedUp = false;
    protected float throwSpeed;
    protected bool throwed = false;
    protected Vector3 throwdir = Vector3.zero;

    protected virtual void Awake()
    {
        TimeManager.Instance.tick.AddListener(TimedUpdate);
    }

    protected virtual void TimedUpdate()
    {
        if (throwed)
        {
            transform.position += throwdir * throwSpeed * TimeManager.deltaTime;
        }
    }

    public void SetRotation(float angle)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public virtual void Use()
    {
        Debug.LogError("GENERIC WEAPON TRYING TO BE USED"); 
    }

    public WeaponType GetWType()
    {
        return type; 
    }

    protected void GunBreak()
    {
        if (throwed)
        {
            GetComponent<Explosion>().Explode();
        }
        Destroy(gameObject);
    }

    public virtual void PickedUp(Shooter shooter)
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

    protected virtual void OnCollisionEnter2D(Collision2D collision)
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

    public bool CanBePicked()
    {
        return !pickedUp && !throwed;
    }

    public void Throw(Vector3 dir, float throwSpeed)
    {
        transform.position = shooter.transform.position; 
        this.throwdir = dir;
        this.throwSpeed = throwSpeed;
        throwed = true;
    }
}
