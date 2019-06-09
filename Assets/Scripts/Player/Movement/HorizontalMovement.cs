using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour {
    public float speed;
    public Camera cam;
    bool unlock;
    public GameObject confusion;

    // Use this for initialization
    void Start()
    {
        unlock = false;
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
        if ((moveHorizontal > 0 && speed > 0) || (moveHorizontal < 0 && speed < 0))
        {
            //test colliders of the character's movement to the right
            collider = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed + 0.5f, movement.y * speed), 0.01f);
            collider2 = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed + 0.5f, movement.y * speed + 0.5f), 0.01f);
            collider3 = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed + 0.5f, movement.y * speed - 0.48f), 0.01f);
        } else if ((moveHorizontal < 0 && speed > 0) || (moveHorizontal > 0 && speed < 0))
        {
            //test colliders of the character's movement to the left
            collider = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed - 0.5f, movement.y * speed), 0.01f);
            collider2 = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed - 0.5f, movement.y * speed + 0.5f), 0.01f);
            collider3 = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed - 0.5f, movement.y * speed - 0.48f), 0.01f);
        }
        bool col = false;
        float distanceToCollision = 0f;
        //goes through everything that collided with 
        foreach (var collide in collider)
        {
           

            if (collide.gameObject.GetComponent<Collideable>())
            {
                col = true;
                Debug.Log(GetComponent<BoxCollider2D>().Distance(collide).distance);
                distanceToCollision = GetComponent<BoxCollider2D>().Distance(collide).distance;
            }
        }
        foreach (var collide2 in collider2)
        {
            //if (collide2.tag != "Player" && collide2.tag != "Ground" && collide2.tag != "Key" && collide2.tag != "Door")
            //{
            //    col = true;
            //}
            if (collide2.gameObject.GetComponent<Collideable>())
            {
                col = true;
            }
        }
        foreach (var collide3 in collider3)
        {
            //if (collide3.tag != "Player" && collide3.tag != "Ground" && collide3.tag != "Key" && collide3.tag != "Door")
            //{
            //    col = true;
            //}
            if (collide3.gameObject.GetComponent<Collideable>())
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
        } else //col == true
        {
            Debug.Log("Distance to collision: " + distanceToCollision);
            // Move character right up to the colliding wall
            if (moveHorizontal > 0) //moving right
            {
                transform.position += new Vector3(distanceToCollision - .01f, movement.y * speed );
            } else //moving left
            {
                transform.position += new Vector3(-distanceToCollision - .01f, movement.y * speed);
            }
        }
        //transform.position = transform.position + new Vector3(movement.x * speed, movement.y * speed);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Shroom")
        {
            speed = speed * -1;
            Destroy(other.gameObject);
            if (speed < 0)
            {
                confusion.SetActive(true);
            } else if (speed > 0)
            {
                confusion.SetActive(false);
            }
        }
    }
    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.tag == "Key")
    //    {
    //        unlock = true;
    //        Destroy(other.gameObject);
    //    }
    //    if (other.tag == "Door")
    //    {
    //        if (unlock == true)
    //        {
    //            Destroy(other.gameObject);
    //            unlock = false;
    //        }
    //    }
    //}
}
