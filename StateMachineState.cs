
public abstract class StateMachineState {
    public string Name = "";

    public StateMachineState(string name) {
        Name = name;
    }

    public virtual void Start() { }
    public virtual void End() { }

    public abstract string Check();
    public abstract void Update();
}
