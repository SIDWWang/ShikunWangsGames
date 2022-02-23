using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public Action OnStateChange;
    
    public enum State
    {
        InProgress,
        Completed
    }

    [HideInInspector] public State currentState { get; private set;  }

    public void OnInteract()
    {
        if (currentState == State.InProgress)
            Execute();
    }

    public void SetState(State state)
    {
        currentState = state;
        OnStateChange?.Invoke();
    }
    protected abstract void Execute();
}
