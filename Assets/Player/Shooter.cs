using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

    [SerializeField]
    Gun gun; 

    Rigidbody2D body; 

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (gun != null)
        {
            gun.SetRotation(MouseRotation());
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }

        if(Input.GetButtonDown("Fire2"))
        {
            LeaveGun(); 
        }
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

    void Shoot()
    {
        gun.Shoot();
        ApplyRecoil();
    }

    void ApplyRecoil()
    {
        Vector3 recoil = gun.GetRecoilVector();
        transform.position += recoil; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Gun>())
            PickGun(collision.GetComponent<Gun>()); 
    }

    void PickGun(Gun gun)
    {
        print("pickup"); 
        GameObject gunGameObject = gun.gameObject;
        gunGameObject.transform.SetParent(transform); 
        gunGameObject.transform.position = transform.position;
        this.gun = gun;
        gun.PickedUp(); 
    }

    void LeaveGun()
    {
        if (gun)
        {
            print("leave");
            GameObject gunGameObject = gun.gameObject;
            gunGameObject.transform.SetParent(null);
            gun.Leaved();
            gun = null;
        }
    }
}
