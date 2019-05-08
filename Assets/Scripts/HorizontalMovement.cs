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
        //colliders test if these overlaping circles collide with anything, and list the colliders
        //three colliders for two corners on side of box and midpoint inbetween them
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed, movement.y * speed), 0.01f);
        Collider2D[] collider2 = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed, movement.y * speed + 0.5f), 0.01f);
        Collider2D[] collider3 = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed, movement.y * speed - 0.48f), 0.01f);
        //if moving right
        if (moveHorizontal > 0)
        {
            //test colliders of the character's movement to the right
            collider = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed + 0.5f, movement.y * speed), 0.01f);
            collider2 = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed + 0.5f, movement.y * speed + 0.5f), 0.01f);
            collider3 = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed + 0.5f, movement.y * speed - 0.48f), 0.01f);
        } else if (moveHorizontal < 0)
        {
            //test colliders of the character's movement to the left
            collider = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed - 0.5f, movement.y * speed), 0.01f);
            collider2 = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed - 0.5f, movement.y * speed + 0.5f), 0.01f);
            collider3 = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed - 0.5f, movement.y * speed - 0.48f), 0.01f);
        }
        bool col = false;
        //goes through everything that collided with 
        foreach (var collide in collider)
        {
            if (collide.tag != "Player")
            {
                if (collide.tag != "Ground")
                {
                    //if this collider isn't the Player and isn't the ground
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
                col = true;
            }
        }
        //if at the left edge of screen (-18 is the left side of the screen)
        if ((transform.position.x + movement.x * speed) <= -18)
        {
            col = true;
            transform.position = new Vector3(-18, transform.position.y);
        }
        //if no collision was found
        if (col == false)
        {
            //if going right and not at right edge of screen)
            if (cam.WorldToScreenPoint(transform.position).x <= cam.pixelWidth - 20 && moveHorizontal > 0)
            {
                transform.position = transform.position + new Vector3(movement.x * speed, movement.y * speed);
            }
            //if going left and not at left edge of screen)
            else if (cam.WorldToScreenPoint(transform.position).x >= 20 && moveHorizontal < 0)
            {
                transform.position = transform.position + new Vector3(movement.x * speed, movement.y * speed);
            }
        }
        //transform.position = transform.position + new Vector3(movement.x * speed, movement.y * speed);
    }
}
