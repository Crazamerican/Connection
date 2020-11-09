using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
    //The two player characters
    public GameObject player;       
    public GameObject player2;
    HorizontalMovement horScript;
    float p1width;

    Camera cam;

    Vector3 playerCam;
    Vector3 player2Cam;

    float height;
    float width;
    float worldMiddle;
    float moveHorizontal;
    float playerAvg;
    Vector3 camDif;
    Vector3 camLeft;
    Vector3 initCam;
    Vector3 camRight;

    public GameObject firstStop;
    public GameObject secondStop;
    cameraEnd firstStopScript;
    cameraEnd secondStopScript;

    bool curCamEnd;
    bool switchStop;
    public bool switchToSecond;

    public bool freezePlayers;

    public GameObject playBoth;
    DeathScript deathScript;
    bool startDead;
    bool onInit;

    float firstEnd;
    public float secondInit;


    private Vector3 offset;         //Private variable to store the offset distance between the player and camera

    // Use this for initialization
    void Start()
    {
        horScript = player.GetComponent<HorizontalMovement>();
        firstEnd = horScript.firstEnd;
        onInit = true;
        startDead = true;
        deathScript = playBoth.GetComponent<DeathScript>();
        firstStopScript = firstStop.GetComponent<cameraEnd>();
        secondStopScript = secondStop.GetComponent<cameraEnd>();
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
        cam = GetComponent<Camera>();
        //the screen position of the two player characters
        playerCam = cam.WorldToScreenPoint(player.transform.position);
        player2Cam = cam.WorldToScreenPoint(player2.transform.position);
        playerAvg = (playerCam.x + player2Cam.x) / 2;
        //the screen height
        height = cam.pixelHeight;
        width = cam.pixelWidth;
        p1width = player.GetComponent<BoxCollider2D>().bounds.size.x;
        initCam = (cam.ScreenToWorldPoint(new Vector3(playerAvg + width / 2, height / 2)));
        initCam -= new Vector3(p1width * 3 / 4, 0);
        transform.position = initCam;
        camLeft = new Vector3(0, 0, 0);
        camRight = new Vector3(0, 0, 0);
        camLeft = cam.ScreenToWorldPoint(transform.position);
        Vector3 widThing = new Vector3(width, 0, 0);
        widThing = cam.ScreenToWorldPoint(widThing);
        camRight = transform.position + widThing;
        curCamEnd = firstStopScript.cameraHere;
        switchStop = false;
        freezePlayers = false;
        switchToSecond = false;
    }
    private void Update()
    {
        firstEnd = horScript.firstEnd;
        //the player's horizontal input (whether they are pressing the arrows right or left)
        //0 if not touch, negative if left, positive if right
        moveHorizontal = Input.GetAxis("Horizontal");
        //screen pos of two player characters
        playerCam = cam.WorldToScreenPoint(player.transform.position);
        player2Cam = cam.WorldToScreenPoint(player2.transform.position);
        //avg screen x of two players
        playerAvg = (playerCam.x + player2Cam.x) / 2;
        camDif = new Vector3(0, 0, 0);
        camLeft = new Vector3(0, 0, 0);
        camRight = new Vector3(0, 0, 0);
        //gets the very lefthand side of the screen in the world position
        camLeft = cam.ScreenToWorldPoint(transform.position);
        Vector3 widThing = new Vector3(width, 0, 0);
        widThing = cam.ScreenToWorldPoint(widThing);
        camRight = transform.position + widThing;
        //help setup next screen transition
        if (switchStop == false)
        {
            curCamEnd = firstStopScript.cameraHere;
        }
        else {
            curCamEnd = secondStopScript.cameraHere;
        }
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        //when transitioning between parts of levels
        if (freezePlayers == true && deathScript.dead == false && deathScript.camDone == false)
        {
            if (cam.transform.position.x > initCam.x)
            {
                freezePlayers = false;
                transform.position = initCam;
                switchToSecond = true;
            } else
            {
                transform.position = transform.position + new Vector3(0.18f, 0);
            }
        } //when player is dead and camera transitioning back to old checkpoint
        else if (freezePlayers == true && deathScript.dead == true) {
            if (startDead == true)
            {
                startDead = false;
                if (player.transform.position.x < initCam.x) {
                    onInit = true;
                } else
                {
                    onInit = false;
                }
                Debug.Log("onInit" + onInit);
            }
            if (onInit == true) {
                if (transform.position.x <= initCam.x)
                {
                    deathScript.camDone = true;
                    transform.position = initCam;
                    deathScript.dead = false;
                    startDead = true;
                }
                else
                {
                    transform.position = transform.position - new Vector3(0.18f, 0);
                }
            } else
            {
                if (playerCam.x <= (width * .48) && player2Cam.x <= (width * .48))
                {
                    transform.position = transform.position - new Vector3(0.18f, 0);
                } else if (playerCam.x >= (width * .52) && player2Cam.x >= (width * .52))
                {
                    transform.position = transform.position + new Vector3(0.18f, 0);
                }
                else
                {
                    deathScript.camDone = true;
                    deathScript.dead = false;
                    startDead = true;
                }
            }
        }
        //move camera right
        else if (moveHorizontal > 0)
        {
            if (playerAvg - cam.pixelWidth * .05f > cam.pixelWidth * .5f && curCamEnd == false)
            {
                //make camDif the playerAvg if playerAvg is bigger than middle of screen
                camDif = cam.ScreenToWorldPoint(new Vector3(playerAvg - cam.pixelWidth * .05f, height / 2));
                //move cam to new pos
                transform.position = camDif;
            }
        }
        //if moving left and left side of screen is greater than -18 (which is the very left side of the level) 
        else if (moveHorizontal < 0 && camLeft.x > cam.ScreenToWorldPoint(initCam).x)
        {
            if (playerAvg + cam.pixelWidth * .05f < cam.pixelWidth / 2)
            {
                camDif = cam.ScreenToWorldPoint(new Vector3(playerAvg + cam.pixelWidth * .05f, height / 2));
                transform.position = camDif;
            }
        }
        else if (camLeft.x > cam.ScreenToWorldPoint(initCam).x && curCamEnd == false)
        {
            //camDif = cam.ScreenToWorldPoint(new Vector3(playerAvg, height / 2));
            //transform.position = camDif;
        } //move camera to initial position
        else if (curCamEnd == false) {
            transform.position = initCam;
        }
        //used to indicate screen transition
        //if (playerCam.x >= (width) && player2Cam.x >= (width))
        if (player.transform.position.x >= firstEnd - .05f && player2.transform.position.x >= firstEnd - .05f && switchStop == false)
        {
            freezePlayers = true;
            playerCam = cam.WorldToScreenPoint(player.transform.position);
            player2Cam = cam.WorldToScreenPoint(player2.transform.position);
            playerAvg = (playerCam.x + player2Cam.x) / 2;
            initCam = (cam.ScreenToWorldPoint(new Vector3(playerAvg + width / 2, height / 2)));
            initCam -= new Vector3(p1width * 1 / 2, 0);
            //initCam = new Vector3(secondInit, initCam.y);
            //used to setup next screen transition
            switchStop = true;
            player.transform.position = new Vector3(horScript.secondStart + .05f, player.transform.position.y);
            player2.transform.position = new Vector3(horScript.secondStart + .05f, player2.transform.position.y);
        }
    }
}
