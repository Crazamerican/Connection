using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverArrow : MonoBehaviour
{
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
        count = 0;
        change = false;
        arrowPos = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Return))
        {
            if(position == HoverEnum.FIRST_OPTION)
            {
                HandleNewGameMenu();
            }
            if(position == HoverEnum.SECOND_OPTION)
            {
                HandleLoadGameMenu();
            }
            if(position == HoverEnum.THIRD_OPTION)
            {
                Application.Quit();
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
                transform.position = new Vector3(transform.position.x, firstOption.transform.position.y);
                position = HoverEnum.FIRST_OPTION;
            }
            else if (arrowPos == 1)
            {
                transform.position = new Vector3(transform.position.x, secondOption.transform.position.y);
                position = HoverEnum.SECOND_OPTION;
            }
            else if (arrowPos == 2)
            {
                transform.position = new Vector3(transform.position.x, thirdOption.transform.position.y);
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
}
