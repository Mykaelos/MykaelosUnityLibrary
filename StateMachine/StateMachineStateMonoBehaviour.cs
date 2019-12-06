using System;
using UnityEngine;

public class StateMachineStateMonoBehaviour : MonoBehaviour, IStateMachineState {
    private StateMachine Parent;
    private string Name = "";

    private Func<string> CheckFnStored;
    private Action UpdateFnStored;
    private Action StartFnStored;
    private Action EndFnStored;


    // StateMachineStateMonoBehaviour cannot be "new"ed, because it's a MonoBehaviour. Instead, it has a Setup method.
    public void Setup(string name, Func<string> check = null, Action update = null, Action start = null, Action end = null) {
        Name = name;
        CheckFnStored = check;
        UpdateFnStored = update;
        StartFnStored = start;
        EndFnStored = end;
    }

    public void SetName(string name) { Name = name; }
    public string GetName() { return Name; }

    public void SetParent(StateMachine parent) { Parent = parent; }
    public StateMachine GetParent() { return Parent; }

    public Func<string> GetCheckFn() { return CheckFnStored ?? CheckFn; }
    public Action GetUpdateFn() { return UpdateFnStored ?? UpdateFn; }
    public Action GetStartFn() { return StartFnStored ?? StartFn; }
    public Action GetEndFn() { return EndFnStored ?? EndFn; }

    public void SwitchState(string nextStateName) {
        if (Parent != null) {
            Parent.SwitchState(nextStateName);
        }
    }

    // Utility method to make it easy to get the name of the StateMachineState. This makes it easy to switch a state by its class name.
    // SwitchState(ClassNameOf<GamePlayState>());
    public static string ClassNameOf<T>() {
        return typeof(T).Name;
    }

    // State Methods for overriding in a child class.
    protected virtual string CheckFn() { return null; }
    protected virtual void UpdateFn() { }
    protected virtual void StartFn() { }
    protected virtual void EndFn() { }
}
