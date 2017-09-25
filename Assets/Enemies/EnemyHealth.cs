using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health {

    protected override void Die()
    {
        GetComponent<Shooter>().LeaveGun();
        base.Die();
    }
}
