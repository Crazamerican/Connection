using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public int curLevel;
    private static GameObject endOfLevel;
    int world;
    // Start is called before the first frame update
    void Start()
    {
        endOfLevel = GameObject.Find("EndOfLevel");
        endOfLevel.GetComponent<GameManagementScript>().unlock = false;
        world = (curLevel - 1) / 5 + 1;
        Debug.Log("world: " + world);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (collision.GetComponent<CheckCollectibles>().hasTrophy == true) {
                endOfLevel.GetComponent<GameManagementScript>().UpdateLevelDataTrophy();
            }
            GameManagementScript manager = endOfLevel.GetComponent<GameManagementScript>();
            manager.UpdateLevelDataCleared();
            manager.UnlockNextLevel();
            manager.SaveLevel();
            manager.unlock = true;

            manager.LoadLevel("UILevelSelect" + world);
            //if (curLevel == 1)
            //{
            //    SceneManager.LoadScene("NewGraphics2");
            //}
            //else if (curLevel == 2)
            //{
            //    SceneManager.LoadScene("NewGraphics3");
            //}
            //else if (curLevel == 3)
            //{
            //    SceneManager.LoadScene("NewGraphics4");
            //}
            //else if (curLevel == 4)
            //{
            //    SceneManager.LoadScene("NewGraphics5");
            //}
            //else if (curLevel == 5)
            //{
            //    SceneManager.LoadScene("NewGraphics6");
            //}
            //else if (curLevel == 6)
            //{
            //    SceneManager.LoadScene("NewGraphics7");
            //}
            //else
            //{
            //    SceneManager.LoadScene("End Screen");
            //}
        }
    }
}
