using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveSlime()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/slime.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        DataHolder data = new DataHolder();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void RespawnSlime(int ph, int hum, int hung, int energy)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/slime.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        DataHolder data = new DataHolder(ph, hum, hung, energy);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    
    public static DataHolder LoadSlime()
    {
        string path = Application.persistentDataPath + "/slime.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            DataHolder data = formatter.Deserialize(stream) as DataHolder;
            
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}