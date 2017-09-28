using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : Shooter
{

    GameObject player;

    protected override void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        if (transform.GetChild(0) != null)
        {
            PickGun(transform.GetChild(0).GetComponent<Gun>());
        }
        base.Awake();
    }

    protected override void TimedUpdate()
    {
        base.TimedUpdate(); 
        if (gun)
        {
            gun.SetRotation(GetToPlayerRotation());
            Shoot();
        }
    }

    protected override void Shoot()
    {
        if (canShoot && player)
        {
            //should probably move somewhere
            if (PlayerInSight())
            {
                canShoot = false;
                gun.Shoot();
            }
        }
    }

    bool PlayerInSight()
    {
        RaycastHit2D[] ray = Physics2D.RaycastAll(transform.position, Quaternion.Euler(0, 0, GetToPlayerRotation()) * Vector2.up, 10000);
        for (int i = 0; i < ray.Length; i++)
        {
            if (ray[i].transform.gameObject.GetComponent<PlayerMovement>())
                return true;
            if (ray[i].transform.gameObject.name.StartsWith("wall"))
                return false;
        }
        return false;
    }


    float GetToPlayerRotation()
    {
        float angle = 0;

        if (player)
        {
            Vector3 v = transform.position - player.transform.position;
            angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg + 90;
        }
        return angle;
    }

    public bool HasGun()
    {
        return gun != null; 
    }

    public void HitByAPunch(Vector3 punchDirection)
    {
        LeaveGun();
        body.AddForce (punchDirection.normalized, ForceMode2D.Impulse);
    }
}
