using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyRespawn : MonoBehaviour
{
    public bool collected;
    public bool trulyCollected;
    DeathScript deathScript;
    GameObject players;
    GameObject player;
    GameObject player2;
    public bool player1World;
    // Start is called before the first frame update
    void Start()
    {
        if (this.transform.position.y >= 2.3f)
        {
            player1World = true;
        }
        else {
            player1World = false;
        }
        players = GameObject.Find("Characters_V4");
        player = players.transform.Find("Player").gameObject;
        player2 = players.transform.Find("Player2").gameObject;
        deathScript = players.GetComponent<DeathScript>();
        collected = false;
        trulyCollected = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool doorOpen = player.GetComponent<CheckCollectibles>().doorOpen;
        if (collected == true && this.isActiveAndEnabled) {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (trulyCollected == false && deathScript.dead == true && doorOpen == false) {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            if (player1World == true)
            {
                player.GetComponent<CheckCollectibles>().unlock = false;
            }
            else if (player1World == false) {
                player2.GetComponent<CheckCollectibles>().unlock = false;
            }
            collected = false;
        }
    }
    private void LateUpdate()
    {
        if (player.GetComponent<CheckCollectibles>().hitCheckpoint == true || player2.GetComponent<CheckCollectibles>().hitCheckpoint == true)
        {
            //Debug.Log("hitCheckpoint");
            //Debug.Log("collected: " + collected + " trulyCollectd: " + trulyCollected);
            if (collected == true && trulyCollected == false)
            {
                Debug.Log("trueCollect");
                trulyCollected = true;
            }
        }
    }
}
