using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    public GameObject Checkpoint;
    public GameObject player1;
    public GameObject player2;
    public GameObject camBoi;
    FollowPlayer cameraScript;
    GameManagementScript gameManagement;
    //public EventManager em;

    public bool dead;
    int timer;
    public bool camDone;


    public void OnEnable()
    {
        EventManager.OnDeath += PlayerDiedMovePlayers;

    }

    public void OnDisable()
    {
        EventManager.OnDeath -= PlayerDiedMovePlayers;

    }

    // Start is called before the first frame update
    void Start()
    {
        gameManagement = GameObject.Find("EndOfLevel").GetComponent<GameManagementScript>();

        timer = 0;
        camDone = false;
        dead = false;
        cameraScript = camBoi.GetComponent<FollowPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dead == true)
        {
            PlayerDiedMovePlayers();
        }
        if (camDone == true)
        {
            if (timer == 20)
            {
                camDone = false;
                cameraScript.freezePlayers = false;
                timer = 0;
            } else
            {
                timer++;
            }
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            PlayerDiedMovePlayers();
        }
    }

    public void PlayerDiedMovePlayers()
    {
        dead = true;
        cameraScript.freezePlayers = true;
        Vector3 topPos, botPos;
        (topPos, botPos) = gameManagement.GetCheckPointPositionToMovePlayersTo();
        Debug.Log("Setting the Top player to : " + topPos + " \n Setting the bottom player to: " + botPos);
        player1.transform.position = topPos;
        player2.transform.position = botPos;
    }
}
