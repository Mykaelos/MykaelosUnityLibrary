﻿using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {
    Dictionary<string, StateMachineState> States = new Dictionary<string, StateMachineState>();
    StateMachineState CurrentState;
    public string CurrentStateName {
        get { return CurrentState == null ? "" : CurrentState.Name; }
    }


    public static StateMachine Initialize(GameObject gameObject, List<StateMachineState> states, string initialState = null) {
        StateMachine machine;
        if (gameObject.GetComponent<StateMachine>() != null) {
            machine = gameObject.GetComponent<StateMachine>();
            machine.CurrentState = null;
        }
        else {
            machine = gameObject.AddComponent<StateMachine>();
        }

        machine.States.Clear();
        foreach (var state in states) {
            machine.States.Add(state.Name, state);
            state.Parent = machine;
        }
        if (initialState == null) { // Default to first state if null.
            initialState = states[0].Name;
        }

        machine.SwitchState(initialState);

        return machine;
    }

    void Update() {
        if (CurrentState != null) {
            if (CurrentState.Check != null) {
                string nextStateName = CurrentState.Check();
                if (nextStateName != null) {
                    SwitchState(nextStateName);
                }
            }

            if (CurrentState.Update != null) {
                CurrentState.Update();
            }
        }
    }

    public void SwitchState(string nextStateName) {
        if (CurrentStateName.Equals(nextStateName)) {
            return;
        }

        StateMachineState nextState = States.Get(nextStateName);

        if (nextState == null) {
            Debug.Log(string.Format("StateMachine on {0} could not switch to {1}.", gameObject.name, nextStateName));
            return;
        }

        if (CurrentState != null && CurrentState.End != null) {
            CurrentState.End();
        }

        CurrentState = nextState;
        if (CurrentState.Start != null) {
            CurrentState.Start();
        }
    }
}