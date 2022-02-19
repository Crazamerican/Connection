using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSelectorScript : MonoBehaviour
{

    public GameObject[] worldImages;
    public GameObject[] worldTexts;
    public GameObject[] worldCanvases;
    public GameObject rootcanvas;
    bool moveStart;
    float xMove;
    float yMove;
    int level;
    private bool moveable;
    public GameObject returnPanel;
    public float timer = 1;
    int direction = -1;
    float curTime = 0;
    int arrowPos = 0;

    private CanvasProper canvasProperScript;
    // Start is called before the first frame update
    void Start()
    {
        moveable = true;
        level = 0;
        arrowPos = 0;
        this.gameObject.transform.position = new Vector3(worldImages[0].gameObject.transform.position.x, worldImages[0].gameObject.transform.position.y);
        canvasProperScript = rootcanvas.GetComponent<CanvasProper>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moveable)
        {
            bool xPosEnd = false;
            bool yPosEnd = false;
            if (Input.GetKeyDown(KeyCode.Return) && !moveStart)
            {
                Debug.Log("attempting to turn on level: " + level);
                for(float i = 0f; i < .25f; i += Time.deltaTime)
                {

                }
                canvasProperScript.ActivatePanel(worldCanvases[level]);
                //worldCanvases[level].SetActive(true);
                //rootcanvas.SetActive(false);
            }
            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && level < worldImages.Length - 1 && !moveStart)
            {
                worldTexts[level].SetActive(false);
                level++;
                xMove = (worldImages[level].transform.position.x - transform.position.x) / 40f;
                yMove = (worldImages[level].transform.position.y - transform.position.y) / 40f;
                moveStart = true;
            }
            else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && level > 0 && !moveStart)
            {
                worldTexts[level].SetActive(false);
                level--;
                xMove = (worldImages[level].transform.position.x - transform.position.x) / 40f;
                yMove = (worldImages[level].transform.position.y - transform.position.y) / 40f;
                moveStart = true;
            }
            if (moveStart == true)
            {
                //125 by 126

                transform.position += new Vector3(xMove, yMove);
                if ((xMove > 0 && transform.position.x >= worldImages[level].transform.position.x) || (xMove < 0 && transform.position.x <= worldImages[level].transform.position.x))
                {
                    transform.position = new Vector3(worldImages[level].transform.position.x, transform.position.y);
                    xPosEnd = true;
                }
                if ((yMove > 0 && transform.position.y >= worldImages[level].transform.position.y) || (yMove < 0 && transform.position.y <= worldImages[level].transform.position.y))
                {
                    transform.position = new Vector3(transform.position.x, worldImages[level].transform.position.y);
                    yPosEnd = true;
                }
                if (xPosEnd && yPosEnd)
                {
                    moveStart = false;
                    worldTexts[level].SetActive(true);
                }
            }
            if (curTime < timer)
            {
                transform.position += new Vector3(0, direction * .01f);
                curTime += Time.deltaTime;
            }
            else
            {
                curTime = 0;
                direction = direction * -1;
            }
        }
    }

    public void SetMoveable(bool canMove)
    {
        moveable = canMove;
    }
}
