using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContextAdvisor : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Gun g = (collision.GetComponent<Gun>());
        if (g)
        {
            if (g.CanBePicked())
            {
                print("near");
                GetComponentInParent<Shooter>().SetNearGun(g);
            }
        }

        EnemyShooter es = collision.GetComponent<EnemyShooter>();
        if (es)
        {
            GetComponentInParent<PlayerShooter>().SetNearEnemy(es); 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Gun g = (collision.GetComponent<Gun>());
        if (g)
        {
            print("not near");
            GetComponentInParent<Shooter>().SetNearGun(null);
        }

        EnemyShooter es = collision.GetComponent<EnemyShooter>();
        if (es)
        {
            GetComponentInParent<PlayerShooter>().SetNearEnemy(null);
        }
    }
}
