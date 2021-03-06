﻿using System.Collections;
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
    public int curCheckPoint;
    private GameObject endOfLevel;
    GameManagementScript gameManager;
    private Transform bottomCheckPoint;
    // Start is called before the first frame update
    void Start()
    {
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
            LevelData data = gameManager.GetLevelData();
            data.topCharacterStartPosition = player1.transform.localPosition;
            data.bottomCharacterStartPosition = player2.transform.localPosition;
            Debug.Log("Setting the world position to: " + player1.transform.localPosition + " And second position to: " + player2.transform.localPosition);
        }
    }
}
