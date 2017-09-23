using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    protected Gun gun;
    protected Rigidbody2D body;
    protected Gun nearGun;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    protected void Shoot()
    {
        bool hasActuallyShot = gun.Shoot();
        if (hasActuallyShot)
            ApplyRecoil();
    }

    void ApplyRecoil()
    {
        Vector3 recoil = gun.GetRecoilVector();
        transform.position += recoil;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Gun g = (collision.GetComponent<Gun>());
        if (g)
        {
            if (g.CanBePicked())
            {
                print("near");
                nearGun = g;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Gun g = (collision.GetComponent<Gun>());
        if (g && g.Equals(nearGun))
        {
            print("not near");
            nearGun = null;
        }
    }

    public bool ReloadGun()
    {
        if(gun && !gun.IsFull())
        {
            gun.Reload();
            return true; 
        }
        return false;
    }

    protected void PickGun(Gun gun)
    {
        if (gun && gun.CanBePicked())
        {
            print("pickup");
            GameObject gunGameObject = gun.gameObject;
            gunGameObject.transform.SetParent(transform);
            gunGameObject.transform.position = transform.position;
            this.gun = gun;
            gun.PickedUp();
        }
    }

    protected void LeaveGun()
    {
        print("leave");
        GameObject gunGameObject = gun.gameObject;
        gunGameObject.transform.SetParent(null);
        gun.Leaved();
        gun = null;
    }
}
