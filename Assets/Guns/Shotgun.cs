using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun {

    public override bool Shoot()
    {
        if (canShoot)
        {
            GameObject b = bulletPooler.GetObject();
            GameObject b1 = bulletPooler.GetObject();
            GameObject b2 = bulletPooler.GetObject();

            b.transform.position = shootPos.position;
            b1.transform.position = shootPos.position;
            b2.transform.position = shootPos.position;

            float angle = 20;

            Vector2 dir = shootPos.position - transform.position;
            Vector2 dir1 = Quaternion.Euler(0, 0, angle) * dir;
            Vector2 dir2 = Quaternion.Euler(0, 0, -angle) * dir; 

            b.GetComponent<Bullet>().Fire(dir, bulletPooler);
            b1.GetComponent<Bullet>().Fire(dir1 , bulletPooler);
            b2.GetComponent<Bullet>().Fire(dir2 , bulletPooler);

            canShoot = false;
            return true;
        }
        return false;
    }


}
