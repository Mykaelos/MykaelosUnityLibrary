using System;

public interface IStateMachineState {
    void SetName(string name);
    string GetName();

    void SetParent(StateMachine parent);
    StateMachine GetParent();

    Action GetStartFn();
    Func<string> GetCheckFn();
    Action GetUpdateFn();
    Action GetEndFn();

    void SwitchState(string nextStateName);
}
