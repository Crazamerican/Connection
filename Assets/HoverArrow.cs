using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoverArrow : MonoBehaviour
{
    private static GameObject endOfLevel;
    //0 for NewLoadQuit, 1 for NewGame, 2 for LoadGame
    public int screen = 0;
    public int timer = 4;
    int direction = -1;
    int curTime = 0;
    int arrowPos = 0;
    public GameObject firstOption;
    public GameObject secondOption;
    public GameObject thirdOption;
    bool change;
    public int changeTime;
    int count;
    private HoverEnum position = HoverEnum.FIRST_OPTION;

    public GameObject newLoadQuitPanel;
    public GameObject newGameSelect;
    public GameObject loadGameSelect;

    public GameObject firstButtonNew;
    public GameObject firstButtonLoad;
    // Start is called before the first frame update
    void Start()
    {
        endOfLevel = GameObject.Find("EndOfLevel");
        count = 0;
        change = false;
        arrowPos = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            arrowPos = 0;
            transform.position = new Vector3(transform.position.x, firstOption.transform.position.y + 2.1f);
            position = HoverEnum.FIRST_OPTION;
            if (screen == 0)
            {
                if (position == HoverEnum.FIRST_OPTION)
                {
                    HandleNewGameMenu();
                }
                if (position == HoverEnum.SECOND_OPTION)
                {
                    HandleLoadGameMenu();
                }
                if (position == HoverEnum.THIRD_OPTION)
                {
                    Application.Quit();
                }
            }
            else if (screen == 2)
            {
                if (position == HoverEnum.FIRST_OPTION)
                {
                    endOfLevel.GetComponent<GameManagementScript>().SetFileNumber("1");
                    endOfLevel.GetComponent<GameManagementScript>().LoadGame();
                }
                if (position == HoverEnum.SECOND_OPTION)
                {
                    endOfLevel.GetComponent<GameManagementScript>().SetFileNumber("2");
                    endOfLevel.GetComponent<GameManagementScript>().LoadGame();
                }
                if (position == HoverEnum.THIRD_OPTION)
                {
                    endOfLevel.GetComponent<GameManagementScript>().SetFileNumber("3");
                    endOfLevel.GetComponent<GameManagementScript>().LoadGame();
                }
                SceneManager.LoadScene("UILevelSelect");
            }
            else if (screen == 1) {
                if (position == HoverEnum.FIRST_OPTION)
                {
                    endOfLevel.GetComponent<GameManagementScript>().CreateNewFile();
                    endOfLevel.GetComponent<GameManagementScript>().LoadGame();
                }
                if (position == HoverEnum.SECOND_OPTION)
                {
                    endOfLevel.GetComponent<GameManagementScript>().CreateNewFile();
                    endOfLevel.GetComponent<GameManagementScript>().LoadGame();
                }
                if (position == HoverEnum.THIRD_OPTION)
                {
                    endOfLevel.GetComponent<GameManagementScript>().CreateNewFile();
                    endOfLevel.GetComponent<GameManagementScript>().LoadGame();
                }
                SceneManager.LoadScene("UILevelSelect");
            }
        }
        if (Input.GetButtonDown("Push")) {
            if (screen == 1 || screen == 2) {
                arrowPos = 0;
                transform.position = new Vector3(transform.position.x, firstOption.transform.position.y + 2.1f);
                position = HoverEnum.FIRST_OPTION;
                HandleNewLoadQuiteMenu();
            }
        }
        if (curTime < timer)
        {
            transform.position += new Vector3(direction * .10f, 0);
            curTime++;
        }
        else {
            curTime = 0;
            direction = direction * -1;
        }
    }

    enum HoverEnum
    {
        FIRST_OPTION,
        SECOND_OPTION,
        THIRD_OPTION
    }

    private void FixedUpdate()
    {
        if (change)
        {
            if (count > changeTime) {
                count = 0;
                change = false;
            }
            count++;
            if (arrowPos == 0)
            {
                transform.position = new Vector3(transform.position.x, firstOption.transform.position.y + 2.1f);
                position = HoverEnum.FIRST_OPTION;
            }
            else if (arrowPos == 1)
            {
                transform.position = new Vector3(transform.position.x, secondOption.transform.position.y + 2.1f);
                position = HoverEnum.SECOND_OPTION;
            }
            else if (arrowPos == 2)
            {
                transform.position = new Vector3(transform.position.x, thirdOption.transform.position.y + 2.1f);
                position = HoverEnum.THIRD_OPTION;
            }
        }
        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && arrowPos != 2 && !change) {
            arrowPos = arrowPos + 1;
            change = true;
        }
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && arrowPos != 0 && !change)
        {
            arrowPos = arrowPos - 1;
            change = true;
        }
        if(screen == 2)
        {
            ClearPercent percent;
            if (position == HoverEnum.FIRST_OPTION)
            { 
                percent = endOfLevel.GetComponent<GameManagementScript>().GetClearPercent("1");
            }
            if (position == HoverEnum.SECOND_OPTION)
            {
                percent = endOfLevel.GetComponent<GameManagementScript>().GetClearPercent("2");
            }
            if (position == HoverEnum.THIRD_OPTION)
            {
                percent = endOfLevel.GetComponent<GameManagementScript>().GetClearPercent("3");
            }
            SceneManager.LoadScene("UILevelSelect");
        }
    }

    private void HandleNewGameMenu()
    {
        newLoadQuitPanel.SetActive(false);
        newGameSelect.SetActive(true);
        firstButtonNew.GetComponent<ButtonManager>().SetThisButtonAsFirstUsable();
    }
    private void HandleLoadGameMenu()
    {
        newLoadQuitPanel.SetActive(false);
        loadGameSelect.SetActive(true);
        firstButtonLoad.GetComponent<ButtonManager>().SetThisButtonAsFirstUsable();
    }
    private void HandleNewLoadQuiteMenu() {
        newGameSelect.SetActive(false);
        loadGameSelect.SetActive(false);
        newLoadQuitPanel.SetActive(true);
    }
}
