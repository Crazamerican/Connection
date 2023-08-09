using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophyGetAnimTrigger : MonoBehaviour
{
    public bool collected;
    public bool trulyCollected;
    DeathScript deathScript;
    GameObject players;
    GameObject player;
    GameObject player2;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        players = GameObject.Find("Characters_V4");
        player = players.transform.Find("Player").gameObject;
        player2 = players.transform.Find("Player2").gameObject;
        deathScript = players.GetComponent<DeathScript>();
        collected = false;
        trulyCollected = false;
    }

    void FixedUpdate()
    {
        if (collected == true && this.isActiveAndEnabled)
        {
            player.GetComponent<CheckCollectibles>().hasTrophy = true;
            player2.GetComponent<CheckCollectibles>().hasTrophy = true;
            anim.SetBool("TrophyGet", true);
        }
        if (trulyCollected == false && deathScript.dead == true)
        {
            player.GetComponent<CheckCollectibles>().hasTrophy = false;
            player2.GetComponent<CheckCollectibles>().hasTrophy = false;
            anim.SetBool("TrophyGet", false);
            collected = false;
        }
    }

    private void LateUpdate()
    {
        if (player.GetComponent<CheckCollectibles>().hitCheckpoint == true || player2.GetComponent<CheckCollectibles>().hitCheckpoint == true)
        {
            if (collected == true && trulyCollected == false)
            {
                trulyCollected = true;
            }
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Animator anim = this.GetComponent<Animator>();
            anim.SetBool("TrophyGet", true);
        }
    }*/
}
