
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;



public static class SaveSystem
{
    public static void SaveStoryManager(StoryManager storyManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/erosion.story";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(storyManager);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/erosion.story";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("save file not found");
            return null;
        }
    }

    public static bool SaveDataExists()
    {
        string path = Application.persistentDataPath + "/erosion.story";

        if (File.Exists(path))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
