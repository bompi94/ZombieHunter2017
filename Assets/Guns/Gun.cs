using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    protected GameObject bullet;

    [SerializeField]
    protected Transform shootPos;

    [SerializeField]
    protected int bullets;
    protected int actualBullets;

    [SerializeField]
    protected int damages;

    protected ObjectPooler bulletPooler;
    bool pickedUp = false;
    float throwSpeed;
    bool throwed; 
    Vector3 dir = Vector3.zero; 

    private void Awake()
    {
        bulletPooler = FindObjectOfType<ObjectPooler>();
        actualBullets = bullets;
        TimeManager.Instance.tick.AddListener(TimedUpdate); 
    }

    void TimedUpdate()
    {
        if(throwed)
        {
            transform.position += dir * throwSpeed * TimeManager.deltaTime; 
        }
    }

    public void SetRotation(float angle)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public virtual bool Shoot()
    {
        if (actualBullets == 0)
        {
            NoBullets(); 
            return false;
        }

        else
        {
            GameObject b = bulletPooler.GetObject();
            b.transform.position = shootPos.position;
            Vector3 dir = shootPos.position - transform.position;
            b.GetComponent<Bullet>().Fire(dir, bulletPooler, damages);
            actualBullets--;
            return true;
        }
    }

    void NoBullets()
    {
        print("no bullets");
    }

    protected void GunBreak()
    {
        Destroy(gameObject);
    }

    public bool IsFull()
    {
        return actualBullets == bullets;
    }

    public void PickedUp()
    {
        pickedUp = true;
    }

    public void Leave()
    {
        pickedUp = false;
    }

    public bool CanBePicked()
    {
        return !pickedUp;
    }

    public void Reload()
    {
        actualBullets = bullets;
    }

    public int GetNumberOfBullets()
    {
        return actualBullets;
    }

    public void Throw(Vector3 dir, float throwSpeed)
    {
        this.dir = dir;
        this.throwSpeed = throwSpeed;
        throwed = true; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyShooter es = collision.gameObject.GetComponent<EnemyShooter>();
        if (throwed && (es || collision.gameObject.name.StartsWith("wall")) )
        {
            throwed = false;
            if(es)
            {
                es.HitByAPunch(dir); 
            }
            print("throwed"); 
            GunBreak();
        } 
    }
}
