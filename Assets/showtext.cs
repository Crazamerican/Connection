using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class showtext : MonoBehaviour
{
    public GameObject text;
    bool atDoor = false;
    public int door;
    public string levelName;
    private GameObject endOfLevel;
    private GameManagementScript manager;

    private void Start()
    {
        if (endOfLevel == null)
        {
            endOfLevel = GameObject.Find("EndOfLevel");
        }
        if(manager == null)
        {
            manager = endOfLevel.GetComponent<GameManagementScript>();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            atDoor = true;
            text.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            atDoor = false;
            text.SetActive(false);
        }
    }
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && atDoor) {
            if (manager.levelUnlocked(levelName))
            {
                SceneManager.LoadScene(levelName);
            }
        }
    }
}
