using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacterUI : MonoBehaviour
{
    bool moveStart;
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(transform.position.x);
        bool xPosEnd = false;
        bool yPosEnd = false;
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && level != 3 && !moveStart)
        {
            level++;
            if (level == 2)
            {
                nextlevel = level2;
            }
            else if (level == 3) {
                nextlevel = level3;
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
            }
            else if (level == 2)
            {
                nextlevel = level2;
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
            }
        }
    }
}
