using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollideSpike : MonoBehaviour
{
    public GameObject Checkpoint;
    public GameObject player1;
    public GameObject player2;
    CheckpointScript checkpointScript;
    public GameObject playBoth;
    DeathScript deathScript;
    // Start is called before the first frame update
    void Start()
    {
        deathScript = playBoth.GetComponent<DeathScript>();
        checkpointScript = Checkpoint.GetComponent<CheckpointScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("here!");
            deathScript.dead = true;
        }
    }
}
