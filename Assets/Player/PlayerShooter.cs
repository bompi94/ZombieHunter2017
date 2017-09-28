using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : Shooter
{

    [SerializeField]
    GameObject bullseye;

    [SerializeField]
    GameObject gunPos; 

    [SerializeField]
    float throwSpeed;

    GameObject aim;

    EnemyShooter nearEnemy;


    Animator rightArmAnim;
    Animator leftArmAnim;

    float leftAnimSpeed;
    float rightAnimSpeed;

    protected override void Awake()
    {
        base.Awake();
        aim = bullseye.transform.GetChild(0).gameObject;
        rightArmAnim = GetComponentsInChildren<Animator>()[0];
        leftArmAnim = GetComponentsInChildren<Animator>()[1];
        rightAnimSpeed = rightArmAnim.speed;
        leftAnimSpeed = leftArmAnim.speed;
    }

    private void Update()
    {
        if (gun != null)
        {
            gun.transform.position = gunPos.transform.position;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            TimeManager.Instance.Impulse();

            if (gun != null)
            {
                gun.transform.position = gunPos.transform.position;
                Shoot();
            }

            else
            {
                ManageClick();
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            TimeManager.Instance.Impulse();

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
            Punch();
        }
        else if (nearGun)
        {
            PickGun(nearGun);
        }
    }

    protected override void PickGun(Gun gun)
    {
        base.PickGun(gun);
        rightArmAnim.SetBool("Gun", true);
    }

    public override void LeaveGun()
    {
        base.LeaveGun();
        rightArmAnim.SetBool("Gun", false);
    }

    void Punch()
    {
        leftArmAnim.SetTrigger("Punch");
        rightArmAnim.SetTrigger("Punch");
        nearEnemy.HitByAPunch(); 
    }

    protected override void TimedUpdate()
    {
        base.TimedUpdate();

        if (gun != null)
        {
            gun.SetRotation(MouseRotation());
        }
        transform.rotation = Quaternion.Euler(0, 0, MouseRotation());
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
