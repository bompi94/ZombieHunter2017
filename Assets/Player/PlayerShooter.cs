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

    int minimumNumberOfCasualBullets = 2;
    int maximumNumberOfCasualBullets = 7; 

    GameObject aim;
    EnemyShooter nearEnemy;
    Animator rightArmAnim;
    Animator leftArmAnim;
    float leftAnimSpeed;
    float rightAnimSpeed;
    UIInterfaceProject UIInterface; 

    protected override void Awake()
    {
        base.Awake();
        aim = bullseye.transform.GetChild(0).gameObject;
        rightArmAnim = GetComponentsInChildren<Animator>()[0];
        leftArmAnim = GetComponentsInChildren<Animator>()[1];
        rightAnimSpeed = rightArmAnim.speed;
        leftAnimSpeed = leftArmAnim.speed;
        UIInterface = FindObjectOfType<UIInterfaceProject>();
    }

    private void Update()
    {
        if (Armed())
        {
            gun.transform.position = gunPos.transform.position;
            bullseye.transform.localPosition = gun.transform.localPosition + new Vector3(0, 1f, 0); 
        }

        if (Input.GetButtonDown("Fire1"))
        {
            ManageLeftClick(); 
        }

        if (Input.GetButtonDown("Fire2"))
        {
            ManageRightClick(); 
        }
    }

    bool Armed()
    {
        return gun != null;
    }

    void ManageLeftClick()
    {
        TimeManager.Instance.Impulse();
        if (Armed())
        {
            Shoot();
        }
        else
        {
            UnarmedLeftClick();
        }
    }

    void UnarmedLeftClick()
    {
        if (nearGun)
        {
            PickGun(nearGun);
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
        gun.Throw(aim.transform.position - gun.transform.position, throwSpeed);
        LeaveGun();
    }

    protected override void PickGun(Gun gun)
    {
        base.PickGun(gun);
        rightArmAnim.SetBool("Gun", true);
        gun.SetNumberOfBullets(Random.Range(minimumNumberOfCasualBullets, maximumNumberOfCasualBullets)); 
    }

    public override void LeaveGun()
    {
        base.LeaveGun();
        rightArmAnim.SetBool("Gun", false);
        bullseye.transform.localPosition = new Vector3(0, 1.5f, 0);
    }

    public override void NoBullets()
    {
        UIInterface.NoBullets(); 
    }

    void Punch()
    {
        leftArmAnim.SetTrigger("Punch");
        rightArmAnim.SetTrigger("Punch");
        nearEnemy.HitByAPunch(nearEnemy.transform.position - transform.position); 
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
