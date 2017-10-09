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
    bool confused;
    float confusedTimer;
    float confusionTimeEnd = 2;

    Weapon nearest;
    float minGunDistanceToPick = 0.5f;

    [SerializeField]
    GameObject confusedSignal;

    private void Awake()
    {
        if (FindObjectOfType<PlayerMovement>())
            player = FindObjectOfType<PlayerMovement>().gameObject;
        shooter = GetComponent<EnemyShooter>();
        TimeManager.Instance.tick.AddListener(TimedUpdate);
    }

    bool CanPickGun()
    {
        return !shooter.HasWeapon() && nearest && Vector3.Distance(transform.position, nearest.transform.position) < minGunDistanceToPick;
    }

    private void Update()
    {
        Movement();

        if (!confused && CanPickGun())
        {
            shooter.PickWeapon(nearest);
        }

    }

    void TimedUpdate()
    {
        if (confused)
        {
            confusedTimer += Time.deltaTime;
            if (confusedTimer >= confusionTimeEnd)
            {
                NoMoreConfused();
            }
        }

        transform.position += direction * speed * TimeManager.deltaTime;
    }

    void Movement()
    {
        direction = Vector3.zero;

        if (player && SeePlayer() && !confused)
        {
            if (shooter.HasWeapon())
                direction = (player.transform.position - transform.position).normalized;
            else if (!confused)
                direction = (GetNearestGunPosition() - transform.position).normalized;
        }

    }

    Vector3 GetNearestGunPosition()
    {
        Weapon[] guns = FindObjectsOfType<Weapon>();
        float dist = Mathf.Infinity;
        nearest = null;
        for (int i = 0; i < guns.Length; i++)
        {

            float d = Vector3.Distance(guns[i].transform.position, transform.position);

            if (guns[i].CanBePicked() && d < dist)
            {
                nearest = guns[i];
                dist = d;
            }
        }
        if (nearest)
            return nearest.transform.position;
        return Vector3.zero;
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

    public void Confused()
    {
        confused = true;
        confusedTimer = 0;
        confusedSignal.SetActive(true);
    }

    void NoMoreConfused()
    {
        confused = false;
        confusedTimer = 0;
        confusedSignal.SetActive(false);
    }
}
