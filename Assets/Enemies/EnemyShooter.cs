using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : Shooter
{
    GameObject player;

    float reflexesTime = 0.8f;
    float reflexesTimer;

    float closeTime = 0.5f;
    float closeTimer = 0; 

    protected override void Awake()
    {
        if (FindObjectOfType<PlayerMovement>())
            player = FindObjectOfType<PlayerMovement>().gameObject;

        if (transform.GetChild(0) != null)
        {
            PickWeapon(transform.GetChild(0).GetComponent<Weapon>());
        }
        base.Awake();
    }

    protected override void TimedUpdate()
    {
        base.TimedUpdate();

        if (HasWeapon())
        {
            TryToHitPlayer();
        }
    }

    private void TryToHitPlayer()
    {
        if (PlayerInSight())
        {
            reflexesTimer += TimeManager.deltaTime;
            Aim();
            if (canUseWeapon && player && reflexesTimer >= reflexesTime)
            {
                UseWeapon();
            }
        }

        else
        {
            reflexesTimer = 0;
        }
    }

    protected override void UseWeapon()
    {
        if (weapon.GetWType() == WeaponType.Gun)
        {
            AimWithError();
            base.UseWeapon();
        }

        if(weapon.GetWType() == WeaponType.Bat)
        {
            float d = Vector3.Distance(player.transform.position, transform.position);
            if (d < 4)
            {
                closeTimer += TimeManager.deltaTime; 
                if(closeTimer>=closeTime)
                {
                    base.UseWeapon();
                    closeTimer = 0; 
                }
            }

            else
            {
                closeTimer = 0; 
            }
        }

    }

    void Aim()
    {
        float angle = GetToPlayerRotationAngle();
        transform.rotation = Quaternion.Euler(0, 0, angle); 
    }

    void AimWithError()
    {
        float angle = GetToPlayerRotationAngle();
        float aimError = Random.Range(-10f, 10f);
        weapon.SetRotation(angle + aimError);
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

    public override void LeaveWeapon()
    {
        reflexesTimer = 0;
        base.LeaveWeapon();
    }

    public void HitByAPunch(Vector3 punchDirection)
    {
        LeaveWeapon();
        GetComponent<EnemyMovement>().Confused();
        body.AddForce(punchDirection.normalized, ForceMode2D.Impulse);
    }

    public override void PickWeapon(Weapon weapon)
    {
        base.PickWeapon(weapon);

        if (weapon)
        {
            if (weapon.GetWType() == WeaponType.Gun)
            {
                cooldownTime = 0.8f;
            }

            if (weapon.GetWType() == WeaponType.Bat)
            {
                cooldownTime = 0.8f;
            }
        }
    }
}
