using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    public GameObject Checkpoint;
    public GameObject player1;
    public GameObject player2;
    public GameObject camBoi;
    CheckpointScript checkpointScript;
    FollowPlayer cameraScript;

    public bool dead;
    int timer;
    public bool camDone;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        camDone = false;
        dead = false;
        checkpointScript = Checkpoint.GetComponent<CheckpointScript>();
        cameraScript = camBoi.GetComponent<FollowPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dead == true)
        {
            cameraScript.freezePlayers = true;
            player1.transform.position = checkpointScript.respawnPoint;
            player2.transform.position = checkpointScript.respawnPoint2;
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
    }
}
