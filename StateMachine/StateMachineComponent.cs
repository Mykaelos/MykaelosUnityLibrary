using System.Collections.Generic;
using UnityEngine;

namespace Mykaelos.UnityLibrary.StateMachine {
    public class StateMachineComponent : MonoBehaviour {
        public StateMachine StateMachine;


        public static StateMachine Initialize(GameObject gameObject, List<StateMachineState> states, string initialState = null) {
            StateMachineComponent stateMachineComponent;

            if (gameObject.GetComponent<StateMachineComponent>() != null) {
                stateMachineComponent = gameObject.GetComponent<StateMachineComponent>();
                stateMachineComponent.StateMachine = new StateMachine(states, initialState);
                stateMachineComponent.StateMachine.CurrentState = null;
            }
            else {
                stateMachineComponent = gameObject.AddComponent<StateMachineComponent>();
                stateMachineComponent.StateMachine = new StateMachine(states, initialState);
            }

            return stateMachineComponent.StateMachine;
        }

        private void Update() {
            if (StateMachine != null) {
                StateMachine.Update();
            }
        }

        public void SwitchState(string nextStateName) {
            if (StateMachine != null) {
                StateMachine.SwitchState(nextStateName);
            }
        }
    }
}
