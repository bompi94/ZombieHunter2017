using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : Shooter
{
    [SerializeField]
    float shootTime; 
    GameObject player;
    float shootTimer;

    protected override void Awake()
    {
        shootTimer = shootTime; 
        player = FindObjectOfType<PlayerMovement>().gameObject;
        if (transform.GetChild(0) != null)
        {
            PickGun(transform.GetChild(0).GetComponent<Gun>());
        }
        base.Awake();
    }

    private void Update()
    {
        gun.SetRotation(GetToPlayerRotation());
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootTime)
        {
            if (gun)
                ShootPlayer();
            shootTimer = 0; 
        }
    }

    void ShootPlayer()
    {
        if (player)
        {
            //should probably move somewhere
            if (PlayerInSight())
                gun.Shoot();
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
