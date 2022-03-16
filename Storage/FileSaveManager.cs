using System;
using System.Collections.Generic;
using UnityEngine;

public class FileSaveManager : MonoBehaviour {
    private static Dictionary<string, FileSaveContainer> FileSaveContainers = new Dictionary<string, FileSaveContainer>();

    public bool IsAutoSaveEnabled = true;
    public float AutoSaveDelay = 30f;
    Timer SaveTimer;


    #region Unity Methods for Autosave, event save, and debug
    void Awake() {
        SaveTimer = new Timer(AutoSaveDelay);
    }

    void Update() {
        if (IsAutoSaveEnabled && SaveTimer!= null && SaveTimer.Check()) {
            SaveTimer.Reset();
            Debug.Log("FileSaveManager: AutoSave");
            SaveAll();
        }
    }

    private void OnDestroy() {
        Debug.Log("FileSaveManager: OnDestroy");
        SaveAll();
    }

    void OnApplicationQuit() {
        Debug.Log("FileSaveManager: Application quit.");
        SaveAll();
    }

    void OnApplicationPause(bool isPaused) {
        if (isPaused) {
            Debug.Log("FileSaveManager: Application paused.");
            SaveAll();
        }
    }
    #endregion
    
    public static T GetOrLoadOrCreate<T>(string fileName, T defaultFileSaveData = default(T)) {
        Debug.Log("FileSaveManager: GetOrLoadOrCreate[{0}]".FormatWith(fileName));

        if (!FileSaveContainers.Has(fileName)) {
            var fileData = FileSaveUtils.Load<T>(fileName);

            if (fileData == null) {
                fileData = defaultFileSaveData;
                Create(fileName, fileData);
            }
            else {
                var fileSaveContainer = new FileSaveContainer {
                    FileName = fileName,
                    Data = fileData,
                    DataType = typeof(T)
                };

                FileSaveContainers.Set(fileSaveContainer.FileName, fileSaveContainer);
            }
        }

        return (T)FileSaveContainers.Get(fileName).Data;
    }

    public static bool Save(string fileName) {
        Debug.Log("FileSaveManager: Save[{0}]".FormatWith(fileName));
        var fileSaveContainer = FileSaveContainers.Get(fileName);

        if (fileSaveContainer == null) {
            return false;
        }

        return FileSaveUtils.Save(fileSaveContainer.FileName, fileSaveContainer.Data, fileSaveContainer.DataType);
    }

    public static bool Create<T>(string fileName, T fileData) {
        Debug.Log("FileSaveManager: Create[{0}]".FormatWith(fileName));
        var fileSaveContainer = new FileSaveContainer {
            FileName = fileName,
            Data = fileData,
            DataType = typeof(T)
        };

        FileSaveContainers.Set(fileSaveContainer.FileName, fileSaveContainer);

        return FileSaveUtils.Save(fileSaveContainer.FileName, fileSaveContainer.Data, fileSaveContainer.DataType);
    }

    public static void SaveAll() {
        Debug.Log("FileSaveManager: SaveAll");
        foreach (var fileSaveContainer in FileSaveContainers.Values) {
            FileSaveUtils.Save(fileSaveContainer.FileName, fileSaveContainer.Data, fileSaveContainer.DataType);
        }
    }

    public static void Delete(string fileName) {
        Debug.Log("FileSaveManager: Delete[{0}]".FormatWith(fileName));

        FileSaveContainers.Remove(fileName);
        FileSaveUtils.Delete(fileName);
    }

    public static void HardReset() {
        Debug.Log("FileSaveManager: HardReset");

        foreach (var fileSaveData in FileSaveContainers.Values) {
            FileSaveUtils.Delete(fileSaveData.FileName);
        }
        FileSaveContainers.Clear();
    }

    public class FileSaveContainer {
        public string FileName; // Must be unique, used as the Dictionary key.
        public object Data;
        public Type DataType;
    }
}
