using System;
using System.Collections.Generic;
using UnityEngine;

public class LocalMessengerMonoBehaviour : MonoBehaviour {
    private LocalMessenger LocalMessenger = new LocalMessenger();


    public static LocalMessengerMonoBehaviour Get(Transform childTransform) {
        var checkingTransform = childTransform;
        while (checkingTransform != null) {
            LocalMessengerMonoBehaviour foundComponent = checkingTransform.GetComponent<LocalMessengerMonoBehaviour>();
            if (foundComponent != null) {
                return foundComponent;
            }
            checkingTransform = checkingTransform.parent;
        }

        return null;
    }

    #region LocalMessenger Methods
    public void On(string message, Action action) {
        LocalMessenger.On(message, action);
    }

    public void On(string message, Action<object[]> action) {
        LocalMessenger.On(message, action);
    }

    public void Un(string message, Action action) {
        LocalMessenger.Un(message, action);
    }

    public void Un(string message, Action<object[]> action) {
        LocalMessenger.Un(message, action);
    }

    public void Fire(string message, object[] args = null) {
        LocalMessenger.Fire(message, args);
    }

    public void RemoveAll(string message = null) {
        LocalMessenger.RemoveAll(message);
    }
    #endregion LocalMessenger Methods
}
