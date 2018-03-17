using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatManager {
    #region LocalMessenger
    public const string AFTER_RECALCULATE = "StatManager.AFTER_RECALCULATE";
    LocalMessenger LocalMessenger = new LocalMessenger();
    #endregion

    private Dictionary<string, double> Stats = new Dictionary<string, double>();
    private Dictionary<string, Stat> PersistentStats = new Dictionary<string, Stat>();
    private List<StatProvider> TransientStatProviders = new List<StatProvider>();


    public StatManager(List<StatProvider> persistentStatBonusProviders, List<StatProvider> transientStatBonusProviders) {
        TransientStatProviders = transientStatBonusProviders;

        // Preload PersistentStatBonuses
        foreach (var provider in persistentStatBonusProviders) {
            var bonuses = provider.GetStats();
            foreach (var statBonus in bonuses) {
                AddPersistent(statBonus);
            }
        }

        Recalculate();
    }

    public void Recalculate(object[] args = null) {
        Stats.Clear();

        // Preloading all of the PersistentStatBonuses into the stats
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

    protected abstract void SpecificRecalulate();

    #region Stat Methods
    public double Get(string name) {
        return Stats.Get(name, 0);
    }

    public void Set(string name, double value) {
        Stats.Set(name, value);
    }

    public void Add(string name, double value) {
        var currentValue = Get(name);
        Stats.Set(name, currentValue + value);
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
    public void On(string message, Callback callback) {
        LocalMessenger.On(message, callback);
    }

    public void Un(string message, Callback callback) {
        LocalMessenger.Un(message, callback);
    }
    #endregion
}

[Serializable]
public class Stat {
    #region LocalMessenger
    public const string CHANGED = "CHANGED";

    private LocalMessenger LocalMessenger {
        get { return _LocalMessenger == null ? _LocalMessenger = new LocalMessenger() : _LocalMessenger; }
    }
    [NonSerialized]
    private LocalMessenger _LocalMessenger;
    #endregion

    public string Name;
    public double Value {
        get { return _Value; }
        set { _Value = value; LocalMessenger.Fire(CHANGED, new object[] { }); /*Debug.Log("{0}: {1}".FormatWith(Name, _Value));*/ }
    }
    private double _Value;

    public Stat(string name, double value) {
        Name = name;
        _Value = value;
    }

    #region LocalMessenger Methods
    public void On(string message, Callback callback) {
        LocalMessenger.On(message, callback);
    }

    public void Un(string message, Callback callback) {
        LocalMessenger.Un(message, callback);
    }
    #endregion
}

public interface StatProvider {
    List<Stat> GetStats();
}
