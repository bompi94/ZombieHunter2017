using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField]
    protected GameObject bullet;

    [SerializeField]
    protected float recoil;

    [SerializeField]
    protected Transform shootPos;

    [SerializeField]
    float coolDown = 1;

    [SerializeField]
   protected int bullets; 

    float coolDownTimer; 

    protected bool canShoot = true; 

    protected ObjectPooler bulletPooler;

    bool pickedUp = false;

    bool canBePickedUp = true; 

    private void Awake()
    {
        GameObject go = new GameObject();
        go.name = "gun pool"; 
        go.transform.SetParent(gameObject.transform);
        go.AddComponent<ObjectPooler>();
        go.GetComponent<ObjectPooler>().SetUp(bullet);
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

    public virtual bool Shoot()
    {
        if (canShoot)
        {
            GameObject b = bulletPooler.GetObject();
            b.transform.position = shootPos.position;
            Vector3 dir = shootPos.position - transform.position;
            b.GetComponent<Bullet>().Fire(dir, bulletPooler);
            canShoot = false;
            bullets--;
            if (bullets == 0)
            {
                GunBreak();
                return false;
            }
            return true; 
        }
        return false; 
    }

    protected void GunBreak()
    {
        Destroy(gameObject); 
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
