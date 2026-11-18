using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public List<State> States = new List<State>();
    public State CurrentState = null;

    public void SwitchState<T>()
    {
        foreach (State state in States)
        {
            if (state.GetType() == typeof(T))
            {
                CurrentState?.ExitState();
                CurrentState = state;
                CurrentState.EnterState();
                return;
            }
        }

        Debug.LogWarning("State does not exist");
    }

    public State GetState<T>() where T : State
    {
        return States.Find(state => state.GetType() == typeof(T));
    }

    public virtual void UpdateStateMachine()
    {
        CurrentState?.UpdateState();
    }

    public bool IsState<T>()
    {
        if (!CurrentState) return false;
        return CurrentState.GetType() == typeof(T);
    }
}