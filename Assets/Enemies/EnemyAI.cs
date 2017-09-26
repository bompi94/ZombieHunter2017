using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : Shooter
{

    FSM stateMachine = new FSM();
    GameObject player;
    Vector3 nearestGunPosition = Vector3.zero;
    EnemyMovement movementEngine;

    bool goingToGun;

    protected override void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        movementEngine = GetComponent<EnemyMovement>();
        myFaction = Faction.Bad;
        stateMachine.SetCurrentState(SeekGun);
        base.Awake();
    }

    private void Update()
    {
        stateMachine.Run();
    }

    //state
    void SeekGun()
    {
        if (ThereIsAGunPickable())
        {
            movementEngine.SetDestination(nearestGunPosition);
        }

        else
        {
            //transition
            stateMachine.SetCurrentState(Hide);
        }

        //transition
        if (!gun && nearGun)
        {
            PickGun(nearGun);
            stateMachine.SetCurrentState(ShootPlayer);
        }
    }

    bool ThereIsAGunPickable()
    {
        Gun[] guns = FindObjectsOfType<Gun>();

        bool thereIsAGunPickable = false;

        for (int i = 0; i < guns.Length; i++)
        {
            if (guns[i].CanBePicked())
            {
                if (!thereIsAGunPickable)
                {
                    nearestGunPosition = guns[i].transform.position;
                }
                Vector3 vv = guns[i].transform.position;
                if ((transform.position - vv).magnitude < (transform.position - nearestGunPosition).magnitude)
                {
                    nearestGunPosition = vv;
                }
                thereIsAGunPickable = true;
            }
        }
        return thereIsAGunPickable;
    }

    //state
    void Hide()
    {
        movementEngine.SetDestination((transform.position - player.transform.position).normalized * 2);

        if (ThereIsAGunPickable())
        {
            stateMachine.SetCurrentState(SeekGun);
        }
    }

    //state
    void ShootPlayer()
    {
        //transition
        if (!gun || !player)
            stateMachine.SetCurrentState(SeekGun);

        else if (player)
        {
            movementEngine.SetDestination(player.transform.position);
            gun.SetRotation(GetToPlayerRotation());
            if (PlayerInSight())
                gun.Shoot(myFaction);
        }
    }

    bool PlayerInSight()
    {
        RaycastHit2D[] ray = Physics2D.RaycastAll(transform.position, Quaternion.Euler(0, 0, GetToPlayerRotation()) * Vector2.up, 10000);
        for (int i = 0; i < ray.Length; i++)
        {
            if (ray[i].transform.gameObject.GetComponent<PlayerMovement>())
                return true;
            if (ray[i].transform.gameObject.name.StartsWith("wall"))
                return false;
        }
        return false;
    }

    float GetToPlayerRotation()
    {
        float angle = 0;
        Vector3 v = transform.position - player.transform.position;
        angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg + 90;
        return angle;
    }
}
