using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballBat : Weapon {

    private void Awake()
    {
        type = WeaponType.Bat; 
    }

    public override void Use()
    {
        print("swinging my bat"); 
    }

}
