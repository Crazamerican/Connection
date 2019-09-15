using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingBlock : MonoBehaviour
{
    public Vector3 speed;
    public int time;
    int timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer != time)
        {
            timer++;
            transform.position += speed;
        }
        else {
            timer = 0;
            speed.x = speed.x * -1;
        }
    }
}
