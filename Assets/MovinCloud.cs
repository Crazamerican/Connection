using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovinCloud : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= -51.35759f)
        {
            transform.position = new Vector3(8.237969f, transform.position.y, transform.position.z);
        }
        else {
            transform.position = transform.position + new Vector3(-speed, 0);
        }
    }
}
