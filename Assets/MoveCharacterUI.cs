using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    float xMove;
    float yMove;
    int level;
    GameObject nextlevel = null;
    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        moveStart = false;
        xMove = 0f;
        yMove = 0f;
        level1UI.SetActive(true);
        level2UI.SetActive(false);
        level3UI.SetActive(false);
        level4UI.SetActive(false);
        level5UI.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(transform.position.x);
        bool xPosEnd = false;
        bool yPosEnd = false;
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && level != 5 && !moveStart)
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
            xMove = (nextlevel.transform.position.x - transform.position.x)/40f;
            yMove = (nextlevel.transform.position.y - transform.position.y) / 40f;
            moveStart = true;
        }
        else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && level != 1 && !moveStart) {
            level--;
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
            xMove = (nextlevel.transform.position.x - transform.position.x) / 40f;
            yMove = (nextlevel.transform.position.y - transform.position.y) / 40f;
            moveStart = true;
        }
        if (moveStart == true) {
            //125 by 126
            transform.position += new Vector3(xMove, yMove);
            if ((xMove > 0 && transform.position.x >= nextlevel.transform.position.x) || (xMove < 0 && transform.position.x <= nextlevel.transform.position.x)) {
                transform.position = new Vector3(nextlevel.transform.position.x, transform.position.y);
                xPosEnd = true;
            }
            if ((yMove > 0 && transform.position.y >= nextlevel.transform.position.y) || (yMove < 0 && transform.position.y <= nextlevel.transform.position.y))
            {
                transform.position = new Vector3(transform.position.x, nextlevel.transform.position.y);
                yPosEnd = true;
            }
            if (xPosEnd && yPosEnd) {
                moveStart = false;
                if (level == 1)
                {
                    level1UI.SetActive(true);
                }
                else if (level == 2) {
                    level2UI.SetActive(true);
                }
                else if (level == 3)
                {
                    level3UI.SetActive(true);
                }
                else if (level == 4)
                {
                    level4UI.SetActive(true);
                }
                else if (level == 5)
                {
                    level5UI.SetActive(true);
                }
            }
        }
    }
}
