using UnityEngine;
using System.Collections;
using System;

public class EventLink {


    public EventLink() { }

    public virtual void Trigger(EventChain parent, Action finishedCallback) { }

    public virtual void Stop() { }
}
