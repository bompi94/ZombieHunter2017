using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {

    [SerializeField]
    public int startingHP;

    bool canBeDamaged = true; 

    int hp;

    [HideInInspector]
    public UnityEvent healthChanged = new UnityEvent(); 

    private void Awake()
    {
        hp = startingHP;
    }

    public void TakeDamage(int amount)
    {
        if (canBeDamaged)
        {
            hp -= amount;
            healthChanged.Invoke(); 
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

    public int GetHP()
    {
        return hp; 
    }
}
