using System.Collections.Generic;
using UnityEngine;

namespace Mykaelos.UnityLibrary.StateMachine {
    public class StateMachine {
        internal Dictionary<string, StateMachineState> States = new Dictionary<string, StateMachineState>();
        internal StateMachineState CurrentState;
        public string CurrentStateName {
            get { return CurrentState == null ? "" : CurrentState.Name; }
        }


        public StateMachine(List<StateMachineState> states, string initialState = null) {
            States.Clear();
            foreach (var state in states) {
                States.Add(state.Name, state);
                state.Parent = this;
            }
            if (initialState == null) { // Default to first state if null.
                initialState = states[0].Name;
            }

            SwitchState(initialState);
        }

        public void Update() {
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
                Debug.Log(string.Format("StateMachine could not switch to {0}.", nextStateName));
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
}
