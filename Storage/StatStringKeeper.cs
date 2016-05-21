using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class StatStringKeeper : SavableData {
    [SerializeField]
    private string _Prefix = "";
    public string Prefix {
        set { _Prefix = value.IsNullOrEmpty() ? "" : value + "."; }
        get { return _Prefix; }
    }
    public LocalMessenger LocalMessenger;
    public List<StatString> Stats = new List<StatString>();

    [NonSerialized]
    private Dictionary<string, StatString> _StatsDictionary;
    public Dictionary<string, StatString> StatsDictionary {
        get { return _StatsDictionary == null ? _StatsDictionary = SetupDictionary() : _StatsDictionary; }
    }

    Dictionary<string, StatString> SetupDictionary() {
        var statsDictionary = new Dictionary<string, StatString>();
        var stats = new List<StatString>(Stats);
        for (int i = 0; i < stats.Count; i++) {
            var existingStat = statsDictionary.Get(stats[i].Key);
            if (existingStat != null) {
                Debug.Log(string.Format("ERROR: Duplicate Stat in StatStringKeeper: {0}. existingValue = {1}, newValue = {2}. REMOVING.", existingStat.Key, existingStat.Value, Stats[i].Value));
                Stats.Remove(stats[i]);
                continue;
            }
            statsDictionary.Add(stats[i].Key, stats[i]);
        }
        return statsDictionary;
    }


    public StatStringKeeper() { }
    public StatStringKeeper(string prefix, LocalMessenger localMessenger) {
        Prefix = prefix;
        LocalMessenger = localMessenger;
    }

    public void Set(string key, string value) {
        var stat = Get(key);
        string previous = stat.Value;
        stat.Value = value;
        var args = new object[] { stat.Value, previous };
        Fire(stat.Key, "Updated", args);
    }

    public void Set(List<StatString> statStrings) {
        for(int i = 0; i < statStrings.Count; i++) {
            Set(statStrings[i].Key, statStrings[i].Value);
        }
    }

    public StatString Get(string key, string defaultValue = default(string)) {
        StatString stat = StatsDictionary.Get(key);
        if (stat == null) {
            stat = new StatString(key, defaultValue);
            StatsDictionary.Add(stat.Key, stat);
            Stats.Add(stat);
        }
        return stat;
    }

    public bool Has(string key) {
        return StatsDictionary.ContainsKey(key);
    }

    void Fire(string statKey, string functionName, object[] args) {
        if (LocalMessenger == null) {
            Messenger.Fire("{0}{1}.{2}".FormatWith(Prefix, statKey, functionName), args);
        }
        else {
            LocalMessenger.Fire("{0}{1}.{2}".FormatWith(Prefix, statKey, functionName), args);
        }
    }

    #region SavableData
    public override bool HasData() {
        return Stats != null && Stats.Count > 0;
    }
    public override void PrepareDataAfterLoad() { }
    public override void PrepareDataForSave() { }
    #endregion
}

[Serializable]
public class StatString {
    public string Key;
    public string Value;


    public StatString(string key, string value) {
        Key = key;
        Value = value;
    }
}
