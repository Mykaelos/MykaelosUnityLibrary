using UnityEngine;
using System.Collections.Generic;

public class EventChain : MonoBehaviour {
    private List<EventLink> Chain = new List<EventLink>();
    private int CurrentEvent;

    public bool IsRunning = false;
    public bool IsFinished = false;

    private static EventChain Instance;
    public static EventChain GetInstance() {
        if (Instance == null) {
            Instance = new GameObject("EventChain").AddComponent<EventChain>();
        }

        return Instance;
    }

    public static EventChain Begin(List<EventLink> chain) {
        return Begin(null, chain);
    }

    public static EventChain Begin(GameObject gameObject, List<EventLink> chain) {
        EventChain instance;
        if (gameObject != null) {
            if (gameObject.GetComponent<EventChain>()) {
                instance = gameObject.GetComponent<EventChain>();

                if (instance.IsRunning) {
                    instance.Stop();
                }
            }
            else {
                instance = gameObject.AddComponent<EventChain>();
            }
        }
        else {
            instance = GetInstance();
        }
        
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
