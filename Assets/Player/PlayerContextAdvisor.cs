using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContextAdvisor : MonoBehaviour
{
    PlayerShooter shooter;

    private void Awake()
    {
        shooter = GetComponentInParent<PlayerShooter>(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Gun g = (collision.GetComponent<Gun>());
        EnemyShooter es = collision.GetComponent<EnemyShooter>();

        if (g)
        {
            if (g.CanBePicked())
            {
                HighLightObject(g.gameObject);
                shooter.SetNearGun(g);
            }
        }

        else if (es)
        {
            shooter.SetNearEnemy(es);
            HighLightObject(es.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Gun g = (collision.GetComponent<Gun>());
        if (g)
        {
            shooter.SetNearGun(null);
        }

        EnemyShooter es = collision.GetComponent<EnemyShooter>();
        if (es)
        {
            shooter.SetNearEnemy(null);
        }
    }

    void HighLightObject(GameObject go)
    {
    }

    void DeHighLightObject(GameObject go)
    {
    }
}
