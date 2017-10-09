using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{

    protected override void Die()
    {
        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
        if (spawner)
            spawner.EnemyDead();

        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager)
            scoreManager.IncreaseScore();

        GetComponent<Explosion>().Explode();
        GetComponent<EnemyShooter>().LeaveWeapon();

        base.Die();
    }
}
