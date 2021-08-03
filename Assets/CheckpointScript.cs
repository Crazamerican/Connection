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
    public Vector3 thisRespawnPoint;
    public Vector3 thisRespawnPoint2;
    public Vector3 respawnPoint;
    public Vector3 respawnPoint2;
    public int curCheckPoint;
    private GameObject endOfLevel;
    GameManagementScript gameManager;
    private Transform bottomCheckPoint;
    // Start is called before the first frame update
    void Start()
    {
        respawnPoint = player1.transform.position;
        respawnPoint2 = player2.transform.position;
        bottomCheckPoint = gameObject.transform.GetChild(0);
        checkpointSpriteRenderer = GetComponent<SpriteRenderer>();
        endOfLevel = GameObject.Find("EndOfLevel");
        gameManager = endOfLevel.GetComponent<GameManagementScript>();
        gameManager.checkPoints.Add(this.gameObject);
        curCheckPoint = gameManager.checkPoints.IndexOf(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            checkpointSpriteRenderer.sprite = greenCheckpoint;
            respawnPoint = player1.transform.position;
            respawnPoint2 = player2.transform.position;
            //gameManager.UpdateLocations(player1.transform.position, player2.transform.position);
            Debug.Log("Setting the world position to: " + player1.transform.position + " And second position to: " + player2.transform.position);
        }
    }
}
