using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

public class FileDataHandler 
{
    private string dataDirPath = "";
    private string dataFileName = "";
    private bool useEncription = false;
    private readonly string encryptionCodeWord = "Mondongo";

    public FileDataHandler(string dirPath, string fileName, bool encrypt) 
    {
        dataDirPath = dirPath;
        dataFileName = fileName;
        useEncription = encrypt;
    }
    public GameData Load(string profileID) 
    {
        string fullPath = Path.Combine(dataDirPath, profileID, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath)) 
        {
            try 
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open)) 
                {
                    using(StreamReader reader = new StreamReader(stream)) 
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                if (useEncription)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch(Exception e) 
            {
                Debug.Log("Error de cargado en : " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }
    public void Save(GameData data, string profileID) 
    {
        string fullPath = Path.Combine(dataDirPath, profileID, dataFileName);
        try 
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataToStore = JsonUtility.ToJson(data, true);
            if (useEncription) 
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }
            using(FileStream stream = new FileStream(fullPath, FileMode.Create)) 
            {
                using (StreamWriter writer = new StreamWriter(stream)) 
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch(Exception e)
        {
            Debug.Log("Error de guardado en : " + fullPath + "\n" + e);
        }
    }
    public Dictionary<string, GameData> LoadAllData() 
    {
        Dictionary<string, GameData> profileDictionary = new Dictionary<string, GameData>();

        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(dataDirPath).EnumerateDirectories();
        foreach(DirectoryInfo dirInfo in dirInfos) 
        {
            string profileId = dirInfo.Name;
            string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
            if (!File.Exists(fullPath))
            {
                continue;
            }
            GameData profileData = Load(profileId);
            if(profileData!= null) 
            {
                profileDictionary.Add(profileId, profileData);
            }
        }

        return profileDictionary;
    }
    private string EncryptDecrypt(string data) 
    {
        string modifyData = "";
        for (int i = 0; i < data.Length; i++) 
        {
            modifyData += (char)(data[i] ^ encryptionCodeWord[i%encryptionCodeWord.Length]);
        }
        return modifyData;
    }
}

