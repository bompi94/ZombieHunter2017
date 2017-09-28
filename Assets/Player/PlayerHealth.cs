using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health {

    protected override void Die()
    {
        GetComponent<Explosion>().Explode();
        TimeManager.Instance.FastTime(); 
        base.Die();
    }

}
