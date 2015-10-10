﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventChain : MonoBehaviour {
    private List<EventLink> Chain = new List<EventLink>();
    private int CurrentEvent;

    public bool IsRunning = true;
    public bool IsFinished = false;


    private static EventChain CreateInstance() {
        return (new GameObject("EventChain")).AddComponent<EventChain>();
    }

    public static EventChain Begin(List<EventLink> chain) {
        EventChain instance = CreateInstance();
        instance.Chain = chain;
        instance.IsRunning = true;
        instance.IsFinished = false;
        instance.CurrentEvent = -1;
        instance.CallNextEvent();

        return instance;
    }

    public void Pause(bool pause) {
        IsRunning = !pause;
    }

    public void Stop() {
        IsRunning = false;
        if (CurrentEvent < Chain.Count) {
            Chain[CurrentEvent].Stop();
        }
    }

    public void CallNextEvent() {
        if (IsRunning) {
            if (++CurrentEvent < Chain.Count) {
                Chain[CurrentEvent].Trigger(this, CallNextEvent);
            }
            else {
                IsFinished = true;
                IsRunning = false;
            }
        }
    }
}
