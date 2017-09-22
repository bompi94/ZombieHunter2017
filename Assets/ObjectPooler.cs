using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {
    [SerializeField]
    GameObject myObject;

    List<GameObject> Available = new List<GameObject>();
    List<GameObject> NotAvailable = new List<GameObject>(); 

    public GameObject GetObject()
    {
        if(Available.Count==0)
        {
            GameObject b = Instantiate(myObject);
            NotAvailable.Add(b);
            return b; 
        }

        else
        {
            GameObject b = Available[0];
            b.SetActive(true); 
            Available.RemoveAt(0);
            NotAvailable.Add(b);
            return b; 
        }
    }

    public void GameObjectReturnsAvailable(GameObject b)
    {
        b.SetActive(false);
        NotAvailable.Remove(b);
        Available.Add(b);
    }
}
