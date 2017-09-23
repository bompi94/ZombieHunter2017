using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun {

    public override bool Shoot(Faction faction)
    {
        if (canShoot)
        {
            GameObject b = bulletPooler.GetObject();
            GameObject b1 = bulletPooler.GetObject();
            GameObject b2 = bulletPooler.GetObject();

            b.transform.position = shootPos.position;
            b1.transform.position = shootPos.position;
            b2.transform.position = shootPos.position;

            float angle = 10;

            Vector2 dir = shootPos.position - transform.position;
            Vector2 dir1 = Quaternion.Euler(0, 0, angle) * dir;
            Vector2 dir2 = Quaternion.Euler(0, 0, -angle) * dir; 

            b.GetComponent<Bullet>().Fire(dir, bulletPooler, damages, faction);
            b1.GetComponent<Bullet>().Fire(dir1 , bulletPooler, damages, faction);
            b2.GetComponent<Bullet>().Fire(dir2 , bulletPooler, damages, faction);

            canShoot = false;

            actualBullets--;
            if (actualBullets == 0)
            {
                GunBreak();
                return false;
            }

            return true;
        }
        return false;
    }


}
