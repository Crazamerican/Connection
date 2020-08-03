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
    private ArrayList levelNames = new ArrayList()
    {
        "NewGraphics1", "NewGraphics2", "NewGraphics3", "NewGraphics4", "NewGraphics5", "NewGraphics6", "NewGraphics7"
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
        if (levelsCleared.TryGetValue(level, out LevelData data)){
            return data;
        }
        else
        {
            data = new LevelData();
            data.levelCleared = false;
            data.trophyGot = false;
            levelsCleared.Add(level, data);
            return data;
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

}
