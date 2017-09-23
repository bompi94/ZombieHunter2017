using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    float recoil;

    [SerializeField]
    Transform shootPos;

    [SerializeField]
    float coolDown = 1;
    float coolDownTimer; 

    bool canShoot = true; 

    ObjectPooler bulletPooler;

    bool pickedUp = false;

    bool canBePickedUp = true; 

    private void Awake()
    {
        GameObject go = new GameObject();
        go.transform.SetParent(gameObject.transform);
        go.AddComponent<ObjectPooler>();
        go.GetComponent<ObjectPooler>().SetUp(bullet, 10);
        bulletPooler = go.GetComponent<ObjectPooler>();   
    }

    private void Update()
    {
        if(!canShoot)
        {
            coolDownTimer += Time.deltaTime; 
            if(coolDownTimer>=coolDown)
            {
                canShoot = true;
                coolDownTimer = 0; 
            }
        }
    }

    public void SetRotation(float angle)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public bool Shoot()
    {
        if (canShoot)
        {
            GameObject b = bulletPooler.GetObject();
            b.transform.position = shootPos.position;
            Vector3 dir = shootPos.position - transform.position;
            b.GetComponent<Bullet>().Fire(dir, bulletPooler);
            canShoot = false;
            return true; 
        }
        return false; 
    }

    public Vector3 GetRecoilVector()
    {
        return (transform.position - shootPos.position ) * recoil; 
    }

    public void PickedUp()
    {
        pickedUp = true;
    }

    public void Leaved()
    {
        pickedUp = false;      
    }

    public bool CanBePicked()
    {
        return !pickedUp; 
    }
}
