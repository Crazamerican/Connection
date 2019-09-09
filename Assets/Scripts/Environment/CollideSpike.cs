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
    // Start is called before the first frame update
    void Start()
    {
        checkpointScript = Checkpoint.GetComponent<CheckpointScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player1.transform.position = checkpointScript.respawnPoint;
            player2.transform.position = checkpointScript.respawnPoint2;
        }
    }
}
