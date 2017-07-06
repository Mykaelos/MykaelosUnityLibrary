using UnityEngine;
using System;

public class StateMachineState {
    public StateMachine Parent;

    public string Name = "";
    public Action Start;
    public Func<string> Check;
    public Action Update;
    public Action End;


    public StateMachineState(string name, Func<string> check = null, Action update = null, Action start = null, Action end = null) {
        Name = name;
        Start = start;
        Check = check;
        Update = update;
        End = end;
    }

    public StateMachineState() {
        // This base constructor is automatically run in C#. So it doesn't need to be called specifically. 
        Name = GetType().Name;

        Check = CheckFn;
        Update = UpdateFn;
        Start = StartFn;
        End = EndFn;
    }

    // These virtual methods allow any child StateMachineStates to override the methods directly instead of having to pass them in the constructor.
    public virtual string CheckFn() { return null; }
    public virtual void UpdateFn() { }
    public virtual void StartFn() { }
    public virtual void EndFn() { }

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
}
