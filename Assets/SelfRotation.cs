using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRotation : MonoBehaviour {

    [SerializeField]
    float rotationSpeed;

    Vector3 rot = new Vector3(0, 0, 0); 

    private void OnEnable()
    {
        rot.z = rotationSpeed; 
        TimeManager.Instance.tick.AddListener(TimedUpdate); 
    }

    void TimedUpdate()
    {
        transform.Rotate(rot * TimeManager.deltaTime); 
    }
}
