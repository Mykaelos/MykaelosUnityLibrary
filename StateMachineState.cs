using System;

public class StateMachineState {
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

    public StateMachineState() { }
}
