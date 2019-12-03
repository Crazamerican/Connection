using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBox : MonoBehaviour
{

    public float xEnd;
    public float yEnd;
    public float speed;

    Vector2 start;
    Vector2 end;
    float t;

    float changeDirection;

    bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;
        end = new Vector2(start.x + xEnd, start.y + yEnd);
        t = 0;
        changeDirection = 1;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            t += (Time.deltaTime / speed) * changeDirection;

            transform.position = Vector2.Lerp(start, end, t);
            if (t >= 1.0 || t < 0.0)
            {
                changeDirection = changeDirection * -1;
            }
        }
        //canMove = true;
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
