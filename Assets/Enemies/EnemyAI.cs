using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : Shooter {

    FSM stateMachine = new FSM();
    GameObject player;
    Vector3 nearestGunPosition = Vector3.zero;
    EnemyMovement movementEngine;

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

    void SeekGun()
    {
        Gun[] guns = FindObjectsOfType<Gun>();

        if (guns.Length >= 0)
        {
            GoToNearestGun(guns);
        }
        else
        {
            movementEngine.SetDestination(transform.position);
        }

        if(!gun && nearGun)
        {
            PickGun(nearGun);
            stateMachine.SetCurrentState(ShootPlayer); 
        }
    }

    void GoToNearestGun(Gun[] guns)
    {
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
        if (thereIsAGunPickable)
        {
            movementEngine.SetDestination(nearestGunPosition);
        }
        else
        {
            movementEngine.SetDestination(transform.position);
        }
    }

    void ShootPlayer()
    {
        if (!gun || !player)
            stateMachine.SetCurrentState(SeekGun); 

        else if (player)
        {
            movementEngine.SetDestination(player.transform.position);
            gun.SetRotation(GetToPlayerRotation());
            gun.Shoot(myFaction);
        }
    }

    float GetToPlayerRotation()
    {
        float angle = 0;
        Vector3 v = transform.position - player.transform.position;
        angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg + 90;
        return angle;
    }
}
