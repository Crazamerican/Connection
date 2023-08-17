using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSaver : MonoBehaviour
{
    public Dictionary<String, LevelData> levelsCleared = new Dictionary<String, LevelData>();
    private static List<String> levelNames = new List<string>()
    {
        //"NewGraphics1", "NewGraphics2", "NewGraphics3", "NewGraphics4", "NewGraphics5", "NewGraphics6", "NewGraphics7", "Level1"
        "Level1", "NewGraphics2", "NewGraphics3", "NewGraphics4", "NewGraphics5", "NewGraphics6", "NewGraphics7"
    };
    

    public void SaveGame(String fileNum)
    {
        Debug.Log("saving the game to " + Application.persistentDataPath + "/GameSaveFile" + fileNum + ".dat");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/GameSaveFile" + fileNum + ".dat");
        SaveData data = new SaveData();
        data.levelsData = this.levelsCleared;
        bf.Serialize(file, data);
        file.Close();

    }


    public ClearPercent getClearPercent(String fileNum)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/GameSaveFile" + fileNum + ".dat", FileMode.Open);
        SaveData data = (SaveData)bf.Deserialize(file);
        file.Close();
        levelsCleared = data.levelsData;
        ClearPercent clearpercent = new ClearPercent();
        int clearedLevels = 0;
        int trophies = 0;
        foreach (var values in levelsCleared)
        {
            if (values.Value.levelCleared)
            {
                clearedLevels++;
            }
            if(values.Value.trophyGot)
            {
                trophies++;
            }
        }
        clearpercent.levelsCleared = levelsCleared.Keys.Count / clearedLevels;
        clearpercent.trophiesAcquired = levelsCleared.Keys.Count / trophies;
        levelsCleared.Clear();
        return clearpercent;
    }

    private void SaveGame(String fileNum, SaveData saveData)
    {
        Debug.Log("saving the game to " + Application.persistentDataPath + "/GameSaveFile" + fileNum + ".dat");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/GameSaveFile" + fileNum + ".dat");
        bf.Serialize(file, saveData);
        file.Close();
    }

    public void LoadGame(string fileNum)
    {
        if(File.Exists(Application.persistentDataPath + "/GameSaveFile" + fileNum + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/GameSaveFile" + fileNum + ".dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            levelsCleared = data.levelsData;
            Debug.Log("The loaded file information is: " + levelsCleared);
        }
    }

    public void SaveLevelData(bool gotTrophy, bool levelCleared)
    {
        Scene scene = SceneManager.GetActiveScene();
        string level = scene.name;
        if(level.Equals("Level1_Cutscene"))
        {
            level = "Level1";
        }
        if(levelsCleared.TryGetValue(level, out LevelData data))
        {
            data.trophyGot = gotTrophy;
            data.levelCleared = levelCleared;

        }
    }


    public LevelData GetLevelData()
    {
        Scene scene = SceneManager.GetActiveScene();
        string level = scene.name;
        if(level.Equals("Level1_Cutscene"))
        {
            level = "Level1";
        }
        if (levelsCleared.TryGetValue(level, out LevelData data)){
            return data;
        }
        else
        {
            data = new LevelData();
            data.levelCleared = false;
            data.trophyGot = false;
            data.checkPoint = 0;
            levelsCleared.Add(level, data);
            return data;
        }
    }

    public bool CheckStartingLocation()
    {
        Scene scene = SceneManager.GetActiveScene();
        String level = scene.name;
        if(levelsCleared.TryGetValue(level, out LevelData data))
        {
            if(data.topCharacterStartPosition != null && data.bottomCharacterStartPosition != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void tryUnlockNextLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        string level = scene.name;
        Debug.Log("current level name: " + level);
        int curLevelInt = levelNames.IndexOf(level);
        Debug.Log("Current level index: " + curLevelInt);
        if(levelNames.Count > curLevelInt)
        {
            int nextLevelInt = ++curLevelInt;
            Debug.Log("The next level index is: " + nextLevelInt);
            string nextLevel = (string)levelNames[nextLevelInt];
            LevelData nextLevelData = GetLevelData(nextLevel);
            Debug.Log("trying to unlock level: " + nextLevel);
            nextLevelData.levelUnlocked = true;
        } else
        {
            Debug.Log("Something goofed and hit the last level");
        }
    }

    public bool levelUnlocked(string levelName)
    {
        if (levelsCleared.TryGetValue(levelName, out LevelData data))
        {
            return data.levelUnlocked;
        }
        else
        {
            return false;
        }
    }

    public LevelData GetLevelData(string levelName)
    {
        if (levelsCleared.TryGetValue(levelName, out LevelData data))
        {
            return data;
        }
        else
        {
            data = new LevelData();
            data.levelCleared = false;
            data.trophyGot = false;
            data.levelUnlocked = false;
            data.checkPoint = 0;
            data.topCharacterStartPosition = new SerializableVector3(0, 0, 0);
            data.bottomCharacterStartPosition = new SerializableVector3(0, 0, 0);
            levelsCleared.Add(levelName, data);
            return data;
        }
    }

    public void createNewLevelData(string fileNum)
    {
        SaveData saveData = new SaveData();
        for(int i = 0; i < levelNames.Count; i++)
        {
            string name = (string)levelNames[i];
            LevelData levelData = new LevelData();
            levelData.levelUnlocked = false;
            levelData.levelCleared = false;
            levelData.trophyGot = false;
            levelData.checkPoint = 0;
            if(i == 0)
            {
                Debug.Log("The first level has been unlocked: " + name);
                levelData.levelUnlocked = true;
            }
            saveData.levelsData.Add(name, levelData);
        }
        SaveGame(fileNum, saveData);

    }

}

public class ClearPercent
{
    public float levelsCleared;
    public float trophiesAcquired;
}

[Serializable]
class SaveData
{
    public Dictionary<String, LevelData> levelsData { get; set; }
    public SaveData()
    {
        levelsData = new Dictionary<string, LevelData>();
    }
}
[Serializable]
public class LevelData
{
    public Boolean levelUnlocked { get; set; }
    public Boolean levelCleared { get;  set; }
    public Boolean trophyGot { get;  set; }
    public int checkPoint { get; set; }
    public SerializableVector3 topCharacterStartPosition { get; set; }
    public SerializableVector3 bottomCharacterStartPosition { get; set; }
}


[Serializable]
public class SerializableVector3
{
    public float x;
    public float y;
    public float z;
    public SerializableVector3(float rx, float ry, float rz)
    {
        x = rx;
        y = ry;
        z = rz;
    }

    public override string ToString()
    {
        return String.Format("[{0}, {1}, {2}]", x, y, z);
    }

    public static implicit operator Vector3(SerializableVector3 rValue)
    {
        return new Vector3(rValue.x, rValue.y, rValue.z);
    }

    public static implicit operator SerializableVector3(Vector3 rValue)
    {
        return new SerializableVector3(rValue.x, rValue.y, rValue.z);
    }


}
