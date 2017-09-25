using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour {

    public State nextState; 

    public virtual bool IsConditionVerified()
    {
        return false; 
    }
}
