using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveScore(SnakeGameManager manager) // Saves the file to a xml file
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Score.xml";

        FileStream fileStream = new FileStream(path, FileMode.Create);

        SaveAndLoad saveAndLoad = new SaveAndLoad(manager);

        binaryFormatter.Serialize(fileStream, saveAndLoad);
        fileStream.Close();
        Debug.Log("Succesfully saved data");
    }

    public static SaveAndLoad LoadScore() // Loads the data from a file if it exists
    {
        string path = Application.persistentDataPath + "/Score.xml";
        if(File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);

            SaveAndLoad score = binaryFormatter.Deserialize(fileStream) as SaveAndLoad;

            fileStream.Close();
            Debug.Log("Succesfully loaded data");
            return score;
        }
        else
        {
            Debug.LogError("Save file not existing");
            return null;
        }
    }

}
