using System;
using System.Collections.Generic;

public class Messenger {
    private static Dictionary<string, Action> Listeners = new Dictionary<string, Action>();
    private static Dictionary<string, Action<object[]>> ListenersWithArgs = new Dictionary<string, Action<object[]>>();
    // Unfortunately, a second list must be used to support callbacks with and without args.


    public static void On(string message, Action action) {
        Action actions = Listeners.Get(message);
        actions += action;
        Listeners.Set(message, actions);
    }

    public static void On(string message, Action<object[]> action) {
        Action<object[]> actions = ListenersWithArgs.Get(message);
        actions += action;
        ListenersWithArgs.Set(message, actions);
    }

    public static void Un(string message, Action action) {
        if (action != null && Listeners.TryGetValue(message, out Action actions)) {
            actions -= action;
            Listeners.Set(message, actions);
        }
    }

    public static void Un(string message, Action<object[]> action) {
        if (action != null && ListenersWithArgs.TryGetValue(message, out Action<object[]> actions)) {
            actions -= action;
            ListenersWithArgs.Set(message, actions);
        }
    }

    public static void Fire(string message, object[] args = null) {
        //Debug.Log("Messenger.Fire: " + message);
        if (Listeners.TryGetValue(message, out Action actions)) {
            actions?.Invoke();
        }
        if (ListenersWithArgs.TryGetValue(message, out Action<object[]> actionsWithArgs)) {
            actionsWithArgs?.Invoke(args);
        }
    }

    public static void RemoveAll(string message = null) {
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
