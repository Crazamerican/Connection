using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager2 : MonoBehaviour
{
    public string fileNum;
    private static GameObject endOfLevel;
    // Start is called before the first frame update
    void Start()
    {
        endOfLevel = GameObject.Find("EndOfLevel");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetFileNum()
    {
        
        if (fileNum == null)
        {
            fileNum = "test";
        }
        endOfLevel.GetComponent<GameManagementScript>().setFileNumber(fileNum);
        endOfLevel.GetComponent<GameManagementScript>().createNewFile();
        SceneManager.LoadScene("HubWorldMusicTest");
    }
}
