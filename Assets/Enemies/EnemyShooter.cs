using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : Shooter
{
    GameObject player;

    float reflexesTime = 0.8f;
    float reflexesTimer;

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
    }

    protected override void UseWeapon()
    {
        AimWithError();
        canUseWeapon = false;
        weapon.Use();
    }

    void Aim()
    {
        float angle = GetToPlayerRotationAngle();
        weapon.SetRotation(angle);
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
}
