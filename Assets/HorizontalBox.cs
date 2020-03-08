using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalBox : MonoBehaviour
{
    public float speed = .1f;
    public int timer = 20;
    int time;
    int direction;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        direction = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (time > timer) {
            time = 0;
            speed = speed * -1;
        }
        time++;
        transform.position += new Vector3(speed, 0, 0); 
    }
}
