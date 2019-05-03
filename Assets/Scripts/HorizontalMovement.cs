using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour {
    public float speed;
    public Camera cam;

    // Use this for initialization
    void Start()
    {
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, 0);
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed, movement.y * speed), 0.01f);
        Collider2D[] collider2 = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed, movement.y * speed + 0.5f), 0.01f);
        Collider2D[] collider3 = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed, movement.y * speed - 0.48f), 0.01f);
        if (moveHorizontal > 0)
        {
            collider = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed + 0.5f, movement.y * speed), 0.01f);
            collider2 = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed + 0.5f, movement.y * speed + 0.5f), 0.01f);
            collider3 = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed + 0.5f, movement.y * speed - 0.48f), 0.01f);
        } else if (moveHorizontal < 0)
        {
            collider = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed - 0.5f, movement.y * speed), 0.01f);
            collider2 = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed - 0.5f, movement.y * speed + 0.5f), 0.01f);
            collider3 = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed - 0.5f, movement.y * speed - 0.48f), 0.01f);
        }
        bool col = false;
        foreach (var collide in collider)
        {
            if (collide.tag != "Player")
            {
                if (collide.tag != "Ground")
                {
                    //Debug.Log(collide);
                    col = true;
                }
            }
        }
        foreach (var collide2 in collider2)
        {
            if (collide2.tag != "Player" && collide2.tag != "Ground")
            {
                col = true;
            }
        }
        foreach (var collide3 in collider3)
        {
            if (collide3.tag != "Player" && collide3.tag != "Ground")
            {
                //Debug.Log(collide);
                col = true;
            }
        }
        if ((transform.position.x + movement.x * speed) <= -18)
        {
            col = true;
            transform.position = new Vector3(-18, transform.position.y);
        }
        if (col == false)
        {
            if (cam.WorldToScreenPoint(transform.position).x <= cam.pixelWidth - 20 && moveHorizontal > 0)
            {
                transform.position = transform.position + new Vector3(movement.x * speed, movement.y * speed);
            } else if (cam.WorldToScreenPoint(transform.position).x >= 20 && moveHorizontal < 0)
            {
                transform.position = transform.position + new Vector3(movement.x * speed, movement.y * speed);
            }
        }
        //transform.position = transform.position + new Vector3(movement.x * speed, movement.y * speed);
    }
}
