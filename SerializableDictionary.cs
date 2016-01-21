using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
public class SerializableDictionary<K, V> {
    public List<SerializableDictionaryPair<K, V>> List;


    [NonSerialized]
    public Dictionary<K, int> _Dictionary;
    public Dictionary<K, int> Dictionary {
        get { if (_Dictionary == null) { PrepareDictionary(); } return _Dictionary; }
    }

    public void PrepareDictionary() {
        _Dictionary = new Dictionary<K, int>();
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

    public V Get(K key) {
        int index = Dictionary.Get(key, -1);
        if(index == -1) {
            return default(V);
        }

        return List[index].Value;
    }
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
