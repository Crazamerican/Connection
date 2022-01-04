using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointOther : MonoBehaviour
{
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
            Animator anim = this.GetComponent<Animator>();
            anim.SetBool("isChecked", true);
            checkpointScript.respawnPoint = checkpointScript.player1.transform.position;
            checkpointScript.respawnPoint2 = checkpointScript.player2.transform.position;
        }
    }
}
