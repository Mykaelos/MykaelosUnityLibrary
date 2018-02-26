using System;
using System.Collections.Generic;

public abstract class StatManager {
    #region LocalMessenger
    public const string AFTER_RECALCULATE = "StatManager.AFTER_RECALCULATE";
    LocalMessenger LocalMessenger = new LocalMessenger();
    #endregion

    private Dictionary<string, int> Stats = new Dictionary<string, int>();
    private Dictionary<string, StatBonus> PersistentStatBonuses = new Dictionary<string, StatBonus>();
    private List<StatBonusProvider> TransientStatBonusProviders = new List<StatBonusProvider>();


    public StatManager(List<StatBonusProvider> persistentStatBonusProviders, List<StatBonusProvider> transientStatBonusProviders) {
        TransientStatBonusProviders = transientStatBonusProviders;

        // Preload PersistentStatBonuses
        foreach (var provider in persistentStatBonusProviders) {
            var bonuses = provider.GetStatBonuses();
            foreach (var statBonus in bonuses) {
                AddPersistent(statBonus);
            }
        }

        Recalculate();
    }

    public void Recalculate(object[] args = null) {
        Stats.Clear();

        // Preloading all of the PersistentStatBonuses into the stats
        foreach (var statBonusPair in PersistentStatBonuses) {
            var statBonus = statBonusPair.Value;
            Add(statBonus.Name, (int)statBonus.Value);
        }

        foreach (var provider in TransientStatBonusProviders) {
            var bonuses = provider.GetStatBonuses();
            foreach (var statBonus in bonuses) {
                Add(statBonus.Name, (int)statBonus.Value);
            }
        }

        SpecificRecalulate();

        LocalMessenger.Fire(AFTER_RECALCULATE);
    }

    protected abstract void SpecificRecalulate();

    #region Stat Methods
    public int Get(string name) {
        return Stats.Get(name, 0);
    }

    public void Set(string name, int value) {
        Stats.Set(name, value);
    }

    public void Add(string name, int value) {
        var currentValue = Get(name);
        Stats.Set(name, currentValue + value);
    }
    #endregion

    #region Persistent Methods
    // The goal of Persistent StatBonuses is to allow them to be passed by reference, which allows other classes
    // to adjust them permanently. These StatBonuses can then be saved to the file system via the statBonusProviders
    // that provided them.
    public void AddPersistent(StatBonus statBonus) {
        PersistentStatBonuses.Add(statBonus.Name, statBonus);
    }

    public StatBonus GetPersistentReference(string name) {
        return PersistentStatBonuses.Get(name);
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
public class StatBonus {
    public string Name;
    public double Value;

    public StatBonus(string name, double value) {
        Name = name;
        Value = value;
    }
}

public interface StatBonusProvider {
    List<StatBonus> GetStatBonuses();
}
