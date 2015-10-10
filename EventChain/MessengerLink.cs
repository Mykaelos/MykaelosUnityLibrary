using UnityEngine;
using System.Collections;
using System;

public class MessengerLink : EventLink {
    private string Message;
    private object[] Args;


    public MessengerLink(string message, object[] args = null) {
        Message = message;
        Args = args;
    }

    public override void Trigger(EventChain parent, Action finishedCallback) {
        Messenger.Fire(Message, Args);

        if (finishedCallback != null) {
            finishedCallback();
        }
    }
}
