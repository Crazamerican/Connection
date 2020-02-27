﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public int curLevel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (curLevel == 1)
            {
                SceneManager.LoadScene("NewGraphics2");
            }
            else if (curLevel == 0)
            {
                SceneManager.LoadScene("NewGraphics3");
            }
            else if (curLevel == 2)
            {
                SceneManager.LoadScene("NewGraphics4");
            }
            else
            {
                SceneManager.LoadScene("End Screen");
            }
        }
    }
}
