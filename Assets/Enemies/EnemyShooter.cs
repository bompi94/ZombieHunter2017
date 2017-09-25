using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : Shooter
{
    Vector3 nearestGunPosition = Vector3.zero;
    EnemyMovement movementEngine;
    GameObject player;

    protected override void Awake()
    {
        movementEngine = GetComponent<EnemyMovement>();
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
            movementEngine.SetDestination(transform.position);
        }
    }

    void GoToNearestGun(Gun[] guns)
    {
        bool thereIsAGunPickable = false;

        for (int i = 0; i < guns.Length; i++)
        {
            if (guns[i].CanBePicked())
            {
                if (!thereIsAGunPickable)
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
            movementEngine.SetDestination(nearestGunPosition);
        }
        else
        {
            movementEngine.SetDestination(transform.position);
        }
    }

    void ShootPlayer()
    {
        movementEngine.SetDestination(player.transform.position);
        gun.SetRotation(GetToPlayerRotation());
        gun.Shoot(myFaction);
    }

    float GetToPlayerRotation()
    {
        float angle = 0;
        Vector3 v = transform.position - player.transform.position;
        angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg + 90;
        return angle;
    }
}
