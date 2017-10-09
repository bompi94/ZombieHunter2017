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

    private void OnTriggerStay2D(Collider2D collision)
    {
        Weapon g = collision.GetComponent<Weapon>();
        EnemyShooter es = collision.GetComponent<EnemyShooter>();

        if (g)
        {
            if (g.CanBePicked() && !shooter.HasWeapon())
            {
                HighLightObject(g.gameObject);
                shooter.SetNearWeapon(g);
                GetComponentInParent<UIUpdater>().ShowPickPanel(); 
            }
        }

        else if (es && !shooter.HasWeapon())
        {
            shooter.SetNearEnemy(es);
            HighLightObject(es.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Weapon g = collision.GetComponent<Weapon>();
        if (g)
        {
            shooter.SetNearWeapon(null);
            DeHighLightObject(g.gameObject); 
        }

        EnemyShooter es = collision.GetComponent<EnemyShooter>();
        if (es)
        {
            shooter.SetNearEnemy(null);
            DeHighLightObject(es.gameObject);

        }
    }

    void HighLightObject(GameObject go)
    {
        go.GetComponent<Highlighter>().Highlight(Color.yellow); 
    }

    void DeHighLightObject(GameObject go)
    {
        go.GetComponent<Highlighter>().DeHighLight(); 
 
    }
}
