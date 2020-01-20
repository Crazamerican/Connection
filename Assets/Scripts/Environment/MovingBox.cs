using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBox : MonoBehaviour
{

    public float xEnd;
    public float yEnd;
    public float speed;

    Vector3 start;
    Vector3 end;
    Vector3 target;
    float t;

    float changeDirection;

    bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;
        end = new Vector3(start.x + xEnd, start.y + yEnd, 0);
        t = 0;
        changeDirection = 1;
        canMove = true;
        speed = speed / 100;
        target = end;
    }

    // Update is called once per frame
    void Update()
    {
        
        //canMove = true;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            t = Time.deltaTime * speed;

            transform.position = Vector3.MoveTowards(transform.position, target, speed);
            if (transform.position == start)
            {
                target = end;
            } else if (transform.position == end)
            {
                target = start;
            }
        }
    }

    public float getT()
    {
        return t;
    }

    public void stopMoving()
    {
        canMove = false;
    }
}
