using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public delegate void Callback(object[] args);

public class Messenger {
    private static Dictionary<string, List<Callback>> Listeners = new Dictionary<string, List<Callback>>();

    public static void On(string message, Callback callback) {
        List<Callback> group = null;
        if (!Listeners.TryGetValue(message, out group)) {
            group = new List<Callback>();
            Listeners.Add(message, group);
        }
        group.Add(callback);
    }

    public static void Un(string message, Callback callback) {
        List<Callback> group = null;
        if (callback != null && Listeners.TryGetValue(message, out group)) {
            group.Remove(callback);
        }
    }

    public static void Fire(string message, object[] args = null) {
        List<Callback> group = null;
        if (Listeners.TryGetValue(message, out group)) {
            List<Callback> tempList = new List<Callback>(group); //protects from removing callbacks during fire
            foreach (var listener in tempList) {
                listener(args);
            }
        }
    }

    public static void RemoveAll(string message = null) {
        if (message != null) {
            Listeners.Remove(message);
        }
        else {
            Listeners.Clear();
        }
    }
}
