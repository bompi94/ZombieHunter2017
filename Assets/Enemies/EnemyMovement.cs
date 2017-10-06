using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField]
    float speed;

    GameObject player;
    Vector3 direction;
    EnemyShooter shooter;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        shooter = GetComponent<EnemyShooter>();
        TimeManager.Instance.tick.AddListener(TimedUpdate);
    }

    private void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (player && SeePlayer())
        {
            if (shooter.HasGun())
                direction = (player.transform.position - transform.position).normalized;
            else
                direction = (transform.position - player.transform.position).normalized;

        }

        else
        {
            direction = Vector3.zero;
        }
    }

    bool SeePlayer()
    {
        RaycastHit2D[] rays = Physics2D.RaycastAll(transform.position, (player.transform.position - transform.position));
        for (int i = 0; i < rays.Length; i++)
        {
            if (rays[i].collider.gameObject.CompareTag("platform"))
            {
                return false;
            }
            if (rays[i].collider.gameObject.GetComponent<PlayerMovement>())
            {
                return true;
            }
        }
        return false;
    }

    void TimedUpdate()
    {
        transform.position += direction * speed * TimeManager.deltaTime;
    }
}
