using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : Shooter
{

    [SerializeField]
    GameObject bullseye;

    [SerializeField]
    GameObject rightHand;
    [SerializeField]
    GameObject leftHand;

    Vector3 rightHandPos, leftHandPos; 

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
        rightHandPos = rightHand.transform.localPosition;
        leftHandPos = leftHand.transform.localPosition; 
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

    protected override void UseWeapon()
    {
        if (weapon.GetWType() == WeaponType.Bat)
        {
            SetHandsPos(-weapon.transform.localPosition);
        }
        base.UseWeapon();
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
            SetHandsPos(gunPos.transform.localPosition);
        }

        if (weapon.GetWType() == WeaponType.Bat)
        {
            cooldownTime = 0.0f;
            SetHandsPos(batPos.transform.localPosition);
        }
    }

    public override void LeaveWeapon()
    {
        base.LeaveWeapon();
        bullseye.transform.localPosition = new Vector3(0, 1.5f, 0);
        SetHandsPos(rightHandPos, leftHandPos); 
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

    void SetHandsPos(Vector3 rightPos)
    {
        SetHandsPos(rightPos, rightPos); 
    }

    void SetHandsPos(Vector3 rightPos, Vector3 leftPos)
    {
        rightHand.transform.localPosition = rightPos;
        leftHand.transform.localPosition = leftPos;
    }
    
}
