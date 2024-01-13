using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHoverArrow2 : MonoBehaviour
{

    public int timer = 4;
    int direction = -1;
    float curTime = 0f;
    int arrowPos = 0;
    public GameObject firstOption;
    public GameObject secondOption;
    bool change = false;
    public float changeTime;
    float count;
    private HoverEnum position = HoverEnum.FIRST_OPTION;
    public GameObject menu;
    public GameObject otherSelector;
    // Start is called before the first frame update
    void Start()
    {
        count = 0.0f;
        change = false;
        arrowPos = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return) || Input.GetButtonDown("Jump"))
        {
            if (position == HoverEnum.FIRST_OPTION)
            {
                HandleResume();
            }
            if (position == HoverEnum.SECOND_OPTION)
            {
                HandleMainMenu();
            }
        }
        if (curTime < timer)
        {
            otherSelector.transform.position += new Vector3(direction * .25f * -1 * Time.deltaTime, 0);
            transform.position += new Vector3(direction * .25f * Time.deltaTime, 0);
            curTime += Time.deltaTime;
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
            count = count + Time.fixedUnscaledDeltaTime;
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
        }
        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && arrowPos != 3 && !change)
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
        SECOND_OPTION
    }


    private void HandleResume()
    {
        menu.GetComponent<PauseMenuController>().ResumeGame();
    }
    private void HandleMainMenu()
    {
        menu.GetComponent<PauseMenuController>().ReturnToMain();
    }
}
