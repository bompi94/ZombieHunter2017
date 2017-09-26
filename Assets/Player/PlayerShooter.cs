using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : Shooter {

    [SerializeField]
    GameObject bullseye; 

    protected override void Awake()
    {
        TimeManager.Instance.tick.AddListener(TimedUpdate);
        base.Awake();  
    }

    private void Update()
    {
        if (gun != null)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (gun)
                LeaveGun();
            else
                PickGun(nearGun);
        }
    }

    void TimedUpdate()
    {
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
}
