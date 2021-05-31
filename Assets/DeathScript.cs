using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScript : MonoBehaviour
{
    public GameObject Checkpoint;
    public GameObject player1;
    public GameObject player2;
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
                gameManagement.freezePlayer = false;
                timer = 0;
                dead = false;
            } else
            {
                timer++;
            }
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            dead = true;
        }
    }

    public void PlayerDiedMovePlayers()
    {
        dead = true;
        gameManagement.freezePlayer = true;
        Vector3 topPos, botPos;
        if (gameManagement.CheckStartingPoints()) {
            (topPos, botPos) = gameManagement.GetCheckPointPositionToMovePlayersTo();
            Debug.Log(topPos);
            Debug.Log(botPos);
            player1.transform.position = topPos;
            player2.transform.position = botPos;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
