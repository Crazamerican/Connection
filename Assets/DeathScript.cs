using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScript : MonoBehaviour
{
    public GameObject Checkpoint;
    CheckpointScript checkpointScript;
    public GameObject player1;
    public GameObject player2;
    public Animator charAnim1;
    public Animator charAnim2;
    GameManagementScript gameManagement;
    //public EventManager em;

    public bool dead;
    bool respawning;
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
        checkpointScript = Checkpoint.GetComponent<CheckpointScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dead == true && !respawning)
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
        StartCoroutine(DeathCycle());
        //Vector3 topPos, botPos;
        //if (gameManagement.CheckStartingPoints()) {
        //    (topPos, botPos) = gameManagement.GetCheckPointPositionToMovePlayersTo();
        //    Debug.Log(topPos);
        //    Debug.Log(botPos);
        //    player1.transform.position = topPos;
        //    player2.transform.position = botPos;
        //}
        //else
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //}
        //gameManagement.freezePlayer = false;
    }

    //Waits for death animation and respawn
    private IEnumerator DeathCycle()
    {
        //death anim
        respawning = true;
        //Debug.Log("dying");
        charAnim1.SetTrigger("death");
        charAnim2.SetTrigger("death");
        yield return new WaitForSecondsRealtime(.75f);

        Vector3 topPos, botPos;
        /*
        if (gameManagement.CheckStartingPoints())
        {
            (topPos, botPos) = gameManagement.GetCheckPointPositionToMovePlayersTo();
            Debug.Log(topPos);
            Debug.Log(botPos);
            player1.transform.position = new Vector3(0.4670523f, 7.777f);
            player2.transform.position = new Vector3(0.4670523f, -0.312f);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        */
        topPos = checkpointScript.respawnPoint;
        botPos = checkpointScript.respawnPoint2;
        player1.transform.position = topPos;
        player2.transform.position = botPos;

        //respawn anim
        yield return new WaitForSecondsRealtime(.75f);
        respawning = false;
        dead = false;
        gameManagement.freezePlayer = false;
        
    }
}
