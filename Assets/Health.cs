using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    [SerializeField]
    public int startingHP;

    bool canBeDamaged = true; 

    int hp;

    private void Awake()
    {
        hp = startingHP; 
    }

    public void TakeDamage(int amount)
    {
        if (canBeDamaged)
        {
            hp -= amount;
            if (hp == 0)
            {
                Die();
            }
        }
    }

    public void BecomesInvulnerable()
    {
        canBeDamaged = false;
    }

    public void BecomesVulnerable()
    {
        canBeDamaged = true; 
    }

    protected virtual void Die()
    {
        Destroy(gameObject); 
    }
}
