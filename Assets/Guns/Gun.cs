using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    float recoil;

    [SerializeField]
    Transform shootPos;

    ObjectPooler bulletPooler;

    bool pickedUp = false;

    bool canBePickedUp = true; 

    private void Awake()
    {
        GameObject go = new GameObject();
        go.transform.SetParent(gameObject.transform);
        go.AddComponent<ObjectPooler>();
        go.GetComponent<ObjectPooler>().SetUp(bullet);
        bulletPooler = go.GetComponent<ObjectPooler>();   
    }

    public void SetRotation(float angle)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void Shoot()
    {
        GameObject b = bulletPooler.GetObject();
        b.transform.position = shootPos.position;
        Vector3 dir = shootPos.position - transform.position;
        b.GetComponent<Bullet>().Fire(dir, bulletPooler);
    }

    public Vector3 GetRecoilVector()
    {
        return (transform.position - shootPos.position ) * recoil; 
    }

    public void PickedUp()
    {
        pickedUp = true;
    }

    public void Leaved()
    {
        pickedUp = false;      
    }

    public bool CanBePicked()
    {
        return !pickedUp; 
    }
}
