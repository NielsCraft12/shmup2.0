using System;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string fileName = "gameSave.dat";

        public static void SerializeData(SaveData data)
        {
            string path = Path.Combine(Application.persistentDataPath, fileName);
            string json = JsonUtility.ToJson(data);

            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(json);
            }

            Debug.Log("Saved game to: " + path);
        }





    public static SaveData DeSerializeData()
   {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(path))
        {
        return ReadSafeFile();
        }
        else
        {
            //Debug.LogError("Save file not found in: " + path);
            ScoreBord.instance.SaveGame();
            return ReadSafeFile();
        }     
   }

    private static SaveData ReadSafeFile()
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        using (StreamReader reader = new StreamReader(path))
        {
            string json = reader.ReadToEnd();
            SaveData data = JsonUtility.FromJson<SaveData>(json);
           // Debug.Log("Save Loaded from: " + path);
            return data;
        }
    }

 
}
