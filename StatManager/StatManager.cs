using System.Collections.Generic;

public abstract class StatManager {
    #region LocalMessenger
    public const string AFTER_RECALCULATE = "StatManager.AFTER_RECALCULATE";
    LocalMessenger LocalMessenger = new LocalMessenger();
    #endregion

    private Dictionary<string, int> Stats = new Dictionary<string, int>();


    public void Recalculate(object[] args = null) {
        Stats.Clear();

        SpecificRecalulate();

        LocalMessenger.Fire(AFTER_RECALCULATE);
    }

    protected abstract void SpecificRecalulate();

    public int Get(string name) {
        return Stats.Get(name, 0);
    }

    public void Add(string name, int value) {
        var currentValue = Get(name);
        Stats.Set(name, currentValue + value);
    }

    public void On(string message, Callback callback) {
        LocalMessenger.On(message, callback);
    }

    public void Un(string message, Callback callback) {
        LocalMessenger.Un(message, callback);
    }
}

public class StatBonus {
    public string Name;
    public int Value;

    public StatBonus(string name, int value) {
        Name = name;
        Value = value;
    }
}
