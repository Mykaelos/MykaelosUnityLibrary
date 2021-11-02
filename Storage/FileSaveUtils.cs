using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class FileSaveUtils {

    public static bool Save<T>(string fileName, T data) {
        return Save(fileName, data, typeof(T));
    }

    public static bool Save(string fileName, object data, Type dataType) {
        if (data == null) {
            Debug.LogWarning("Failed to save file: " + fileName + ". Null data cannot be saved.");
            return false;
        }

        bool wasSavedSuccessfully = false;
        FileStream fileStream = null;
        string filePath = string.Format("{0}/{1}.dat", Application.persistentDataPath, fileName);

        try {
            // BE CAREFUL TO ONLY USE THIS XmlSerializer CONSTRUCTOR. Other constructors do not reuse their assemblies,
            // which can cause a memory leak. https://docs.microsoft.com/en-us/dotnet/api/system.xml.serialization.xmlserializer?view=net-5.0#dynamically-generated-assemblies
            XmlSerializer serializer = new XmlSerializer(dataType);

            // FileMode.Create either creates a new file, or replaces the previous one. 
            // This destroys the file on Open(), so make sure the new data is a good replacement!
            fileStream = File.Open(filePath, FileMode.Create);

            serializer.Serialize(fileStream, data);

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

    public static T Load<T>(string fileName) {
        FileStream fileStream = null;
        string filePath = string.Format("{0}/{1}.dat", Application.persistentDataPath, fileName);
        T data = default(T);

        try {
            if (File.Exists(filePath)) {
                XmlSerializer serializer = new XmlSerializer(typeof(T));

                fileStream = File.Open(filePath, FileMode.Open);

                data = (T)serializer.Deserialize(fileStream);
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
