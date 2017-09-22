using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float angle;

        float x = Input.GetAxis("YHorizontal");
        float y = Input.GetAxis("YVertical");

        if (Mathf.Abs(x) >= 0.5 || Mathf.Abs(y) >= 0.5)
        {
            angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg + 90;
            print(angle);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle));
        }

    }
}
