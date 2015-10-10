using UnityEngine;
using System.Collections;
using System;

public class WaitLink : EventLink {
    private float Duration;


    public WaitLink(float duration) {
        Duration = duration;
    }

    public override void Trigger(EventChain parent, Action finishedCallback) {
        parent.StartCoroutine(Wait(finishedCallback));
    }

    IEnumerator Wait(Action finishedCallback) {
        yield return new WaitForSeconds(Duration);
        finishedCallback();
    }
}
