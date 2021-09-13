using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateTrophyUI : MonoBehaviour
{


    private static GameObject endOfLevel;
    public string levelName;
    public Sprite newTrophy;
    private bool updated;
    // Start is called before the first frame update
    void Start()
    {
        updated = false;
        endOfLevel = GameObject.Find("EndOfLevel");
    }

    // Update is called once per frame
    void Update()
    {
        if(levelName != null && endOfLevel.GetComponent<GameManagementScript>().getLevelData(levelName).trophyGot && !updated)
        {
            updateTrophyImage();
            updated = true;
        }
        if(Input.GetKey(KeyCode.K))
        {
            Debug.Log("did we get the trophy? " + levelName + " " + endOfLevel.GetComponent<GameManagementScript>().getLevelData(levelName).trophyGot);
        }
    }

    public void updateTrophyImage()
    {
        Image image = this.GetComponent<Image>();
        if (newTrophy != null)
        {
            image.sprite = newTrophy;
        }
    }
}
