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
    protected bool throwed;
    protected Vector3 throwdir = Vector3.zero;


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
        return !pickedUp && !throwed;
    }

    public void Throw(Vector3 dir, float throwSpeed)
    {
        this.throwdir = dir;
        this.throwSpeed = throwSpeed;
        throwed = true;
    }
}
