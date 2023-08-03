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
    // Start is called before the first frame update
    void Start()
    {
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
        if (collected == true && this.isActiveAndEnabled) {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (trulyCollected == false && deathScript.dead == true) {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            player.GetComponent<CheckCollectibles>().unlock = false;
            player2.GetComponent<CheckCollectibles>().unlock = false;
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
