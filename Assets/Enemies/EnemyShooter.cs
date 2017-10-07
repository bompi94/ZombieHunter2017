using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : Shooter
{

    GameObject player;

    float reflexesTime = 0.5f;
    float reflexesTimer;

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

        if (HasGun())
        {
            reflexesTimer += TimeManager.deltaTime;
            Aim();
            if (canShoot && player && reflexesTimer>=reflexesTime)
            {
                Shoot();
            }
        }
    }

    protected override void Shoot()
    {
        if (PlayerInSight())
        {
            AimWithError();
            canShoot = false;
            gun.Shoot();
        }
    }

    void Aim()
    {
        float angle = GetToPlayerRotationAngle();
        gun.SetRotation(angle);
    }

    void AimWithError()
    {
        float angle = GetToPlayerRotationAngle();
        float aimError = Random.Range(-10f, 10f);
        gun.SetRotation(angle + aimError);
    }

    bool PlayerInSight()
    {
        RaycastHit2D[] ray = Physics2D.RaycastAll(transform.position, Quaternion.Euler(0, 0, GetToPlayerRotationAngle()) * Vector2.up, 10000);
        for (int i = 0; i < ray.Length; i++)
        {
            if (ray[i].transform.gameObject.GetComponent<PlayerMovement>())
                return true;
            if (ray[i].transform.gameObject.name.StartsWith("wall") ||
                (ray[i].transform.gameObject.GetComponent<EnemyShooter>() && ray[i].transform.gameObject.GetComponent<EnemyShooter>() != this))
                return false;
        }
        return false;
    }


    float GetToPlayerRotationAngle()
    {
        float angle = 0;

        if (player)
        {
            Vector3 v = transform.position - player.transform.position;
            angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg + 90;
        }
        return angle;
    }

    public override void LeaveGun()
    {
        reflexesTimer = 0; 
        base.LeaveGun();
    }

    public void HitByAPunch(Vector3 punchDirection)
    {
        LeaveGun();
        GetComponent<EnemyMovement>().Confused();
        reflexesTimer = 0;
        body.AddForce(punchDirection.normalized, ForceMode2D.Impulse);
    }
}
