using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    [SerializeField]
    public int startingHP;

    int hp;

    private void Awake()
    {
        hp = startingHP; 
    }

    public void TakeDamage(int amount)
    {
        hp -= amount; 
        if(hp==0)
        {
            Die(); 
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject); 
    }
}
