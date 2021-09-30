using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;

public class FileSaveUtils<T> {

    public static bool Save(string fileName, T data) {
        if (data == null) {
            Debug.LogWarning("Failed to save file: " + fileName + ". Null data cannot be saved.");
            return false;
        }

        bool wasSavedSuccessfully = false;
        FileStream fileStream = null;
        string filePath = string.Format("{0}/{1}.dat", Application.persistentDataPath, fileName);

        try {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            // FileMode.Create either creates a new file, or replaces the previous one. 
            // This destroys the file on Open(), so make sure the new data is a good replacement!
            fileStream = File.Open(filePath, FileMode.Create);
            serializer.WriteObject(fileStream, data);
            fileStream.Close();
            wasSavedSuccessfully = true;
        }
        catch (Exception e) {
            Debug.LogWarning("Failed to save file: " + fileName + "\n" + e.Message);
        }
        finally {
            if (fileStream != null) {
                fileStream.Close();
            }
        }

        return wasSavedSuccessfully;
    }

    public static T Load(string fileName) {
        FileStream fileStream = null;
        string filePath = string.Format("{0}/{1}.dat", Application.persistentDataPath, fileName);
        T data = default(T);

        try {
            if (File.Exists(filePath)) {
                DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                fileStream = File.Open(filePath, FileMode.Open);
                data = (T)serializer.ReadObject(fileStream);
            }
            else {
                Debug.Log(string.Format("Failed to load File {0}. It does not exist.", fileName));
            }
        }
        catch (Exception e) {
            Debug.LogWarning("Failed to load file: " + fileName + "\n" + e.Message);
        }
        finally {
            if (fileStream != null) {
                fileStream.Close();
            }
        }

        return data;
    }

    public static void Delete(string fileName) {
        string filePath = string.Format("{0}/{1}.dat", Application.persistentDataPath, fileName);

        try {
            if (File.Exists(filePath)) {
                File.Delete(filePath);
            }
            else {
                Debug.Log(string.Format("Failed to delete File {0}. It does not exist.", fileName));
            }
        }
        catch (Exception e) {
            Debug.LogWarning("Failed to load file: " + fileName + "\n" + e.Message);
        }
    }

    public static string FileSize(string fileName) {
        string filePath = string.Format("{0}/{1}.dat", Application.persistentDataPath, fileName);
        var fileInfo = new FileInfo(filePath);
        float kb = fileInfo.Length / 1024f;
        return "{0:N2} KB".FormatWith(kb);
    }
}
