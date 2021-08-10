using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollideSpike : MonoBehaviour
{
    DeathScript deathScript;

    //EventManager em;

    public GameObject playerThings;

    // Start is called before the first frame update
    void Start()
    {
        /*
        if (em == null)
        {
            em = FindObjectOfType<EventManager>().GetComponent<EventManager>();
        }
        */

        //deathScript = playBoth.GetComponent<DeathScript>();
        //checkpointScript = Checkpoint.GetComponent<CheckpointScript>();
        deathScript = FindObjectOfType<DeathScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("here!");
            deathScript.dead = true;
        }
        /*
        if (other.tag == "Player")
        {
            em.PlayerDied();
        }
        */
    }
}
