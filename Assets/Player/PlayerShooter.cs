using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : Shooter
{

    [SerializeField]
    GameObject bullseye;

    [SerializeField]
    float throwSpeed;

    GameObject aim;

    EnemyShooter nearEnemy;

    protected override void Awake()
    {
        base.Awake();
        aim = bullseye.transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (gun != null)
            {
                Shoot();
            }

            else
            {
                ManageClick();
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (gun != null)
            {
                ThrowGun();
            }
        }
    }

    void ThrowGun()
    {
        gun.Throw(aim.transform.position - transform.position, throwSpeed);
        LeaveGun();
    }

    void ManageClick()
    {

        if (nearEnemy)
        {
            PickGun(nearEnemy.StealGun());
        }
        else if (nearGun)
            PickGun(nearGun);
    }

    protected override void TimedUpdate()
    {
        base.TimedUpdate();
        if (gun != null)
        {
            gun.SetRotation(MouseRotation());
        }
        bullseye.transform.rotation = Quaternion.Euler(0, 0, MouseRotation() + 90);
    }

    float StickRotation()
    {
        float angle = 0;

        float x = Input.GetAxis("YHorizontal");
        float y = Input.GetAxis("YVertical");

        if (Mathf.Abs(x) >= 0.5 || Mathf.Abs(y) >= 0.5)
        {
            angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg + 90;
            angle *= -1;
        }
        return angle;
    }

    float MouseRotation()
    {
        float angle = 0;
        Vector3 mousePositionInWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 v = -mousePositionInWorldPoint + transform.position;

        float x = v.x;
        float y = v.y;

        if (Mathf.Abs(x) >= 0.5 || Mathf.Abs(y) >= 0.5)
        {
            angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg + 90;
        }
        return angle;
    }

    public void SetNearEnemy(EnemyShooter es)
    {
        nearEnemy = es;
    }
}
