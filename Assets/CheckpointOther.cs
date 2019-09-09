using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointOther : MonoBehaviour
{
    public Sprite yellowCheckpoint;
    public Sprite greenCheckpoint;
    private SpriteRenderer checkpointSpriteRenderer;
    public GameObject checkpointMaster;
    public Vector3 thisRespawnPoint;
    public Vector3 thisRespawnPoint2;
    CheckpointScript checkpointScript;
    // Start is called before the first frame update
    void Start()
    {
        checkpointSpriteRenderer = GetComponent<SpriteRenderer>();
        checkpointScript = checkpointMaster.GetComponent<CheckpointScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            checkpointSpriteRenderer.sprite = greenCheckpoint;
            checkpointScript.respawnPoint = thisRespawnPoint;
            checkpointScript.respawnPoint2 = thisRespawnPoint2;
        }
    }
}
