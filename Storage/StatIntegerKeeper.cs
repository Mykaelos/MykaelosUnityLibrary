using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class StatIntegerKeeper : SavableData {
    [SerializeField]
    private string _Prefix = "";
    public string Prefix {
        set { _Prefix = value.IsNullOrEmpty() ? "" : value + "."; }
        get { return _Prefix; }
    }
    public LocalMessenger LocalMessenger;
    public List<StatInteger> Stats = new List<StatInteger>();

    [NonSerialized]
    private Dictionary<string, StatInteger> _StatsDictionary;
    public Dictionary<string, StatInteger> StatsDictionary {
        get { return _StatsDictionary == null ? _StatsDictionary = SetupDictionary() : _StatsDictionary; }
    }

    Dictionary<string, StatInteger> SetupDictionary() {
        var statsDictionary = new Dictionary<string, StatInteger>();
        var stats = new List<StatInteger>(Stats);
        for (int i = 0; i < stats.Count; i++) {
            var existingStat = statsDictionary.Get(stats[i].Key);
            if (existingStat != null) {
                Debug.Log(string.Format("ERROR: Duplicate Stat in StatIntegerKeeper: {0}. existingValue = {1}, newValue = {2}. REMOVING.", existingStat.Key, existingStat.Value, Stats[i].Value));
                Stats.Remove(stats[i]);
                continue;
            }
            statsDictionary.Add(stats[i].Key, stats[i]);
        }
        return statsDictionary;
    }


    public StatIntegerKeeper() { }
    public StatIntegerKeeper(string prefix, LocalMessenger localMessenger) {
        Prefix = prefix;
        LocalMessenger = localMessenger;
    }

    public void Set(string key, int value) {
        var stat = Get(key);
        int previous = stat.Value;
        stat.Value = value;
        Fire(stat.Key, EVENT_UPDATED, new object[] { stat.Value, previous });
    }

    public void Set(List<StatInteger> statIntegers) {
        for (int i = 0; i < statIntegers.Count; i++) {
            Set(statIntegers[i].Key, statIntegers[i].Value);
        }
    }

    public bool Add(string key, int amount, int maximum = int.MaxValue, bool fillToMax = true) {
        var stat = Get(key);
        int roomAvailable = maximum - stat.Value;

        if (!fillToMax && roomAvailable < amount) {
            return false;
        }

        int previous = stat.Value;
        stat.Value = roomAvailable >= amount ? stat.Value + amount : maximum;

        int difference = stat.Value - previous;
        var args = new object[] { stat.Value, amount, difference };
        Fire(stat.Key,EVENT_ADDED, args);
        Fire(stat.Key, EVENT_UPDATED, args);

        return true;
    }

    public bool Subtract(string key, int amount, int minimum = 0) {
        var stat = Get(key);
        int roomAvailable = stat.Value - minimum;

        int previous = stat.Value;
        stat.Value = roomAvailable >= amount ? stat.Value - amount : minimum;

        int difference = stat.Value - previous;
        var args = new object[] { stat.Value, amount, difference };
        Fire(stat.Key, EVENT_SUBTRACTED, args);
        Fire(stat.Key, EVENT_UPDATED, args);

        return stat.Value == minimum;
    }

    public bool Spend(string key, int amount, int minimum = 0) {
        var stat = Get(key);
        int roomAvailable = stat.Value - minimum;

        if (roomAvailable < amount) {
            return false;
        }

        int previous = stat.Value;
        stat.Value = roomAvailable >= amount ? stat.Value - amount : minimum;

        int difference = stat.Value - previous;
        var args = new object[] { stat.Value, amount, difference };
        Fire(stat.Key, EVENT_SPENT, args);
        Fire(stat.Key, EVENT_UPDATED, args);

        return false;
    }

    public bool CanSpend(string key, int amount, int minimum = 0) {
        var stat = Get(key);
        return stat.Value - amount >= minimum;
    }

    public StatInteger Get(string key, int defaultValue = 0) {
        StatInteger stat = StatsDictionary.Get(key);
        if (stat == null) {
            stat = new StatInteger(key, defaultValue);
            StatsDictionary.Add(stat.Key, stat);
            Stats.Add(stat);
        }
        return stat;
    }

    void Fire(string statKey, string functionName, object[] args) {
        if (LocalMessenger == null) {
            Messenger.Fire("{0}{1}{2}".FormatWith(Prefix, statKey, functionName), args);
        }
        else {
            LocalMessenger.Fire("{0}{1}".FormatWith(statKey, functionName), args);
        }
    }

    #region Constants
    public const string EVENT_UPDATED = ".EVENT_UPDATED";
    public const string EVENT_ADDED = ".EVENT_ADDED";
    public const string EVENT_SUBTRACTED = ".EVENT_SUBTRACTED";
    public const string EVENT_SPENT = ".EVENT_SPENT";
    #endregion

    #region SavableData
    public override bool HasData() {
        return Stats != null && Stats.Count > 0;
    }
    public override void PrepareDataAfterLoad() { }
    public override void PrepareDataForSave() { }
    #endregion
}

[Serializable]
public class StatInteger {
    public string Key;
    public int Value;


    public StatInteger(string key, int value) {
        Key = key;
        Value = value;
    }
}
