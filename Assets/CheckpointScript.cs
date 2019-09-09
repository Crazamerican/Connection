using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    public Sprite yellowCheckpoint;
    public Sprite greenCheckpoint;
    private SpriteRenderer checkpointSpriteRenderer;
    public GameObject player1;
    public GameObject player2;
    public Vector3 respawnPoint;
    public Vector3 respawnPoint2;
    public Vector3 thisRespawnPoint;
    public Vector3 thisRespawnPoint2;
    // Start is called before the first frame update
    void Start()
    {
        checkpointSpriteRenderer = GetComponent<SpriteRenderer>();
        respawnPoint = player1.transform.position;
        respawnPoint2 = player2.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            checkpointSpriteRenderer.sprite = greenCheckpoint;
            respawnPoint = thisRespawnPoint;
            respawnPoint2 = thisRespawnPoint2;
        }
    }
}
