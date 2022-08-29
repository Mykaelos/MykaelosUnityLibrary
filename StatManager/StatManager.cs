using System;
using System.Collections.Generic;
using UnityEngine;

/**
 * StatManager handles adding up and tracking a collection of Stats.
 * 
 * PersistentStats are Stats that can't be removed and can be used as base stats. (May remove in the future.)
 * TransientStatProviders are StatProviders that have their stats updated every Recalculate. They can also be removed.
 *      Examples are items or temporary buffs.
 */
public class StatManager {
    #region LocalMessenger
    public const string AFTER_RECALCULATE = "StatManager.AFTER_RECALCULATE";
    LocalMessenger LocalMessenger = new LocalMessenger();
    #endregion

    private Dictionary<string, double> CalculatedStats = new Dictionary<string, double>();
    private Dictionary<string, Stat> PersistentStats = new Dictionary<string, Stat>();
    private List<StatProvider> TransientStatProviders = new List<StatProvider>();


    public StatManager(List<StatProvider> persistentStatProviders = null, List<StatProvider> transientStatProviders = null) {
        //TransientStatProviders = transientStatBonusProviders;

        if (transientStatProviders.IsNotEmpty()) {
            foreach (var provider in transientStatProviders) {
                AddTransientStatProvider(provider);
                //provider.ConnectReference(this);
            }
        }

        // Preload PersistentStats.
        if (persistentStatProviders.IsNotEmpty()) {
            foreach (var provider in persistentStatProviders) {
                var bonuses = provider.GetStats();
                foreach (var statBonus in bonuses) {
                    AddPersistent(statBonus);
                }
            }
        }

        Recalculate();
    }

    public void Recalculate(object[] args = null) {
        CalculatedStats.Clear();

        // Preloading all of the PersistentStats into the stats.
        foreach (var statBonusPair in PersistentStats) {
            var statBonus = statBonusPair.Value;
            Add(statBonus.Name, statBonus.Value);
        }

        foreach (var provider in TransientStatProviders) {
            var stats = provider.GetStats();
            foreach (var stat in stats) {
                Add(stat.Name, (int)stat.Value);
            }
        }

        SpecificRecalulate();

        LocalMessenger.Fire(AFTER_RECALCULATE);
    }

    protected virtual void SpecificRecalulate() {
        // Do nothing.
    }

    public void AddTransientStatProvider(StatProvider transientStatProvider) {
        TransientStatProviders.Add(transientStatProvider);
        transientStatProvider.ConnectReference(this);
    }

    public void RemoveTransientStatProvider(StatProvider transientStatProvider) {
        TransientStatProviders.Remove(transientStatProvider);
        transientStatProvider.ConnectReference(null);
    }


    #region Stat Methods
    public double Get(string name) {
        return CalculatedStats.Get(name, 0);
    }

    public void Set(string name, double value) {
        CalculatedStats.Set(name, value);
    }

    public void Add(string name, double value) {
        var currentValue = Get(name);
        CalculatedStats.Set(name, currentValue + value);
    }
    #endregion

    #region Persistent Methods
    // The goal of Persistent StatBonuses is to allow them to be passed by reference, which allows other classes
    // to adjust them permanently. These StatBonuses can then be saved to the file system via the statBonusProviders
    // that provided them.
    public void AddPersistent(Stat stat) {
        PersistentStats.Add(stat.Name, stat);
    }

    public Stat GetPersistentReference(string name) {
        return PersistentStats.Get(name);
    }
    #endregion

    #region LocalMessenger Methods
    public void On(string message, System.Action callback) {
        LocalMessenger.On(message, callback);
    }

    public void Un(string message, System.Action callback) {
        LocalMessenger.Un(message, callback);
    }
    #endregion
}

[Serializable]
public class Stat {
    #region LocalMessenger
    public const string CHANGED = "CHANGED";

    public LocalMessenger LocalMessenger {
        get { return _LocalMessenger == null ? _LocalMessenger = new LocalMessenger() : _LocalMessenger; }
    }
    [NonSerialized]
    private LocalMessenger _LocalMessenger;
    #endregion

    public string Name;
    public double Value {
        get { return _Value; }
        set {
            var previousValue = _Value;
            _Value = value;
            LocalMessenger.Fire(CHANGED, new object[] { Name, _Value, _Value - previousValue });
            //Debug.Log("{0}: {1}".FormatWith(Name, _Value));
        }
    }
    private double _Value;

    public Stat(string name, double value) {
        Name = name;
        _Value = value;
    }

    #region LocalMessenger Methods
    public void On(string message, System.Action<object[]> callback) {
        LocalMessenger.On(message, callback);
    }

    public void Un(string message, System.Action<object[]> callback) {
        LocalMessenger.Un(message, callback);
    }
    #endregion
}

public interface StatProvider {
    List<Stat> GetStats();
    void ConnectReference(StatManager statManager);
}
