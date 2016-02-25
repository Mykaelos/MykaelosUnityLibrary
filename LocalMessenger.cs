using System.Collections.Generic;

//public delegate void Callback(object[] args);

//There might be a better way to merge this with Messenger at some point, but this is the simplest solution for now.
public class LocalMessenger {
    private Dictionary<string, List<Callback>> Listeners;

    public LocalMessenger() {
        Listeners = new Dictionary<string, List<Callback>>();
    }

    public void On(string message, Callback callback) {
        List<Callback> group = null;
        if (!Listeners.TryGetValue(message, out group)) {
            group = new List<Callback>();
            Listeners.Add(message, group);
        }
        group.Add(callback);
    }

    public void Un(string message, Callback callback) {
        List<Callback> group = null;
        if (callback != null && Listeners.TryGetValue(message, out group)) {
            group.Remove(callback);
        }
    }

    public void Fire(string message, object[] args = null) {
        //Debug.Log("Messenger.Fire: " + message);
        List<Callback> group = null;
        if (Listeners.TryGetValue(message, out group)) {
            List<Callback> tempList = new List<Callback>(group); //protects from removing callbacks during fire
            foreach (var listener in tempList) {
                listener(args);
            }
        }
    }

    public void RemoveAll(string message = null) {
        if (message != null) {
            Listeners.Remove(message);
        }
        else {
            Listeners.Clear();
        }
    }
}
