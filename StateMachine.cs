using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {
    Dictionary<string, StateMachineState> States = new Dictionary<string, StateMachineState>();
    StateMachineState CurrentState;


    public static StateMachine Initialize(GameObject gameObject, List<StateMachineState> states, string initialState) {
        StateMachine machine = gameObject.AddComponent<StateMachine>();

        foreach(var state in states) {
            machine.States.Add(state.Name, state);
        }

        machine.SwitchState(initialState);

        return machine;
    }

    void Update() {
        if(CurrentState != null) {
            string nextStateName = CurrentState.Check();
            if(nextStateName != null) {
                SwitchState(nextStateName);
            }

            CurrentState.Update();
        }
    }

    public void SwitchState(string nextStateName) {
        StateMachineState nextState = States.Get(nextStateName);

        if(nextState == null) {
            Debug.Log(string.Format("StateMachine on {0} could not switch to {1}.", gameObject.name, nextStateName));
            return;
        }

        if(CurrentState != null && CurrentState.End != null) {
            CurrentState.End();
        }

        CurrentState = nextState;
        if(CurrentState.Start != null) {
            CurrentState.Start();
        }
    }
}
