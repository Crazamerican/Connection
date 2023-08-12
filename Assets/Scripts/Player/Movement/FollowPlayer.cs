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
    public Vector3 initCam;
    Vector3 camRight;

    public GameObject firstStop;
    public GameObject secondStop;
    cameraEnd firstStopScript;
    cameraEnd secondStopScript;

    bool curCamEnd;
    bool switchStop;
    public bool switchToSecond;

    private GameManagementScript gameManagement;
    private bool freezePlayers;

    public GameObject playBoth;
    DeathScript deathScript;
    bool startDead;
    bool onInit;

    float firstEnd;
    public float secondInit;

    bool screenTransitioning;

    private Vector3 offset;         //Private variable to store the offset distance between the player and camera

    bool frozenPlayer;

    CheckpointScript checkpoint;

    float oldPlayerPos;
    float oldPlayer2Pos;

    // Use this for initialization
    void Start()
    {
        checkpoint = GameObject.Find("Checkpoint").GetComponent<CheckpointScript>();
        frozenPlayer = false;
        gameManagement = GameObject.Find("EndOfLevel").GetComponent<GameManagementScript>();
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
        oldPlayerPos = player.transform.position.x;
        oldPlayer2Pos = player2.transform.position.x;
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
        if (deathScript.dead == true && checkpoint.respawnPoint.x == player.transform.position.x && frozenPlayer == false) {
            frozenPlayer = true;
            freezePlayers = gameManagement.freezePlayer;
        }
        //Debug.Log("Player1: " + player.transform.position.x);
        //Debug.Log("Player2: " + player2.transform.position.x);
        //Debug.Log("deathScript = " + deathScript.dead + " freezePlayers = " + freezePlayers);
        //Debug.Log("onInit = " + onInit);
        //when transitioning between parts of levels
        if (freezePlayers == true && deathScript.dead == false && deathScript.camDone == false && screenTransitioning == false)
        {
            gameManagement.freezePlayer = false;
            freezePlayers = false;
            //transform.position = initCam;
            //switchToSecond = true;
        } //when player is dead and camera transitioning back to old checkpoint
        else if (freezePlayers == true && deathScript.dead == true)
        {
            //Debug.Log("AHHHHHHHHHHHADFADFADF");
            if (startDead == true)
            {
                Debug.Log("1");
                startDead = false;
                if (player.transform.position.x <= initCam.x)
                {
                    onInit = true;
                }
                else
                {
                    onInit = false;
                }
                Debug.Log("onInit" + onInit);
            }
            if (onInit == true)
            {
                Debug.Log("2");
                if (transform.position.x <= initCam.x)
                {
                    Debug.Log("True done");
                    deathScript.camDone = true;
                    transform.position = initCam;
                    deathScript.dead = false;
                    startDead = true;
                    frozenPlayer = false;
                }
                else
                {
                    transform.position = transform.position - new Vector3(0.18f, 0);
                }
            }
            else
            {
                Debug.Log("3");
                if (playerCam.x <= (width * .48) && player2Cam.x <= (width * .48))
                {
                    transform.position = transform.position - new Vector3(0.18f, 0);
                }
                else if (playerCam.x >= (width * .52) && player2Cam.x >= (width * .52))
                {
                    transform.position = transform.position + new Vector3(0.18f, 0);
                }
                else
                {
                    deathScript.camDone = true;
                    deathScript.dead = false;
                    startDead = true;
                    frozenPlayer = false;
                }
            }
        }
        //move camera right
        else if (moveHorizontal > 0 || (player.transform.position.x > oldPlayerPos) || (player2.transform.position.x > oldPlayer2Pos))
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
        else if ((moveHorizontal < 0 || (player.transform.position.x > oldPlayerPos) || (player2.transform.position.x > oldPlayer2Pos)) && camLeft.x > cam.ScreenToWorldPoint(initCam).x)
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
        else if (curCamEnd == false)
        {
            transform.position = initCam;
        }
        //used to indicate screen transition
        //if (playerCam.x >= (width) && player2Cam.x >= (width))
        //Debug.Log("player transform: " + player.transform.position.x + " firstend: " + firstEnd);
        //Debug.Log("initCam: " + initCam + " initCameStart: " + (initCam.x - cam.pixelWidth * .05f));
        //Debug.Log("Player1Pos: " + player.transform.position.x + " Player2Pos: " + player2.transform.position.x);
        if (player.transform.position.x >= firstEnd - .05f && player2.transform.position.x >= firstEnd - .05f && switchStop == false)
        {
            Debug.Log("HITTTTTTTT");
            if (!screenTransitioning)
            {
                StartCoroutine(MidLevelTransition());
            }

            //Debug.Log("transition time boisss");
            //gameManagement.freezePlayer = true;
            //freezePlayers = true;

            //playerCam = cam.WorldToScreenPoint(player.transform.position);
            //player2Cam = cam.WorldToScreenPoint(player2.transform.position);
            //playerAvg = (playerCam.x + player2Cam.x) / 2;
            //initCam = (cam.ScreenToWorldPoint(new Vector3(playerAvg + width / 2, height / 2)));
            //initCam -= new Vector3(p1width * 1 / 2, 0);
            ////initCam = new Vector3(secondInit, initCam.y);
            ////used to setup next screen transition
            //switchStop = true;
            //player.transform.position = new Vector3(horScript.secondStart + .05f, player.transform.position.y, -1);
            //player2.transform.position = new Vector3(horScript.secondStart + .05f, player2.transform.position.y, -1);
        }
        oldPlayerPos = player.transform.position.x;
        oldPlayer2Pos = player2.transform.position.x;
    }

    IEnumerator MidLevelTransition()
    {
        screenTransitioning = true;
        gameManagement.freezePlayer = true;
        freezePlayers = true;

        player.GetComponentInChildren<Animator>().Play("ScreenTransitionOff");
        player2.GetComponentInChildren<Animator>().Play("ScreenTransitionOff");

        player.GetComponent<HiddenGroundParticles>().enabled = false;

        yield return new WaitForSecondsRealtime(.45f);
        gameManagement.ScreenTransitionToBlack();
        yield return new WaitForSecondsRealtime(1f);

        playerCam = cam.WorldToScreenPoint(player.transform.position);
        player2Cam = cam.WorldToScreenPoint(player2.transform.position);
        playerAvg = (playerCam.x + player2Cam.x) / 2;
        initCam = (cam.ScreenToWorldPoint(new Vector3(playerAvg + width / 2, height / 2)));
        initCam -= new Vector3(p1width * 1 / 2, 0);
        //initCam = new Vector3(secondInit, initCam.y);
        //used to setup next screen transition
        
        player.transform.position = new Vector3(horScript.secondStart + .05f, player.transform.position.y, -1);
        player2.transform.position = new Vector3(horScript.secondStart + .05f, player2.transform.position.y, -1);

        yield return new WaitForSecondsRealtime(.5f);

        switchStop = true;

        transform.position = initCam;
        switchToSecond = true;

        gameManagement.ScreenTransitionUp();

        player.GetComponentInChildren<Animator>().Play("ScreenTransitionOn");
        player2.GetComponentInChildren<Animator>().Play("ScreenTransitionOn");

        yield return new WaitForSecondsRealtime(1.5f); //wait for screentransitionup to finish
        screenTransitioning = false;
        player.GetComponent<HiddenGroundParticles>().enabled = true;
    }
}
