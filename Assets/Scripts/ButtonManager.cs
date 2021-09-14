﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public string fileNum;
    private static GameObject endOfLevel;
    public bool isLoadButton = false;
    // Start is called before the first frame update
    void Start()
    {
        endOfLevel = GameObject.Find("EndOfLevel");
        if(isLoadButton){
            if (!System.IO.File.Exists(Application.persistentDataPath + "/GameSaveFile" + fileNum + ".dat"))
            {
                gameObject.GetComponent<Button>().interactable = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetFileNum()
    {
        if (fileNum != null)
        {
            endOfLevel.GetComponent<GameManagementScript>().SetFileNumber(fileNum);
            endOfLevel.GetComponent<GameManagementScript>().LoadGame();
        }
        if(!isLoadButton)
        {
            endOfLevel.GetComponent<GameManagementScript>().CreateNewFile();
            endOfLevel.GetComponent<GameManagementScript>().LoadGame();
        }
        SceneManager.LoadScene("UILevelSelect");
    }
    public void SetThisButtonAsFirstUsable()
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(gameObject);
    }
}
