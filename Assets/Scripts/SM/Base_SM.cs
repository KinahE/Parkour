using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Base_SM : MonoBehaviour
{
    private State currentState;
    public State DebugCurrentState => currentState;

    public void SwitchState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    private void Update()
    {
        currentState?.Tick(Time.deltaTime);
    }
}
