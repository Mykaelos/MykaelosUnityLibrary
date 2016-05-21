using UnityEngine;
using System.Collections.Generic;
using System;

public class SaveManager : MonoBehaviour {
    public static Dictionary<Type, object> Data = new Dictionary<Type, object>();

    public float AutoSaveDelay = 30f;
    Timer SaveTimer;


    void Awake() {
        SaveTimer = new Timer(AutoSaveDelay);
    }

    void Update() {
        if (SaveTimer.Check()) {
            SaveTimer.Reset();
            Save();
        }

        if (Application.isEditor && Input.GetKeyDown(KeyCode.H)) {
            HardReset();
        }
    }

    void OnApplicationQuit() {
        //Debug.Log("SaveManager: Application quit.");
        Save();
    }

    void OnApplicationPause(bool isPaused) {
        if (isPaused) {
            //Debug.Log("SaveManager: Application paused.");
            Save();
        }
    }

    //void OnApplicationFocus(bool hasFocus) {
    //    if (!hasFocus) {
    //        Debug.Log("SaveManager: Application lost focus.");
    //        Save();
    //    }
    //}

    public static void Set<T>(T data) where T : class {
        Type type = typeof(T);
        if (Data.ContainsKey(type)) {
            Data[type] = data;
        }
        else {
            Data.Add(type, data);
        }
    }

    public static T Get<T>() where T : class {
        T ret = null;
        try {
            ret = Data[typeof(T)] as T;
        }
        catch (KeyNotFoundException) {
        }
        return ret;
    }

    public static bool IsLoaded() {
        return Data.Count > 0;
    }

    public static void Load(List<Type> keys, bool forceReload = false) {
        if (IsLoaded() && !forceReload) {
            //Debug.Log("SaveManager: Already Loaded.");
            return;
        }

        Data.Clear();
        //Debug.Log("SaveManager: Loading data.");

        for (int i = 0; i < keys.Count; i++) {
            Type type = keys[i];
            string name = type.Name;

            object data;
            string prefsString = PlayerPrefs.GetString(name);
            if (string.IsNullOrEmpty(prefsString)) {
                data = (object)Activator.CreateInstance(type);
                //Debug.Log("SaveManager: Creating a new " + name);
            }
            else {
                //Debug.Log(name + ": " + prefsString);
                data = JsonUtility.FromJson(prefsString, type);
            }

            if (data is SavableData) {
                ((SavableData)data).PrepareDataAfterLoad();
            }

            Data.Add(type, data);
        }
    }

    public static void Save() {
        if (Data.IsNullOrEmpty()) {
            Debug.Log("SaveManager.Save: Data is Empty");
            return;
        }
        int size = 0;

        List<Type> keys = new List<Type>(Data.Keys);

        for (int i = 0; i < keys.Count; i++) {
            Type type = keys[i];
            object data = Data[type];

            if (data != null && data is SavableData && ((SavableData)data).HasData()) {
                ((SavableData)data).PrepareDataForSave();
            }

            string name = type.Name;
            string json = JsonUtility.ToJson(data);
            size += System.Text.ASCIIEncoding.Unicode.GetByteCount(json);
            //Debug.Log(name + ": " + json);
            PlayerPrefs.SetString(name, json);

            if (data is SavableData) {
                //Debug.Log("Cleaning data after save");
                ((SavableData)data).PrepareDataAfterLoad();
                Data[type] = data;
            }
        }

        Debug.Log("SaveManager: Saving data. Size: " + size.ToString("N0"));
        PlayerPrefs.Save();
    }

    public static void HardReset() {
        Debug.Log("SaveManager: Hard Reset.");

        List<Type> keys = new List<Type>(Data.Keys);

        for (int i = 0; i < keys.Count; i++) {
            Type type = keys[i];

            string name = type.Name;
            Debug.Log("Deleting " + name);
            PlayerPrefs.DeleteKey(name);
        }

        Data.Clear();
        PlayerPrefs.Save();
    }
}
