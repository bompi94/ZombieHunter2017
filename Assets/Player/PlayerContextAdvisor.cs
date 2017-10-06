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
            if (g.CanBePicked() && !shooter.HasGun())
            {
                HighLightObject(g.gameObject);
                shooter.SetNearGun(g);
                GetComponentInParent<UIUpdater>().ShowPickPanel(); 
            }
        }

        else if (es && !shooter.HasGun())
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
