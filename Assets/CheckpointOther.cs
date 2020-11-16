using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointOther : MonoBehaviour
{
    public Sprite yellowCheckpoint;
    public Sprite greenCheckpoint;
    private SpriteRenderer checkpointSpriteRenderer;
    public GameObject checkpointMaster;
    CheckpointScript checkpointScript;
    public int curCheckPoint;
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
        }
    }
}
