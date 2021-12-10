using System;
using System.Collections.Generic;

//public delegate void Callback(object[] args);

public class LocalMessenger {
    private Dictionary<string, Action> Listeners = new Dictionary<string, Action>();
    private Dictionary<string, Action<object[]>> ListenersWithArgs = new Dictionary<string, Action<object[]>>();
    // Unfortunately, a second list must be used to support callbacks with and without args.


    public void On(string message, Action action) {
        Action actions = Listeners.Get(message);
        actions += action;
        Listeners.Set(message, actions);
    }

    public void On(string message, Action<object[]> action) {
        Action<object[]> actions = ListenersWithArgs.Get(message);
        actions += action;
        ListenersWithArgs.Set(message, actions);
    }

    public void Un(string message, Action action) {
        if (action != null && Listeners.TryGetValue(message, out Action actions)) {
            actions -= action;
            Listeners.Set(message, actions);
        }
    }

    public void Un(string message, Action<object[]> action) {
        if (action != null && ListenersWithArgs.TryGetValue(message, out Action<object[]> actions)) {
            actions -= action;
            ListenersWithArgs.Set(message, actions);
        }
    }

    public void Fire(string message, object[] args = null) {
        //Debug.Log("Messenger.Fire: " + message);
        if (Listeners.TryGetValue(message, out Action actions)) {
            actions?.Invoke();
        }
        if (ListenersWithArgs.TryGetValue(message, out Action<object[]> actionsWithArgs)) {
            actionsWithArgs?.Invoke(args);
        }
    }

    public void RemoveAll(string message = null) {
        if (message != null) {
            Listeners.Remove(message);
            ListenersWithArgs.Remove(message);
        }
        else {
            Listeners.Clear();
            ListenersWithArgs.Clear();
        }
    }
}
