using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    public delegate void state();

    state handler; 

    public void Run()
    {
        handler();  
    }

    public void SetCurrentState(state s)
    {
        handler = s; 
    }
}
