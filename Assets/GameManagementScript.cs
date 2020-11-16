using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagementScript : MonoBehaviour
{

    //GameManager
    public static GameManagementScript Instance { get; private set; }
    private GameSaver gameSaver;
    private string fileNum;
    public bool unlock { get; set; }
    public List<GameObject> checkPoints;
    //Create the singleton
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Debug.Log("Warning: multiple " + this + " in scene!");
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        if (gameSaver == null)
        {
            gameSaver = new GameSaver();
        }
        unlock = false;
    }

    public void SetFileNumber(string filNum)
    {
        Debug.Log("Setting the file Number: " + filNum);
        this.fileNum = filNum;
    }

    public void LoadGame()
    {
        gameSaver.LoadGame(this.fileNum);
    }

    public LevelData getLevelData(string levelName)
    {
        return gameSaver.GetLevelData(levelName);
    }

    public void SaveLevel()
    {
        if(fileNum == null)
        {
            fileNum = "test";
        }
        gameSaver.SaveGame(fileNum);
    }
    
    public void UpdateLevelDataTrophy()
    {
        LevelData levelData = gameSaver.GetLevelData();
        if(levelData == null)
        {
            levelData = new LevelData();
        }
        levelData.trophyGot = true;
    }
     public LevelData GetLevelData()
    {
        return gameSaver.GetLevelData();
    }

    public void UpdateLevelDataCleared()
    {
        LevelData levelData = gameSaver.GetLevelData();
        if (levelData == null)
        {
            levelData = new LevelData();
        }
        levelData.levelCleared = true;
    }

    public void UnlockNextLevel()
    {
        gameSaver.tryUnlockNextLevel();
    }

    public bool levelUnlocked(string levelName)
    {
        return gameSaver.levelUnlocked(levelName);
    }

    public void CreateNewFile()
    {
        Debug.Log("Creating New FIle");
        if(fileNum == null)
        {
            fileNum = "test";
        }
        gameSaver.createNewLevelData(fileNum);
    }
    
    public (Vector3 p1, Vector3 p2) GetCheckPointPositionToMovePlayersTo()
    {
        Vector3 topCharPos = GetLevelData().topCharacterStartPosition;
        Vector3 botCharPos = GetLevelData().bottomCharacterStartPosition;
        return (topCharPos, botCharPos);
    }
    

}
