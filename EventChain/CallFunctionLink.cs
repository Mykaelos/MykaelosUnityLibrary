using UnityEngine;
using System.Collections;
using System;

public delegate void CallFunctionPrototype(object[] args);

public class CallFunctionLink : EventLink {
    private CallFunctionPrototype CallFunction;
    private object[] Args;


    public CallFunctionLink(CallFunctionPrototype callFunction, object[] args = null) {
        CallFunction = callFunction;
        Args = args;
    }

    public override void Trigger(EventChain parent, Action finishedCallback) {
        if (CallFunction != null) {
            CallFunction(Args);
        }
        if (finishedCallback != null) {
            finishedCallback();
        }
    }
}
