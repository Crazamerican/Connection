using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullRiseAndFade : MonoBehaviour
{
    Vector3 startPos;
    public int timer;
    public float speed;
    public int delay;
    int delaycount;
    int count;
    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position;
        count = 0;
        delaycount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (delaycount > delay)
        {
            if (count < timer)
            {
                count++;
                this.transform.position += new Vector3(0, speed);
                if (count >= timer * .75f)
                {
                    this.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, this.GetComponent<SpriteRenderer>().color.a - (1.0f / (timer / 4.0f)));
                    Debug.Log(this.GetComponent<SpriteRenderer>().color.a);
                }
            }
            else
            {
                this.transform.position = startPos;
                count = 0;
                this.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
        }
        else {
            delaycount++;
        }
    }
}
