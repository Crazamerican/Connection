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
    //public BoxCollider correctCollider;

    public Animator charAnim;

    public GameObject cameraBoi;
    FollowPlayer cameraScript;
    bool col;
    float distanceToCollision;
    float extraSpeed;
    public int player;
    public GameObject player1;
    VerticalMaster masterScript;

    public int moveDirection;

    public bool onMoveLeft;

    public float cameraStart;

    public float firstStart;
    public float firstEnd;
    public float secondStart;

    public bool touchingMoving;
    public float movingPush;

    public bool pushLeft;
    public bool nonPushLeft;
    public bool pushRight;
    public bool nonPushRight;

    // Use this for initialization
    void Start()
    {
        pushLeft = false;
        nonPushLeft = false;
        pushRight = false;
        nonPushRight = false;
        touchingMoving = false;
        movingPush = 0f;
        onMoveLeft = false;
        masterScript = player1.GetComponent<VerticalMaster>();
        cameraScript = cameraBoi.GetComponent<FollowPlayer>();
        unlock = false;
        width = GetComponent<BoxCollider2D>().bounds.size.x;
        height = GetComponent<BoxCollider2D>().bounds.size.y;
        //charAnim = GetComponentInChildren<Animator>();
        col = false;
        distanceToCollision = 0f;
        extraSpeed = 0f;
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        movingPush = 0f;
        touchingMoving = false;
        //Store the current horizontal input in the float moveHorizontal.
        moveDirection = 0;
        if (cameraScript.freezePlayers == false)
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                //Debug.Log("right hit");
                moveDirection = 1;


                //set the animations to point right
                charAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                //Debug.Log("left hit");
                moveDirection = -1;

                //set the animations to point left
                charAnim.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            
        }

        //send data to animator
        charAnim.SetFloat("horizontalSpeed", moveDirection);



        

        //Use the two store floats to create a new Vector2 variable movement.
        //Vector2 movement = new Vector2(moveHorizontal, 0);

        Collider2D[] collider7 = Physics2D.OverlapCircleAll(transform.position + new Vector3(moveDirection * speed + (width / 2) + .03f, 0), 0.01f);
        Collider2D[] collider8 = Physics2D.OverlapCircleAll(transform.position + new Vector3(moveDirection * speed + (width / 2) + .03f, 0 + (height / 2 - .01f)), 0.01f);
        Collider2D[] collider9 = Physics2D.OverlapCircleAll(transform.position + new Vector3(moveDirection * speed + (width / 2) + .03f, 0 - (height / 2 - .01f)), 0.01f);
        Collider2D[] collider10 = Physics2D.OverlapCircleAll(transform.position + new Vector3(moveDirection * speed - (width / 2) - .03f, 0), 0.01f);
        Collider2D[] collider11 = Physics2D.OverlapCircleAll(transform.position + new Vector3(moveDirection * speed - (width / 2) - .03f, +(height / 2 - .01f)), 0.01f);
        Collider2D[] collider12 = Physics2D.OverlapCircleAll(transform.position + new Vector3(moveDirection * speed - (width / 2) - .03f, -(height / 2 - .01f)), 0.01f);

        colliderHelper2(collider7, false);
        colliderHelper2(collider8, false);
        colliderHelper2(collider9, false);
        colliderHelper2(collider10, true);
        colliderHelper2(collider11, true);
        colliderHelper2(collider12, true);

        if (player == 1)
        {
            if (masterScript.onMoving == true)
            {
                extraSpeed = masterScript.onSpeed;
            }
        }
        else if (player == 2)
        {
            if (masterScript.onMoving2 == true)
            {
                extraSpeed = masterScript.onSpeed2;
            }
        }

        float additionalSpeed = 0.0f;

        if (extraSpeed != 0)
        {
            //Debug.Log("something");
            if (player == 1)
            {
                Debug.Log("additional speed 2 bois");
                if (masterScript.onMoving == false)
                {
                    if (!onMoveLeft && extraSpeed >= 0)
                    {
                        additionalSpeed += extraSpeed - .05f;
                        //transform.position += new Vector3(extraSpeed + .02f, 0);
                    }
                    else if (!onMoveLeft && extraSpeed < 0)
                    {
                        additionalSpeed += -.05f;
                        //transform.position += new Vector3(.02f, 0);
                    }
                    else if (onMoveLeft && extraSpeed <= 0)
                    {
                        additionalSpeed += extraSpeed + .05f;
                        //transform.position += new Vector3(extraSpeed - .02f, 0);
                    }
                    else if (onMoveLeft && extraSpeed > 0)
                    {
                        additionalSpeed += .05f;
                        //transform.position += new Vector3(-.02f, 0);
                    }
                }
            }
            else if (player == 2)
            {
                Debug.Log("additional speed 2 bois");
                if (masterScript.onMoving2 == false)
                {
                    if (!onMoveLeft && extraSpeed >= 0)
                    {
                        additionalSpeed += extraSpeed - .05f;
                        //transform.position += new Vector3(extraSpeed - .05f, 0);
                    }
                    else if (!onMoveLeft && extraSpeed < 0)
                    {
                        additionalSpeed += -.05f;
                        //transform.position += new Vector3(-0.05f, 0);
                    }
                    else if (onMoveLeft && extraSpeed <= 0)
                    {
                        additionalSpeed += extraSpeed + .05f;
                        //transform.position += new Vector3(extraSpeed + .05f, 0);
                    }
                    else if (onMoveLeft && extraSpeed > 0)
                    {
                        additionalSpeed += .05f;
                        //transform.position += new Vector3(.05f, 0);
                    }
                }
            }
            additionalSpeed += extraSpeed;
            //transform.position += new Vector3(extraSpeed, 0);
        }

        //float notMovingCheck = 0f;
        //if (moveHorizontal == 0) {
        //    notMovingCheck = .05f;
        //}

        //colliders test if these overlaping circles collide with anything, and list the colliders
        //three colliders for two corners on side of box and midpoint inbetween them
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position + new Vector3(moveDirection * speed + additionalSpeed, 0), 0.01f);
        Collider2D[] collider2 = Physics2D.OverlapCircleAll(transform.position + new Vector3(moveDirection * speed + additionalSpeed, (height / 2)), 0.01f);
        Collider2D[] collider3 = Physics2D.OverlapCircleAll(transform.position + new Vector3(moveDirection * speed + additionalSpeed, -(height / 2 - .02f)), 0.01f);
        //test colliders of the character's movement to the right
        Collider2D[] collider4 = Physics2D.OverlapCircleAll(transform.position + new Vector3(moveDirection * speed + (width / 2) + additionalSpeed * .98f + .05f, 0), 0.01f);
        Collider2D[] collider5 = Physics2D.OverlapCircleAll(transform.position + new Vector3(moveDirection * speed + (width / 2) + additionalSpeed * .98f + .05f, (height / 2)), 0.01f);
        Collider2D[] collider6 = Physics2D.OverlapCircleAll(transform.position + new Vector3(moveDirection * speed + (width / 2) + additionalSpeed * .98f + .05f, -(height / 2 - .02f)), 0.01f);
        //test colliders of the character's movement to the left
        Collider2D[] collider13 = Physics2D.OverlapCircleAll(transform.position + new Vector3(moveDirection * speed - (width / 2) - additionalSpeed - .05f, 0), 0.01f);
        Collider2D[] collider14 = Physics2D.OverlapCircleAll(transform.position + new Vector3(moveDirection * speed - (width / 2) - additionalSpeed - .05f, (height / 2)), 0.01f);
        Collider2D[] collider15 = Physics2D.OverlapCircleAll(transform.position + new Vector3(moveDirection * speed - (width / 2) - additionalSpeed - .05f, -(height / 2 - .02f)), 0.01f);

        col = false;
        distanceToCollision = 0f;
        //goes through everything that collided with 
        extraSpeed = 0f;
        if ((moveDirection > 0 && speed > 0) || (moveDirection < 0 && speed < 0))
        {
            colliderHelper(collider4, true);
            colliderHelper(collider5, true);
            colliderHelper(collider6, true);
            colliderHelper(collider13, false);
            colliderHelper(collider14, false);
            colliderHelper(collider15, false);
            colliderHelper(collider, false);
            colliderHelper(collider2, false);
            colliderHelper(collider3, false);
        }
        else if ((moveDirection < 0 && speed > 0) || (moveDirection > 0 && speed < 0))
        {
            colliderHelper(collider4, false);
            colliderHelper(collider5, false);
            colliderHelper(collider6, false);
            colliderHelper(collider13, true);
            colliderHelper(collider14, true);
            colliderHelper(collider15, true);
            colliderHelper(collider, false);
            colliderHelper(collider2, false);
            colliderHelper(collider3, false);
        }
        else {
            colliderHelper(collider4, false);
            colliderHelper(collider5, false);
            colliderHelper(collider6, false);
            colliderHelper(collider13, false);
            colliderHelper(collider14, false);
            colliderHelper(collider15, false);
            colliderHelper(collider, true);
            colliderHelper(collider2, true);
            colliderHelper(collider3, true);
        }

        pushLeft = false;
        nonPushLeft = false;
        pushRight = false;
        nonPushRight = false;

        //test colliders of the character's with push blocks on right
        Collider2D[] colliderRight1 = Physics2D.OverlapCircleAll(transform.position + new Vector3((width / 2) + .05f, 0), 0.01f);
        Collider2D[] colliderRight2 = Physics2D.OverlapCircleAll(transform.position + new Vector3((width / 2) + .05f, (height / 2)), 0.01f);
        Collider2D[] colliderRight3 = Physics2D.OverlapCircleAll(transform.position + new Vector3((width / 2) + .05f, -(height / 2 - .02f)), 0.01f);
        //test colliders of the character's with push blocks on left
        Collider2D[] colliderLeft1 = Physics2D.OverlapCircleAll(transform.position + new Vector3(-(width / 2) - .05f, 0), 0.01f);
        Collider2D[] colliderLeft2 = Physics2D.OverlapCircleAll(transform.position + new Vector3(-(width / 2) - .05f, (height / 2)), 0.01f);
        Collider2D[] colliderLeft3 = Physics2D.OverlapCircleAll(transform.position + new Vector3(-(width / 2) - .05f, -(height / 2 - .02f)), 0.01f);
        collidePush(colliderRight1, false);
        collidePush(colliderRight2, false);
        collidePush(colliderRight3, false);
        //collidePush(colliderLeft1, true);
        collidePush(colliderLeft2, true);
        collidePush(colliderLeft3, true);

        //Debug.Log(transform.position.x);
        //if at the left edge of screen (-18 is the left side of the screen)
        if ((transform.position.x + moveDirection * speed) <= cameraStart)
        {
            col = true;
            transform.position = new Vector3(cameraStart, transform.position.y);
        }
        else if ((transform.position.x + moveDirection * speed) <= firstStart && cameraScript.switchToSecond == false)
        {
            col = true;
            transform.position = new Vector3(firstStart, transform.position.y);
        }
        else if ((transform.position.x + moveDirection * speed) <= secondStart && cameraScript.switchToSecond == true)
        {
            col = true;
            transform.position = new Vector3(secondStart + .05f, transform.position.y);
        }
        else if ((transform.position.x + moveDirection * speed) >= firstEnd && cameraScript.switchToSecond == false)
        {
            col = true;
            transform.position = new Vector3(firstEnd, transform.position.y);
        }
        //if no collision was found
        if (col == false)
        {
            if (touchingMoving == true)
            {
                transform.position = transform.position + new Vector3(movingPush, 0);
            }
            //if going right and not at right edge of screen)
            else if (cam.WorldToScreenPoint(transform.position).x <= cam.pixelWidth && moveDirection > 0)
            {
                Debug.Log("hit!!!");
                transform.position = transform.position + new Vector3(moveDirection * speed, 0);
            }
            //if going left and not at left edge of screen)
            else if (cam.WorldToScreenPoint(transform.position).x >= 0 && moveDirection < 0)
            {
                transform.position = transform.position + new Vector3(moveDirection * speed, 0);
            }
        } else //col == true
        {
            //Debug.Log("Distance to collision: " + distanceToCollision);
            // Move character right up to the colliding wall
            if (moveDirection > 0) //moving right
            {
                transform.position += new Vector3(distanceToCollision - .01f, 0);
            } else  //moving left
            {
                transform.position += new Vector3(-distanceToCollision + .01f, 0);
            }
        }
        col = false;
        if (additionalSpeed != 0) {
            Debug.Log("wubba");
            //colliders test if these overlaping circles collide with anything, and list the colliders
            //three colliders for two corners on side of box and midpoint inbetween them
            Collider2D[] collider16 = Physics2D.OverlapCircleAll(transform.position + new Vector3(additionalSpeed, 0), 0.01f);
            Collider2D[] collider17 = Physics2D.OverlapCircleAll(transform.position + new Vector3(additionalSpeed, (height / 2)), 0.01f);
            Collider2D[] collider18 = Physics2D.OverlapCircleAll(transform.position + new Vector3(additionalSpeed, -(height / 2 - .02f)), 0.01f);
            //test colliders of the character's movement to the right
            Collider2D[] collider19 = Physics2D.OverlapCircleAll(transform.position + new Vector3(additionalSpeed + (width / 2), 0), 0.01f);
            Collider2D[] collider20 = Physics2D.OverlapCircleAll(transform.position + new Vector3(additionalSpeed + (width / 2), (height / 2)), 0.01f);
            Collider2D[] collider21 = Physics2D.OverlapCircleAll(transform.position + new Vector3(additionalSpeed + (width / 2), - (height / 2 - .02f)), 0.01f);
            //test colliders of the character's movement to the left
            Collider2D[] collider22 = Physics2D.OverlapCircleAll(transform.position + new Vector3(additionalSpeed - (width / 2), 0), 0.01f);
            Collider2D[] collider23 = Physics2D.OverlapCircleAll(transform.position + new Vector3(additionalSpeed - (width / 2), (height / 2)), 0.01f);
            Collider2D[] collider24 = Physics2D.OverlapCircleAll(transform.position + new Vector3(additionalSpeed - (width / 2), - (height / 2 - .02f)), 0.01f);
            colliderHelper(collider16, true);
            colliderHelper(collider17, true);
            colliderHelper(collider18, true);
            colliderHelper(collider19, true);
            colliderHelper(collider20, true);
            colliderHelper(collider21, true);
            colliderHelper(collider22, true);
            colliderHelper(collider23, true);
            colliderHelper(collider24, true);
        }
        //if no collision was found
        if (col == false)
        {
            transform.position = transform.position + new Vector3(additionalSpeed, 0);
        }
        else 
        {
            if (additionalSpeed > 0) //moving right
            {
                transform.position += new Vector3(distanceToCollision - .05f, 0);
            }
            else //moving left
            {
                transform.position += new Vector3(-distanceToCollision + .05f, 0);
            }
        }

        //transform.position += new Vector3(additionalSpeed, 0);
        //transform.position = transform.position + new Vector3(movement.x * speed, movement.y * speed);
    }
    private void collidePush(Collider2D[] collider, bool onLeft) {
        bool leftCol = false;
        bool rightCol = false;
        foreach (var collide in collider)
        {
            if (collide.gameObject.tag == "Push")
            {
                pushBlock pushScript = collide.gameObject.GetComponent<pushBlock>();
                leftCol = pushScript.leftCol;
                rightCol = pushScript.rightCol;
            }
            if (collide.gameObject.tag == "Push" && ((!leftCol && moveDirection == -1) || (!rightCol && moveDirection == 1)) && Input.GetButton("Push"))
            {
                if (onLeft)
                {
                    pushLeft = true;
                }
                else
                {
                    pushRight = true;
                }
                touchingMoving = true;
                if (moveDirection == 1)
                {
                    if (masterScript.grounded || masterScript.grounded2) {
                        movingPush = speed * 1 / 2;
                        collide.gameObject.GetComponent<pushBlock>().movingPush = movingPush;
                    }
                }
                else if (moveDirection == -1)
                {
                    if (masterScript.grounded || masterScript.grounded2)
                    {
                        movingPush = -speed * 1 / 2;
                        collide.gameObject.GetComponent<pushBlock>().movingPush = movingPush;
                    }
                }
            }
            else if (collide.gameObject.GetComponent<Collideable>()) {
                if (onLeft)
                {
                    nonPushLeft = true;
                }
                else
                {
                    nonPushRight = true;
                }
            }
        }
    }
    private void colliderHelper(Collider2D[] collider, bool allColliders) {
        bool leftCol = false;
        bool rightCol = false;
        foreach (var collide in collider)
        {
            if (collide.gameObject.tag == "Push")
            {
                pushBlock pushScript = collide.gameObject.GetComponent<pushBlock>();
                leftCol = pushScript.leftCol;
                rightCol = pushScript.rightCol;
            }
            if (collide.gameObject.GetComponent<Collideable>() && allColliders == true)
            {
                if (!masterScript.grounded && !masterScript.grounded2) {
                    col = true;
                    distanceToCollision = GetComponent<BoxCollider2D>().Distance(collide).distance;
                }
                else if (collide.gameObject.tag != "Push" || ((collide.gameObject.tag == "Push" && !Input.GetButton("Push"))) || (((collide.gameObject.tag == "Push" && Input.GetButton("Push"))) && ((leftCol && moveDirection == -1) || (rightCol && moveDirection == 1)))) {
                    col = true;
                    distanceToCollision = GetComponent<BoxCollider2D>().Distance(collide).distance;
                }
            }
            else if (collide.gameObject.tag == "MoveBox" && allColliders == false) {
                float otherMovingSpeed = collide.gameObject.GetComponent<HorizontalBox>().speed;
                col = true;
                if (otherMovingSpeed < 0)
                {
                    distanceToCollision = GetComponent<BoxCollider2D>().Distance(collide).distance + .04f;
                }
                else if (otherMovingSpeed > 0)
                {
                    distanceToCollision = GetComponent<BoxCollider2D>().Distance(collide).distance - .04f;
                }
                else {
                    distanceToCollision = GetComponent<BoxCollider2D>().Distance(collide).distance;
                }
            }
        }
    }
    private void colliderHelper2(Collider2D[] collider, bool leftside)
    {
        foreach (var collide in collider)
        {


            if (collide.gameObject.GetComponent<Collideable>())
            {
                if (collide.gameObject.tag == "MoveBox")
                {
                    Debug.Log("in movebox");
                    extraSpeed = collide.gameObject.GetComponent<HorizontalBox>().speed;
                    onMoveLeft = leftside;
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
