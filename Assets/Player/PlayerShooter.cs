using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : Shooter
{

    [SerializeField]
    GameObject bullseye;

    [SerializeField]
    float throwSpeed;

    int minimumNumberOfCasualBullets = 2;
    int maximumNumberOfCasualBullets = 5;

    GameObject aim;
    EnemyShooter nearEnemy;
    UIInterfaceProject UIInterface;

    protected override void Awake()
    {
        base.Awake();
        aim = bullseye.transform.GetChild(0).gameObject;
        UIInterface = FindObjectOfType<UIInterfaceProject>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ManageLeftClick();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            ManageRightClick();
        }
    }

    void ManageLeftClick()
    {
        TimeManager.Instance.Impulse();
        if (Armed())
        {
            UseWeapon();
        }
        else
        {
            UnarmedLeftClick();
        }
    }

    void UnarmedLeftClick()
    {
        if (nearWeapon)
        {
            PickWeapon(nearWeapon);
        }
        else if (nearEnemy)
        {
            Punch();
        }
    }

    void ManageRightClick()
    {
        TimeManager.Instance.Impulse();
        if (Armed())
        {
            ThrowGun();
        }
    }

    void ThrowGun()
    {
        weapon.Throw(aim.transform.position - transform.position, throwSpeed);
        LeaveWeapon();
    }

    public override void PickWeapon(Weapon weapon)
    {
        base.PickWeapon(weapon);

        if (weapon.GetWType() == WeaponType.Gun)
        {
            cooldownTime = 0.5f; 
            ((Gun)weapon).SetNumberOfBullets(Random.Range(minimumNumberOfCasualBullets, maximumNumberOfCasualBullets));
        }

        if (weapon.GetWType() == WeaponType.Bat)
        {
            cooldownTime = 0.0f;
        }
    }

    public override void LeaveWeapon()
    {
        base.LeaveWeapon();
        bullseye.transform.localPosition = new Vector3(0, 1.5f, 0);
    }

    public override void NoBullets()
    {
        UIInterface.NoBullets();
    }

    void Punch()
    {
        nearEnemy.HitByAPunch(nearEnemy.transform.position - transform.position);
    }

    protected override void TimedUpdate()
    {
        base.TimedUpdate();

        if (weapon != null)
        {
            weapon.SetRotation(MouseRotation());
        }
        transform.rotation = Quaternion.Euler(0, 0, MouseRotation());
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
