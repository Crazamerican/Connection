using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blossomfall : MonoBehaviour
{
    public float xfall;
    public float yfall;
    public int timerEnd;
    int timer;
    private float length, startpos;
    public GameObject cam;
    public float parallaxEffect;
    float offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = 0.0f;
        timer = 0;
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect + offset);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
        Vector3 fall = new Vector3(xfall, -yfall);
        transform.position = transform.position + fall;
        offset += xfall;
        timer++;
        if (timer >= timerEnd) {
            timer = 0;
            xfall = xfall * -1;
            if (xfall > 0 && xfall < .016)
            {
                xfall += .001f;
            }
            else if (xfall < 0 && xfall > -.016) {
                xfall -= .001f;
            }
            //yfall += .0005f;
        }
    }
}
