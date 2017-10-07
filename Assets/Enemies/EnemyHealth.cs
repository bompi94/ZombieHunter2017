using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health {

    EnemySpawner spawner; 

    protected override void Die()
    {
        spawner = FindObjectOfType<EnemySpawner>();
        spawner.EnemyDead();

        FindObjectOfType<ScoreManager>().IncreaseScore(); 

        GetComponent<Explosion>().Explode();
        GetComponent<EnemyShooter>().LeaveGun();

        base.Die();
    }
}
