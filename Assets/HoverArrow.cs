using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverArrow : MonoBehaviour
{
    public int timer = 4;
    int direction = -1;
    int curTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (curTime < timer)
        {
            transform.position += new Vector3(direction * .12f, 0);
            curTime++;
        }
        else {
            curTime = 0;
            direction = direction * -1;
        }
    }
}
