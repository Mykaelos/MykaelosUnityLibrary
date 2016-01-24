using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
public class SerializableDictionary<K, V> : SavableData {
    public List<SerializableDictionaryPair<K, V>> List;


    [NonSerialized]
    public Dictionary<K, int> _Dictionary;
    public Dictionary<K, int> Dictionary {
        get { if (_Dictionary == null) { PrepareDictionary(); } return _Dictionary; }
    }

    public SerializableDictionary() { }

    public void PrepareDictionary() {
        _Dictionary = new Dictionary<K, int>();
        if(List == null) {
            List = new List<SerializableDictionaryPair<K, V>>();
        }
        for(int i = 0; i < List.Count; i++) {
            _Dictionary.Add(List[i].Key, i);
        }
    }

    public void Add(K key, V value) {
        if(Dictionary.ContainsKey(key)) {
            Debug.Log("SerializableDictionary: Already contains " + key);
            return;
        }

        List.Add(new SerializableDictionaryPair<K, V>(key, value));
        Dictionary.Add(key, List.Count - 1);
    }

    public void Remove(K key) {
        if (!Dictionary.ContainsKey(key)) {
            Debug.Log("SerializableDictionary: Does not contain " + key);
            return;
        }

        int index = Dictionary.Get(key);
        List.RemoveAt(index);
        Dictionary.Remove(key);
    }

    public V Get(K key, V defaultValue = default(V)) {
        int index = Dictionary.Get(key, -1);
        if(index == -1) {
            return defaultValue;
        }

        return List[index].Value;
    }

    public bool Has(K key) {
        int index = Dictionary.Get(key, -1);
        return index != -1;
    }

    #region SavableData
    public override bool HasData() {
        return List != null && List.Count > 0;
    }
    public override void PrepareDataAfterLoad() { }
    public override void PrepareDataForSave() { }
    #endregion
}

[Serializable]
public class SerializableDictionaryPair<K, V> {
    public K Key;
    public V Value;

    public SerializableDictionaryPair(K key, V value) {
        Key = key;
        Value = value;
    }
}
