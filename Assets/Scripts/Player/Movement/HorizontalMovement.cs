using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour {
    public float speed;
    public Camera cam;
    bool unlock;
    public GameObject confusion;
    float width;
    float height;
    public BoxCollider correctCollider;

    Animator charAnim;

    public GameObject cameraBoi;
    FollowPlayer cameraScript;
    bool col;
    float distanceToCollision;
    float extraSpeed;
    public int player;
    public GameObject player1;
    VerticalMaster masterScript;

    // Use this for initialization
    void Start()
    {
        masterScript = player1.GetComponent<VerticalMaster>();
        cameraScript = cameraBoi.GetComponent<FollowPlayer>();
        unlock = false;
        width = GetComponent<BoxCollider2D>().bounds.size.x;
        height = GetComponent<BoxCollider2D>().bounds.size.y;
        charAnim = GetComponentInChildren<Animator>();
        col = false;
        distanceToCollision = 0f;
        extraSpeed = 0f;
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = 0f;
        if (cameraScript.freezePlayers == false)
        {
            moveHorizontal = Input.GetAxis("Horizontal");
        }

        //Send data to animator
        if (charAnim != null)
        {
            //To Do: Left Move
            //if (moveHorizontal >= .1)
            //{
            //    charAnim.SetBool("walking", true);
            //    charAnim.speed = moveHorizontal;
            //}  else
            //{
            //    charAnim.SetBool("walking", false);
            //    charAnim.speed = 1;
            //}

            //Set the horizontal speed in the animator, letting the anim blend animations
            charAnim.SetFloat("horizontalSpeed", moveHorizontal);
        }

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, 0);
        //colliders test if these overlaping circles collide with anything, and list the colliders
        //three colliders for two corners on side of box and midpoint inbetween them
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed + (width / 2), 0), 0.01f);
        Collider2D[] collider2 = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed + (width / 2),  0 + (height / 2)), 0.01f);
        Collider2D[] collider3 = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed + (width / 2), 0 - (height / 2 - .01f)), 0.01f);
        Collider2D[] collider4 = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed - (width / 2), 0), 0.01f);
        Collider2D[] collider5 = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed - (width / 2), + (height / 2)), 0.01f);
        Collider2D[] collider6 = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed - (width / 2), - (height / 2 - .01f)), 0.01f);

        Collider2D[] collider7 = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed + (width / 2) + .03f, 0), 0.01f);
        Collider2D[] collider8 = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed + (width / 2) + .03f, 0 + (height / 2 - .01f)), 0.01f);
        Collider2D[] collider9 = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed + (width / 2) + .03f, 0 - (height / 2 - .01f)), 0.01f);
        Collider2D[] collider10 = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed - (width / 2) - .02f, 0), 0.01f);
        Collider2D[] collider11 = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed - (width / 2) - .02f, +(height / 2 - .01f)), 0.01f);
        Collider2D[] collider12 = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed - (width / 2) - .02f, -(height / 2 - .01f)), 0.01f);
        /*
        //if moving right
        if ((moveHorizontal > 0 && speed > 0) || (moveHorizontal < 0 && speed < 0))
        {
            //test colliders of the character's movement to the right
            collider = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed + (width / 2), movement.y * speed), 0.01f);
            collider2 = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed + (width / 2), movement.y * speed + (height / 2)), 0.01f);
            collider3 = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed + (width / 2), movement.y * speed - (height / 2 - .02f)), 0.01f);
        } else if ((moveHorizontal < 0 && speed > 0) || (moveHorizontal > 0 && speed < 0))
        {
            //test colliders of the character's movement to the left
            collider = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed - (width / 2), movement.y * speed), 0.01f);
            collider2 = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed - (width / 2), movement.y * speed + (height / 2)), 0.01f);
            collider3 = Physics2D.OverlapCircleAll(transform.position + new Vector3(movement.x * speed - (width / 2), movement.y * speed - (height / 2 - .02f)), 0.01f);
        }
        */
        col = false;
        distanceToCollision = 0f;
        //goes through everything that collided with 
        extraSpeed = 0f;
        colliderHelper(collider);
        colliderHelper(collider2);
        colliderHelper(collider3);
        colliderHelper(collider4);
        colliderHelper(collider5);
        colliderHelper(collider6);
        
        colliderHelper2(collider7);
        colliderHelper2(collider8);
        colliderHelper2(collider9);
        colliderHelper2(collider10);
        colliderHelper2(collider11);
        colliderHelper2(collider12);

        if (player == 1)
        {
            if (masterScript.onMoving == true)
            {
                extraSpeed = masterScript.onSpeed;
            }
        }
        else if (player == 2) {
            if (masterScript.onMoving2 == true) {
                extraSpeed = masterScript.onSpeed2;
            }
        }
        /*
        foreach (var top in topboi)
        {
            if (top.gameObject.GetComponent<Collideable>())
            {
                if (top.gameObject.tag == "MoveBox")
                {
                    extraSpeed = top.gameObject.GetComponent<HorizontalBox>().speed;
                    Debug.Log("topboi");
                }
            }
        }
        */
        //if at the left edge of screen (-18 is the left side of the screen)
        if ((transform.position.x + movement.x * speed) <= -41)
        {
            col = true;
            transform.position = new Vector3(-41, transform.position.y);
        }
        //if no collision was found
        if (col == false)
        {
            //if going right and not at right edge of screen)
            if (cam.WorldToScreenPoint(transform.position).x <= cam.pixelWidth && moveHorizontal > 0)
            {
                transform.position = transform.position + new Vector3(movement.x * speed, movement.y * speed);
            }
            //if going left and not at left edge of screen)
            else if (cam.WorldToScreenPoint(transform.position).x >= 0 && moveHorizontal < 0)
            {
                transform.position = transform.position + new Vector3(movement.x * speed, movement.y * speed);
            }
        } else //col == true
        {
            //Debug.Log("Distance to collision: " + distanceToCollision);
            // Move character right up to the colliding wall
            if (moveHorizontal > 0) //moving right
            {
                transform.position += new Vector3(distanceToCollision - .01f, movement.y * speed );
            } else //moving left
            {
                transform.position += new Vector3(-distanceToCollision + .01f, movement.y * speed);
            }
        }
        if (extraSpeed != 0) {
            Debug.Log("something");
            transform.position += new Vector3(extraSpeed, 0);
        }
        //transform.position = transform.position + new Vector3(movement.x * speed, movement.y * speed);
    }
    private void colliderHelper(Collider2D[] collider) {
        foreach (var collide in collider)
        {


            if (collide.gameObject.GetComponent<Collideable>())
            {
                col = true;
                distanceToCollision = GetComponent<BoxCollider2D>().Distance(collide).distance;
                //Debug.Log(GetComponent<BoxCollider2D>().Distance(collide).distance);

            }
        }
    }
    private void colliderHelper2(Collider2D[] collider)
    {
        foreach (var collide in collider)
        {


            if (collide.gameObject.GetComponent<Collideable>())
            {
                if (collide.gameObject.tag == "MoveBox")
                {
                    Debug.Log("in movebox");
                    extraSpeed = collide.gameObject.GetComponent<HorizontalBox>().speed;
                }
            }
        }
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
    
}
