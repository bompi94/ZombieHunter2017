using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : Shooter
{
    Vector3 nearestGunPosition = Vector3.zero;
    EnemyMovement movement;
    GameObject player;

    protected override void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        player = FindObjectOfType<PlayerMovement>().gameObject;
        myFaction = Faction.Bad;
        base.Awake();
    }

    private void Update()
    {
        if (nearGun != null && nearGun.CanBePicked() && !gun)
            PickGun(nearGun);

        if (!gun)
        {
            SeekGun();
        }

        else
        {
            ShootPlayer();
        }
    }

    void SeekGun()
    {
        Gun[] guns = FindObjectsOfType<Gun>();

        if (guns.Length >= 0)
        {
            GoToNearestGun(guns); 
        }
        else
        {
            movement.SetDestination(transform.position);
        }
    }

    void GoToNearestGun(Gun[] guns)
    {
        bool thereIsAGunPickable = false;

        for (int i = 0; i < guns.Length; i++)
        {
            if (guns[i].CanBePicked() )
            {
                if(!thereIsAGunPickable)
                {
                    nearestGunPosition = guns[i].transform.position;
                }
                Vector3 vv = guns[i].transform.position;
                if ((transform.position - vv).magnitude < (transform.position - nearestGunPosition).magnitude)
                {
                    nearestGunPosition = vv;
                }
                thereIsAGunPickable = true;
            }
        }
        if (thereIsAGunPickable)
        {
            movement.SetDestination(nearestGunPosition);
        }
        else
        {
            movement.SetDestination(transform.position);
        }
    }

    void ShootPlayer()
    {
        movement.SetDestination(player.transform.position);
        gun.SetRotation(GetToPlayerRotation());
        gun.Shoot(myFaction);
    }

    float GetToPlayerRotation()
    {
        float angle = 0;

        Vector3 v = -player.transform.position + transform.position;

        float x = v.x;
        float y = v.y;

        if (Mathf.Abs(x) >= 0.5 || Mathf.Abs(y) >= 0.5)
        {
            angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg + 90;
        }
        return angle;
    }
}
