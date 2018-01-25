using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class FileSaveUtils<T> {

    public static bool Save(string fileName, T data) {
        bool wasSavedSuccessfully = false;
        FileStream fileStream = null;
        string filePath = string.Format("{0}/{1}.dat", Application.persistentDataPath, fileName);

        try {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            fileStream = File.Open(filePath, FileMode.Create); // Create will either create a new file, or empty and existing one.
            binaryFormatter.Serialize(fileStream, data);
            fileStream.Close();
            wasSavedSuccessfully = true;
        }
        catch(Exception e) {
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
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                fileStream = File.Open(filePath, FileMode.Open);

                data = (T)binaryFormatter.Deserialize(fileStream);
            }
            else {
                Debug.Log(string.Format("Failed to load File {0}. It does not exist.", fileName));
            }
        }
        catch(Exception e) {
            Debug.LogWarning("Failed to load file: " + fileName + "\n" + e.Message);
        }
        finally {
            if (fileStream != null) {
                fileStream.Close();
            }
        }

        return data;
    }
}
