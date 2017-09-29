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
        if (g)
        {
            if (g.CanBePicked())
            {
               shooter.SetNearGun(g);
            }
        }

        EnemyShooter es = collision.GetComponent<EnemyShooter>();
        if (es)
        {
            shooter.SetNearEnemy(es); 
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
}
