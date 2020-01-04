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

    EventManager em;


    // Start is called before the first frame update
    void Start()
    {
        if (em == null)
        {
            em = FindObjectOfType<EventManager>().GetComponent<EventManager>();
        }

        deathScript = playBoth.GetComponent<DeathScript>();
        checkpointScript = Checkpoint.GetComponent<CheckpointScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            em.PlayerDied();
        }
    }
}
