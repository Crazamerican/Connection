﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHoverArrow : MonoBehaviour
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
    public GameObject menu;
    public GameObject otherSelector;

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
        if (Input.GetKey(KeyCode.Return))
        {
            if (position == HoverEnum.FIRST_OPTION)
            {
                HandleResume();
            }
            if (position == HoverEnum.SECOND_OPTION)
            {
                HandleRestart();
            }
            if (position == HoverEnum.THIRD_OPTION)
            {
                HandleMainMenu();
            }
        }
        if (curTime < timer)
        {
            otherSelector.transform.position += new Vector3(direction * .25f * -1, 0);
            transform.position += new Vector3(direction * .25f, 0);
            curTime++;
        }
        else
        {
            curTime = 0;
            direction = direction * -1;
        }

        if (change)
        {
            if (count > changeTime)
            {
                count = 0;
                change = false;
            }
            count++;
            if (arrowPos == 0)
            {
                transform.position = new Vector3(transform.position.x, firstOption.transform.position.y);
                otherSelector.transform.position = new Vector3(otherSelector.transform.position.x, transform.position.y);
                position = HoverEnum.FIRST_OPTION;
            }
            else if (arrowPos == 1)
            {
                transform.position = new Vector3(transform.position.x, secondOption.transform.position.y);
                otherSelector.transform.position = new Vector3(otherSelector.transform.position.x, transform.position.y);
                position = HoverEnum.SECOND_OPTION;
            }
            else if (arrowPos == 2)
            {
                transform.position = new Vector3(transform.position.x, thirdOption.transform.position.y);
                otherSelector.transform.position = new Vector3(otherSelector.transform.position.x, transform.position.y);
                position = HoverEnum.THIRD_OPTION;
            }
        }
        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && arrowPos != 2 && !change)
        {
            arrowPos = arrowPos + 1;
            change = true;
        }
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && arrowPos != 0 && !change)
        {
            arrowPos = arrowPos - 1;
            change = true;
        }
    }

    enum HoverEnum
    {
        FIRST_OPTION,
        SECOND_OPTION,
        THIRD_OPTION
    }


    private void HandleResume()
    {
        menu.GetComponent<PauseMenuController>().ResumeGame();
    }
    private void HandleRestart()
    {
        menu.GetComponent<PauseMenuController>().RestartGame();
    }
    private void HandleMainMenu()
    {
        menu.GetComponent<PauseMenuController>().ReturnToMain();
    }
}
