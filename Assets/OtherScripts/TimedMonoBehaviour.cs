using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedMonoBehaviour : MonoBehaviour {

    protected void OnEnable()
    {
        TimeManager timemanager = TimeManager.Instance; 
        timemanager.RegisterBehaviour(this.TimeManagerUpdate); 
    }

    /// <summary>
    /// Called by TimeManager with a custom delta time
    /// </summary>
    protected virtual void TimeManagerUpdate() { }
}
