using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveCharacterUI : MonoBehaviour
{
    bool moveStart;
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public GameObject level4;
    public GameObject level5;
    public GameObject level1UI;
    public GameObject level2UI;
    public GameObject level3UI;
    public GameObject level4UI;
    public GameObject level5UI;
    public GameObject worldShiftPoint;
    public GameObject worldPreviousPoint;
    float xMove;
    float yMove;
    int level;
    GameObject nextlevel = null;
    private States state;
    private bool moveable;
    public GameObject returnPanel;
    bool allowEnterLevel = false;
    private GameObject endOfLevel;
    public float fractionSpeed = 20f;
    public int worldNumber;

    float externalTimer;
    // Start is called before the first frame update
    void Start()
    {
        endOfLevel = GameObject.Find("EndOfLevel");
        if (endOfLevel.GetComponent<GameManagementScript>().previousState != null)
        {
            state = endOfLevel.GetComponent<GameManagementScript>().previousState;
        } else
        {
            state = States.LEVELONE;
        }
        SetupLocation(state);
    }

    private void SetupLocation(States state)
    {

        level1UI.SetActive(false);
        level2UI.SetActive(false);
        level3UI.SetActive(false);
        level4UI.SetActive(false);
        level5UI.SetActive(false);
        switch (state)
        {
            case States.LEVELONE:
                level = 1;
                level1UI.SetActive(true);
                transform.position = new Vector3(level1.transform.position.x, level1.transform.position.y, transform.position.z);
                break;
            case States.LEVELTWO:
                level = 2;
                level2UI.SetActive(true);
                transform.position = new Vector3(level2.transform.position.x, level2.transform.position.y, transform.position.z);
                break;
            case States.LEVELTHREE:
                level = 3;
                level3UI.SetActive(true);
                transform.position = new Vector3(level3.transform.position.x, level3.transform.position.y, transform.position.z);
                break;
            case States.LEVELFOUR:
                level = 4;
                level4UI.SetActive(true);
                transform.position = new Vector3(level4.transform.position.x, level4.transform.position.y, transform.position.z);
                break;
            case States.LEVELFIVE:
                level = 5;
                level5UI.SetActive(true);
                transform.position = new Vector3(level5.transform.position.x, level5.transform.position.y, transform.position.z);
                break;
            case States.CHANGEWORLD:
                if (worldNumber == 1) {
                    level = 6;
                    transform.position = new Vector3(worldShiftPoint.transform.position.x, worldShiftPoint.transform.position.y, transform.position.z);
                }
                break;
            case States.PREVIOUSWORLD:
                if (worldNumber == 2)
                {
                    level = -1;
                    transform.position = new Vector3(worldPreviousPoint.transform.position.x, worldPreviousPoint.transform.position.y, transform.position.z);
                }
                break;
        }
        moveStart = false;
        xMove = 0f;
        yMove = 0f;
        Debug.Log("The previous state was: " + state);
        moveable = false;
        returnPanel.SetActive(false);
    }

    public enum States
    {
        LEVELONE,
        LEVELTWO,
        LEVELTHREE,
        LEVELFOUR,
        LEVELFIVE,
        CHANGEWORLD,
        PREVIOUSWORLD,
    }

    public void SetMoveable(bool move)
    {
        moveable = move;
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Jump")) && !moveStart)
        {
            int level = (int)state + (5 * (worldNumber - 1) + 1);
            GameManagementScript.Instance.LoadLevel("Level" + level);
            endOfLevel.GetComponent<GameManagementScript>().previousState = state;
            //SceneManager.LoadScene("Level" + level);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > externalTimer)
        {
            moveable = true;
        }
        if (moveable)
        {
            bool xPosEnd = false;
            bool yPosEnd = false;
            int maxLevel = 6;
            if (worldNumber == 2) {
                maxLevel = 5;
            }
            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && level != maxLevel && !moveStart)
            {
                level++;
                if (level == 2)
                {
                    nextlevel = level2;
                    level1UI.SetActive(false);
                }
                else if (level == 3)
                {
                    nextlevel = level3;
                    level2UI.SetActive(false);
                }
                else if (level == 4)
                {
                    nextlevel = level4;
                    level3UI.SetActive(false);
                }
                else if (level == 5)
                {
                    nextlevel = level5;
                    level4UI.SetActive(false);
                }
                else if (level == 6)
                {
                    nextlevel = worldShiftPoint;
                    level5UI.SetActive(false);
                }
                if (level == 6) {
                    xMove = (nextlevel.transform.position.x - transform.position.x) / (2 * fractionSpeed);
                    yMove = (nextlevel.transform.position.y - transform.position.y) / (2 * fractionSpeed);
                } else {
                    xMove = (nextlevel.transform.position.x - transform.position.x) / fractionSpeed;
                    yMove = (nextlevel.transform.position.y - transform.position.y) / fractionSpeed;
                }
                moveStart = true;
            }
            else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && level > 0 && !moveStart)
            {
                bool moveOkay = false;
                if (level == 1 && worldNumber != 1)
                {
                    level = -1;
                    moveOkay = true;
                }
                else if (level != 1)
                {
                    level--;
                    moveOkay = true;
                }
                if (moveOkay) {
                    if (level == 1)
                    {
                        nextlevel = level1;
                        level2UI.SetActive(false);
                    }
                    else if (level == 2)
                    {
                        nextlevel = level2;
                        level3UI.SetActive(false);
                    }
                    else if (level == 3)
                    {
                        nextlevel = level3;
                        level4UI.SetActive(false);
                    }
                    else if (level == 4)
                    {
                        nextlevel = level4;
                        level5UI.SetActive(false);
                    }
                    else if (level == -1)
                    {
                        nextlevel = worldPreviousPoint;
                        level1UI.SetActive(false);
                    }
                    if (level == -1)
                    {
                        xMove = (nextlevel.transform.position.x - transform.position.x) / (2 * fractionSpeed);
                        yMove = (nextlevel.transform.position.y - transform.position.y) / (2 * fractionSpeed);
                    }
                    else
                    {
                        xMove = (nextlevel.transform.position.x - transform.position.x) / fractionSpeed;
                        yMove = (nextlevel.transform.position.y - transform.position.y) / fractionSpeed;
                    }
                    moveStart = true;
                }
            }
            if (moveStart == true)
            {
                //125 by 126
                transform.position += new Vector3(xMove, yMove);
                if ((xMove > 0 && transform.position.x >= nextlevel.transform.position.x) || (xMove < 0 && transform.position.x <= nextlevel.transform.position.x))
                {
                    transform.position = new Vector3(nextlevel.transform.position.x, transform.position.y);
                    xPosEnd = true;
                }
                if ((yMove > 0 && transform.position.y >= nextlevel.transform.position.y) || (yMove < 0 && transform.position.y <= nextlevel.transform.position.y))
                {
                    transform.position = new Vector3(transform.position.x, nextlevel.transform.position.y);
                    yPosEnd = true;
                }
                if (xPosEnd && yPosEnd)
                {
                    moveStart = false;
                    if (level == 1)
                    {
                        level1UI.SetActive(true);
                        state = States.LEVELONE;
                    }
                    else if (level == 2)
                    {
                        level2UI.SetActive(true);
                        state = States.LEVELTWO;
                    }
                    else if (level == 3)
                    {
                        level3UI.SetActive(true);
                        state = States.LEVELTHREE;
                    }
                    else if (level == 4)
                    {
                        level4UI.SetActive(true);
                        state = States.LEVELFOUR;
                    }
                    else if (level == 5)
                    {
                        level5UI.SetActive(true);
                        state = States.LEVELFIVE;
                    }
                }
            }
        }
        if ((worldShiftPoint != null) && this.transform.position.x >= worldShiftPoint.transform.position.x) {
            GameManagementScript.Instance.LoadLevel("UILevelSelect" + (worldNumber + 1));
        }
        if ((worldPreviousPoint != null) && this.transform.position.x <= worldPreviousPoint.transform.position.x)
        {
            GameManagementScript.Instance.LoadLevel("UILevelSelect" + (worldNumber - 1));
        }
    }

    public void SetWaitTime(float timeToWait)
    {
        Debug.Log("Setting the wait time : " + Time.time + timeToWait);
        externalTimer = Time.time + timeToWait;
        moveable = false;
    }
}
