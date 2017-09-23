using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBooster : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;

        if (go.GetComponent<Shooter>())
        {
            if (go.GetComponent<Shooter>().ReloadGun())
                Dismiss();
        }
    }

    void Dismiss()
    {
        Destroy(gameObject);
    }
}
