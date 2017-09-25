using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour {

    List<Transition> transitions = new List<Transition>(); 

    public State Run()
    {
        Transition fired = TransitionFired();
        if (fired)
        {
            ExitAction();
            fired.nextState.EnterAction(); 
            return fired.nextState;
        }
        else
        {
            RunAction();
            return this; 
        }
    }

    public virtual void EnterAction()
    {

    }

    public virtual void RunAction()
    {

    } 

    public virtual void ExitAction()
    {

    }

    public Transition TransitionFired()
    {
        for (int i = 0; i < transitions.Count; i++)
        {
            if (transitions[i].IsConditionVerified())
                return transitions[i]; 
        }
        return null; 
    }

    public void AddTransition(Transition t)
    {
        transitions.Add(t); 
    }
}
