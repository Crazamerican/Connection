using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public string fileNum;
    private static GameObject endOfLevel;
    // Start is called before the first frame update
    void Start()
    {
        endOfLevel = GameObject.Find("EndOfLevel");
        if (!System.IO.File.Exists(Application.persistentDataPath + "/GameSaveFile" + fileNum + ".dat"))
        {
            gameObject.GetComponent<Button>().interactable = false;
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
            endOfLevel.GetComponent<GameManagementScript>().setFileNumber(fileNum);
        }
        SceneManager.LoadScene("HubWorldMusicTest");
    }
}
