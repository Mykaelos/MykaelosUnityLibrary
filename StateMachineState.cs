
using System;

public class StateMachineState {
    public string Name = "";
    public Action Start;
    public Func<string> Check;
    public Action Update;
    public Action End;

    public StateMachineState(string name, Func<string> check, Action update, Action start = null, Action end = null) {
        Name = name;
        Start = start;
        Check = check;
        Update = update;
        End = end;
    }

    //public virtual void Start() { }
    //public virtual void End() { }

    //public abstract string Check();
    //public abstract void Update();
}
