using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {
    Dictionary<string, IStateMachineState> States = new Dictionary<string, IStateMachineState>();
    IStateMachineState CurrentState;
    public string CurrentStateName {
        get { return CurrentState == null ? "" : CurrentState.GetName(); }
    }


    // Static Initializer to create or update a StateMachine on a GameObject.
    public static StateMachine Initialize<T>(GameObject gameObject, List<T> states, string initialState = null) where T : IStateMachineState {
        StateMachine machine;
        if (gameObject.GetComponent<StateMachine>() != null) {
            machine = gameObject.GetComponent<StateMachine>();
            machine.CurrentState = null;
        }
        else {
            machine = gameObject.AddComponent<StateMachine>();
        }

        machine.Initialize(states, initialState);

        return machine;
    }

    // Instanced initializer to update a StateMachine.
    public StateMachine Initialize<T>(List<T> states, string initialState = null) where T : IStateMachineState {
        States.Clear();
        foreach (var state in states) {
            States.Add(state.GetName(), state);
            state.SetParent(this);
        }
        if (initialState == null && states.Count > 0) { // Default to first state if null.
            initialState = states[0].GetName();
        }

        SwitchState(initialState);

        return this;
    }

    void Update() {
        if (CurrentState != null) {
            if (CurrentState.GetCheckFn() != null) {
                string nextStateName = CurrentState.GetCheckFn()();
                if (nextStateName != null) {
                    SwitchState(nextStateName);
                }
            }

            if (CurrentState.GetUpdateFn() != null) {
                CurrentState.GetUpdateFn()();
            }
        }
    }

    public void SwitchState(string nextStateName) {
        if (CurrentStateName.Equals(nextStateName)) {
            return;
        }

        IStateMachineState nextState = States.Get(nextStateName);

        if (nextState == null) {
            Debug.Log(string.Format("StateMachine on {0} could not switch to {1}.", gameObject.name, nextStateName));
            return;
        }

        if (CurrentState != null && CurrentState.GetEndFn() != null) {
            CurrentState.GetEndFn()();
        }

        CurrentState = nextState;
        if (CurrentState.GetStartFn() != null) {
            CurrentState.GetStartFn()();
        }
    }
}
