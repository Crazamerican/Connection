using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagementScript : MonoBehaviour
{

    //GameManager
    public static GameManagementScript Instance { get; private set; }
    private GameSaver gameSaver;
    private string fileNum;
    public bool unlock { get; set; }
    public List<GameObject> checkPoints;
    public bool freezePlayer;
    public Animator screenTransition;
    bool isTransitioning;
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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += ResetScreenTransition;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= ResetScreenTransition;
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
        Debug.Log(gameSaver);
        return gameSaver.GetLevelData();
    }

    public bool CheckStartingPoints()
    {
        return gameSaver.CheckStartingLocation();
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
        Vector3 topCharPos = gameSaver.GetLevelData().topCharacterStartPosition;
        Vector3 botCharPos = gameSaver.GetLevelData().bottomCharacterStartPosition;
        Debug.Log(topCharPos);
        Debug.Log(botCharPos);
        return (topCharPos, botCharPos);
    }

    public void UpdateLocations(Vector3 top, Vector3 bottom)
    {
        LevelData levelData = gameSaver.GetLevelData();
        levelData.bottomCharacterStartPosition = bottom;
        levelData.topCharacterStartPosition = top;
        //gameSaver.updateLocations(top, bottom);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(gameSaver.GetLevelData().bottomCharacterStartPosition + " :::: " + gameSaver.GetLevelData().topCharacterStartPosition);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            gameSaver.GetLevelData().bottomCharacterStartPosition = new Vector3(0, 0, 0);
            gameSaver.GetLevelData().topCharacterStartPosition = new Vector3(0, 0, 0);
            Debug.Log(gameSaver.GetLevelData().bottomCharacterStartPosition + " :::: " + gameSaver.GetLevelData().topCharacterStartPosition);
        }
    }

    //helper method to load level, so we can do things like scene transitions
    public void LoadLevel(string levelName)
    {
        freezePlayer = true;

        if (!isTransitioning)
        {
            int transitionToChoose = Random.Range(1, 3);
            Debug.Log("Transition: " + transitionToChoose);
            screenTransition.Play("ScreenClose" + transitionToChoose);
            StartCoroutine(LevelTransition(levelName));
        }

        isTransitioning = true;
    }

    public void ResetScreenTransition(Scene sceneName, LoadSceneMode mode)
    {
        Camera mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        GetComponent<Canvas>().worldCamera = mainCam;
        int transitionToChoose = Random.Range(1, 3);
        screenTransition.Play("ScreenOpen" + transitionToChoose);
        isTransitioning = false;
    }

    private IEnumerator LevelTransition(string levelName) //probably don't do as Coroutuine
    {
        Debug.Log("level transitioning");
        yield return new WaitForSecondsRealtime(.5f); //TODO: change this to animation time

        SceneManager.LoadScene(levelName);
        freezePlayer = false;
        
    }

    public void ScreenTransitionToBlack()
    {
        int transitionToChoose = Random.Range(1, 3);
        Debug.Log("Transition: " + transitionToChoose);
        screenTransition.Play("ScreenClose" + transitionToChoose);

    }

    public void ScreenTransitionUp()
    {
        int transitionToChoose = Random.Range(1, 3);
        screenTransition.Play("ScreenOpen" + transitionToChoose);
    }

}
