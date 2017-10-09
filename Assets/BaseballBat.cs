using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballBat : Weapon
{

    [SerializeField]
    float swingSpeed;
    bool swinging = false;

    [SerializeField]
    int sign = -1;

    Vector3 destination;

    float localX;

    protected override void Awake()
    {
        base.Awake(); 
        type = WeaponType.Bat;
    }

    protected override void TimedUpdate()
    {
        base.TimedUpdate(); 
        if (swinging)
        {
            Vector3 dir = (destination - transform.localPosition).normalized;
            Vector3 swingVector = dir * swingSpeed * TimeManager.deltaTime;
            transform.localPosition += swingVector;
            transform.localPosition = new Vector3(transform.localPosition.x, .5f, transform.localPosition.z); 

            if(Mathf.Abs(transform.localPosition.x - destination.x)<0.5)
            {
                transform.localPosition = destination;
                swinging = false; 
            }
        }
    }

    public override void Use()
    {
        Swing();
    }

    void Swing()
    {
        if (!swinging)
        {
            localX = transform.localPosition.x;
            destination = new Vector3(-localX, transform.localPosition.y,transform.localPosition.z);
            swinging = true;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision); 
        if(swinging && collision.gameObject.GetComponent<EnemyHealth>())
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1); 
        }
    }

    public override void PickedUp(Shooter shooter)
    {
        base.PickedUp(shooter);
    }

}
